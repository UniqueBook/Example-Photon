using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common;
using System.Xml.Serialization;
using System.IO;
using Photon.SocketServer;

namespace MyPhoton.Threads
{
    /// <summary>
    /// 同步位置信息（线程）
    /// </summary>
    public class SyncPositionThread
    {
        private Thread thread;

        /// <summary>
        /// 启动线程
        /// </summary>
        public void Run()
        {
            thread = new Thread(UpdatePosition)
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {
            thread.Abort();
        }

        /// <summary>
        /// 同步位置信息
        /// </summary>
        private void UpdatePosition()
        {
            Thread.Sleep(5000);//毫秒
            while (true)
            {
                Thread.Sleep(200);//按照位置信息发送的频率进行同步
                SendPosition();
            }
        }

        private void SendPosition()
        {
            List<PlayerData> playerDataList = new List<PlayerData>();
            foreach (MyClient peer in MyServer._instance.clientPeer)
            {
                //IsNullOrEmpty  返回值为 false 表示用户存在
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.Username = peer.username;
                    playerData.Pos = new Victor3Data() { X = peer.x, Y = peer.y, Z = peer.z };
                    playerDataList.Add(playerData);
                }
            }
            //创建读取流
            StringWriter writer = new StringWriter();
            //将所有用户的位置信息序列化
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PlayerData>));

            xmlSerializer.Serialize(writer, playerDataList);
            //关闭流
            writer.Close();

            string playerDataListString = writer.ToString();
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PlayerDataList, playerDataListString);

            //将用户位置信息通过事件发送到各客户端
            foreach (MyClient peer in MyServer._instance.clientPeer)
            {
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    EventData eventData = new EventData((byte)EventCode.SyncPosition);
                    eventData.Parameters = data;
                    peer.SendEvent(eventData, new SendParameters());
                }
            }
        }
    }
}
