using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : Request
{
    //隐藏
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public string Pwd;

    private LoginPanel loginPanel;

    public override void Start()
    {
        base.Start();
        loginPanel = GetComponent<LoginPanel>();//获取组件
    }

    /// <summary>
    /// 发起请求
    /// </summary>
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Name, Name);
        data.Add((byte)ParameterCode.Pwd, Pwd);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    /// <summary>
    /// 服务端的响应处理
    /// </summary>
    /// <param name="operationResponse"></param>
    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;
        Debug.Log(returnCode);
        loginPanel.OnLoginResponse(returnCode);
    }
}
