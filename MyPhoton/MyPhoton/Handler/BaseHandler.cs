using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Photon.SocketServer;

namespace MyPhoton.Handler
{
    public abstract class BaseHandler
    {
        public OperationCode OpCode;

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClient peer);

    }
}
