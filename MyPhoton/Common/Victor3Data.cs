using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// serialzable  -- 可序列化的
    /// 传输玩家坐标数据
    /// </summary>
    [Serializable]
    public class Victor3Data
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
