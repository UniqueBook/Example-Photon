using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common.Tools;
using Common;
using System.Xml.Serialization;
using System.IO;

public class SyncPositionEvent : BaseEvent
{
    private Player player;
    public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void OnEvent(EventData eventData)
    {
        //获取数据
        string playerDataListString = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.PlayerDataList);
        Debug.Log("获取玩家数据playerDataListString：" + playerDataListString);
        //创建读取流
        using (StringReader reader = new StringReader(playerDataListString))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PlayerData>));
            List<PlayerData> playerDataList = (List<PlayerData>)xmlSerializer.Deserialize(reader);
            //reader.Close();  处于using中  会自动关闭
            player.OnSyncPositionEvent(playerDataList);
        }
    }
}
