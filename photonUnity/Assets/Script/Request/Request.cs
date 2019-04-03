using UnityEngine;
using Common;
using ExitGames.Client.Photon;

public abstract class Request : MonoBehaviour
{
    public OperationCode opCode;

    /// <summary>
    /// 发起请求
    /// </summary>
    public abstract void DefaultRequest();

    /// <summary>
    /// 处理响应
    /// </summary>
    /// <param name="operationResponse"></param>
    public abstract void OnOperationResponse(OperationResponse operationResponse);

    //virtual关键字就是告诉子类，此方法可以被override，但非强制
    public virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);
    }

    public void OnDestroy()
    {
        PhotonEngine.Instance.RemoveRequest(this);
    }
}
