using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 区分传送数据的时候，参数的类型
    /// </summary>
    public enum ParameterCode : byte
    {
        Name,
        Pwd,
        Position,
        X,Y,Z,
        UserNameList,
        PlayerDataList
    }
}
