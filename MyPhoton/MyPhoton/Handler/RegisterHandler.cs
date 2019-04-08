using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using MyPhoton.Manger;
using MyPhoton.Model;

namespace MyPhoton.Handler
{
    public class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            OpCode = OperationCode.Register;
        }

        /// <summary>
        /// 客户端发送注册请求的处理
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClient peer)
        {
            string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Name) as string;
            string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Pwd) as string;

            UserManager userManager = new UserManager();
            User user = userManager.GetByName(username);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if (user != null)
            {
                //用户存在
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            else
            {
                //用户不存在
                user = new User() { Name = username, Pwd = password };
                userManager.Add(user);
                response.ReturnCode = (short)ReturnCode.Success;
            }
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
