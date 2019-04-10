using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;
using Common.Tools;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{

    public static PhotonEngine Instance;

    public static PhotonPeer Peer
    {
        get
        {
            return peer;
        }
    }

    private static PhotonPeer peer;

    //用户名 （唯一值）
    public static string username;

    //通过 operationCode  找到对应的 request
    private Dictionary<OperationCode, Request> RequestDict = new Dictionary<OperationCode, Request>();

    //管理事件
    private Dictionary<EventCode, BaseEvent> EventDict = new Dictionary<EventCode, BaseEvent>();

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

    //服务器端向客户端直接发起的请求、通知   (事件分发器)
    public void OnEvent(EventData eventData)
    {
        EventCode code = (EventCode)eventData.Code;
        BaseEvent baseEvent = DictTool.GetValue<EventCode, BaseEvent>(EventDict, code);
        baseEvent.OnEvent(eventData);
    }

    //服务器端响应客户端请求  (请求分发器)
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

    //添加事件
    public void AddEvent(BaseEvent baseEvent)
    {
        EventDict.Add(baseEvent.eventCode, baseEvent);
    }

    //移除事件
    public void RemoveEvent(BaseEvent baseEvent)
    {
        EventDict.Remove(baseEvent.eventCode);
    }
}
