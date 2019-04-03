using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{

    public static PhotonEngine Instance;
    private static PhotonPeer peer;

    public static PhotonPeer Peer
    {
        get
        {
            return peer;
        }
    }

    //通过 operationCode  找到对应的 request
    private Dictionary<OperationCode, Request> RequestDict = new Dictionary<OperationCode, Request>();

    private void Awake()
    {
        //单例  场景一创建后初始化，从场景一到场景二需将场景一初始化的值删除
        //保持场景中只有一个  PontonEngine
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);//防止销毁当前游戏物体
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject); return;//销毁当前游戏物体
        }
    }

    // Use this for initialization
    void Start()
    {
        //利用PhotonPeer  与服务器端建立连接   选择通讯协议
        //通过被监听的对象  this  接收服务器端的响应
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyPhoton");
    }

    // Update is called once per frame
    void Update()
    {
        //判断当前 peer 是否处理已连接状态
        if (peer.PeerState == PeerStateValue.Connected)
        {

        }
        //维持 peer 与服务器端的连接
        peer.Service();
    }

    void OnDestroy()
    {
        //判断是否已经断开连接
        if (peer.PeerState == PeerStateValue.Disconnected)
        {
            peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {

    }

    //服务器端向客户端直接发起的请求、通知
    public void OnEvent(EventData eventData)
    {
        switch (eventData.Code)
        {
            case 1:
                Debug.Log("收到了服务器的事件推送!");
                Dictionary<byte, object> data = eventData.Parameters;
                object intValue; object stringValue;
                data.TryGetValue(1, out intValue);
                data.TryGetValue(2, out stringValue);
                Debug.Log("收到服务器的事件推送的内容：" + intValue.ToString() + stringValue.ToString());
                break;
            default:
                break;
        }

    }

    //服务器端响应客户端请求
    public void OnOperationResponse(OperationResponse operationResponse) 
    {
        OperationCode OperationCode = (OperationCode)operationResponse.OperationCode; //强转为 OperationCode

        Request request = null;

        bool result = RequestDict.TryGetValue(OperationCode, out request); //通过 OperationCode 获取 对应的request

        if (result)
        {
            request.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("未找到响应的请求对象");
        }
    }

    //peer  状态改变会调用此方法
    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("1111111111111111");
    }

    //添加请求
    public void AddRequest(Request request)
    {
        RequestDict.Add(request.opCode, request);
    }

    //移除请求
    public void RemoveRequest(Request request)
    {
        RequestDict.Remove(request.opCode);
    }
}
