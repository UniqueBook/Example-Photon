using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;
using Common.Tools;
using System.Xml.Serialization;
using System.IO;

public class SyncPlayerRequest : Request
{
    private Player player;

    public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void DefaultRequest()
    {
        PhotonEngine.Peer.OpCustom((byte)opCode, null, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        string usernameListString = (string)DictTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.UserNameList);
        Debug.Log("开始打印在线玩家昵称:" + usernameListString);
        using (StringReader reader = new StringReader(usernameListString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            List<string> usernameList = (List<string>)serializer.Deserialize(reader);
            player.OnSyncPlayerResponse(usernameList);
        }
    }
}
