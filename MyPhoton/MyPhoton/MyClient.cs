using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using MyPhoton.Handler;
using Common.Tools;
using Common;

namespace MyPhoton
{
    public class MyClient : ClientPeer
    {
        //调用父类里面的初始化方法，完成初始化工作
        public MyClient(InitRequest initRequest) : base(initRequest)
        {

        }

        //处理客户端的请求  sendParameters  请求/发送的相关属性
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handler = DictTool.GetValue<OperationCode, BaseHandler>(MyServer._instance.handlerDict, (OperationCode)operationRequest.OperationCode);
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                //发送默认请求处理
                BaseHandler defaultHandler = DictTool.GetValue<OperationCode, BaseHandler>(MyServer._instance.handlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }

        //处理断开连接的请求
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {

        }
    }
}
