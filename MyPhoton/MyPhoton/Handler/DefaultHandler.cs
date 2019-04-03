using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;

namespace MyPhoton.Handler
{
    public class DefaultHandler : BaseHandler
    {
        //赋予默认 OperationCode
        public DefaultHandler()
        {
            OpCode = OperationCode.Default;
        }


        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClient peer)
        {
            throw new NotImplementedException();
        }
    }
}
