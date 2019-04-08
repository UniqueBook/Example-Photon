using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 区分请求和响应的类型
    /// </summary>
    public enum OperationCode : byte
    {
        Login,
        Register,
        Default,
        SyncPosition
    }
}
