using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using MyPhoton;
using Common;
using Common.Tools;
using MyPhoton.Manger;

namespace MyPhoton.Handler
{
    class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            OpCode = OperationCode.Login;
        }

        /// <summary>
        /// 客户端发送登录请求的处理
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClient peer)
        {
            string name = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Name) as string;
            string pwd = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Pwd) as string;
            UserManager manager = new UserManager();
            bool isSuccess = manager.VerifyUser(name, pwd);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            if (isSuccess)
            {
                response.ReturnCode = (short)ReturnCode.Success;
                peer.username = name; //保存用户名到服务器
            }
            else
            {
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            //服务端发送响应到客户端
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
