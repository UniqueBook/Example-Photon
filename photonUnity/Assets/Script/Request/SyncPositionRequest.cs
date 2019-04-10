using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ExitGames.Client.Photon;

public class SyncPositionRequest : Request
{
    /// <summary>
    /// 通过代码设置 ， 隐藏
    /// </summary>
    [HideInInspector]
    public Vector3 pos;

    public SyncPositionRequest()
    {
        opCode = OperationCode.SyncPosition;
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.X, pos.x);
        data.Add((byte)ParameterCode.Y, pos.y);
        data.Add((byte)ParameterCode.Z, pos.z);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new System.NotImplementedException();
    }
}
