using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;

namespace MyPhoton.Handler
{
    public class SyncPositionHandler : BaseHandler
    {
        public SyncPositionHandler()
        {
            OpCode = OperationCode.SyncPosition;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClient peer)
        {
            MyPhoton.MyServer.log.Info("获取用户位置信息：");
            float x = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.X);
            float y = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Y);
            float z = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Z);
            MyPhoton.MyServer.log.Info("x:" + x + "y:" + y + "z:" + z);
            peer.x = x;
            peer.y = y;
            peer.z = z;
        }
    }
}
