using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;
using Common.Tools;

public class RegisterRequest : Request
{
    [HideInInspector]
    public string username;
    [HideInInspector]
    public string password;

    private RegisterPanel registerPanel;

    public override void Start()
    {
        base.Start();
        registerPanel = GetComponent<RegisterPanel>();
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Name, username);
        data.Add((byte)ParameterCode.Pwd, password);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    /// <summary>
    /// 服务端响应客户端请求的返回
    /// </summary>
    /// <param name="operationResponse"></param>
    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;
        Debug.Log(returnCode);
        registerPanel.OnRegisterResponse(returnCode);
    }
}
