using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;
using Common.Tools;

public class NewPlayerEvent : BaseEvent
{
    private Player player;

    public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void OnEvent(EventData eventData)
    {
        string username = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.Name);

        //调用用户对象中的方法创建游戏物体
        player.OnNewPlayerEvent(username);
    }
}
