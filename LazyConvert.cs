using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public static class LazyConvert
    {
        public static bool TorF(bool? @bool) { return @bool ?? false; }
        public static bool IsNull<T>(params T[] values)
        {
            bool result = true;
            foreach (var value in values)
            {
                if (value != null) { result = false; break; }
            }
            return result;
        }

        public static T? Value<T>(params T[] values)
        {
            foreach (var value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }
            return default(T);
        }
        public static dynamic Value<T1, T2>(T1 value1, params T2[] value2)
        {
            if (value1 == null)
            {
                return Value(value2);
            }
            return value1;
        }
        public static dynamic Value<T1, T2, T3>(T1 value1, T2 value2, params T3[] value3)
        {
            var result = Value(value1, value2);
            result = result ?? Value(value3);
            return result;
        }

    }
}
