using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools
{
    public class DictTool
    {
        public static T2 GetValue<T1, T2>(Dictionary<T1, T2> dict, T1 key)
        {
            bool isSuccess = dict.TryGetValue(key, out T2 value); //内联声明变量
            if (isSuccess)
            {
                return value;
            }
            else
            {
                return default(T2);
            }
        }
    }
}
