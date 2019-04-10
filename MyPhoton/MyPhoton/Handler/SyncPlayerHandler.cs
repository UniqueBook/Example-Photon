using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using System.Xml.Serialization;
using System.IO;

namespace MyPhoton.Handler
{
    public class SyncPlayerHandler : BaseHandler
    {
        public SyncPlayerHandler()
        {
            OpCode = OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClient peer)
        {
            //取得所有已经登录（在线）玩家的用户名
            List<string> usernameList = new List<string>();
            foreach (MyClient tempPeer in MyServer._instance.clientPeer)
            {
                //判断名称是否存在  不包含当前玩家
                if (string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != peer)
                {
                    usernameList.Add(tempPeer.username);
                }
            }
            //将数据序列化处理
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            serializer.Serialize(sw, usernameList);
            sw.Close();
            string usernameListString = sw.ToString();
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            //传输数据不支持 list 、Diction 等类型，需转换
            data.Add((byte)ParameterCode.UserNameList, usernameListString);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.Parameters = data;
            peer.SendOperationResponse(response, sendParameters);
            //推送到其他客户端，告诉其他客户端有新的客户端加入
            foreach (MyClient tempPeer in MyServer._instance.clientPeer)
            {
                if (string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != peer)
                {
                    EventData eventData = new EventData((byte)EventCode.NewPlayer);
                    Dictionary<byte, object> dataDict = new Dictionary<byte, object>();
                    dataDict.Add((byte)ParameterCode.Name, peer.username);
                    eventData.Parameters = dataDict;
                    tempPeer.SendEvent(eventData, sendParameters);
                }
            }
        }
    }
}
