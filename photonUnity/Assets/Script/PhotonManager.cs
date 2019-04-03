using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;

public class PhotonManager : MonoBehaviour, IPhotonPeerListener
{
    private PhotonPeer peer;
    private ConnectionProtocol protocol = ConnectionProtocol.Udp;   //udp传输协议    private string serverAddress = "127.0.0.1:5055";                //服务器目前的ip地址
    private string applicationName = "MyPhoton";                    //服务器程序名称
    bool connected = false;
    private string serverAddress = "127.0.0.1:5055";

    /// <Awake>
    /// Awake用于在游戏开始之前初始化变量或游戏状态。
    /// 在脚本整个生命周期内它仅被调用一次Awake在所有对象被初始化之后调用
    /// Awake总是在Start之前被调用
    /// </Awake>
    void Awake()
    {
        Debug.Log("Awake---初始化");
        peer = new PhotonPeer(this, protocol);
    }

    /// <Start>
    /// Start仅在Update函数第一次被调用前调用。
    ///Start在behaviour的生命周期中只被调用一次。
    ///它和Awake的不同是Start只在脚本实例被启用时调用。
    /// </Start>
    void Start()
    {
        Debug.Log("Start--");
    }

    /// <OnEnable>
    /// 当对象变为可用或激活状态时此函数被调用。
    /// </OnEnable>
    void OnEnable()
    {
        Debug.Log("script was enabled");
    }

    void Update()
    {
        if (!connected)
        {
            Debug.Log("检测到未连接，重新连接");
            peer.Connect(serverAddress, applicationName);    //，如果服务器未连接，那么连接服务器
        }

        peer.Service();

        Debug.Log("给服务器发请求");
        var parameters = new Dictionary<byte, object>();    //字典
        parameters.Add(0, "武宝宝");
        peer.OpCustom(1, parameters, true);                //给服务器发请求
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("给服务器发请求");
        //    var parameters = new Dictionary<byte, object>();    //字典
        //    parameters.Add(0, "武宝宝");
        //    peer.OpCustom(1, parameters, true);                //给服务器发请求
        //}
    }

    /// <summary>
    /// 服务器断开调用，避免Unity卡死，我一开始没写这个方法unity死了很多次
    /// </summary>
    void OnDestroy()
    {
        Debug.Log("断开连接");
        peer.Disconnect();
    }

    /// <summary>
    /// 返回时调用
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnEvent(EventData eventData)
    {

    }

    /// <summary>
    /// 服务器给客户端的响应
    /// </summary>
    /// <param name="operationResponse"></param>
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log("服务器给客户端的响应");
    }

    /// <summary>
    /// 连接状态改变
    /// </summary>
    /// <param name="statusCode"></param>
    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("连接状态改变");
        Debug.Log(statusCode.ToString());
        switch (statusCode)
        {
            case StatusCode.Connect:
                connected = true;
                break;
            case StatusCode.Disconnect:
                connected = false;
                break;
        }
    }

}