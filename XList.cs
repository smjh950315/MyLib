using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public static class ListHelper
    {
        public static dynamic NoRepeat(dynamic list)
        {
            List<dynamic> list2 = new List<dynamic>();
            foreach (var item in list)
            {
                if (!list2.Contains(item))
                {
                    list2.Add(item);
                }
            }
            return list2;
        }
    }
    public class XList<T> : System.Collections.Generic.List<T>
    {
        public XList() : base() { }
        public XList(dynamic values)
        {
            foreach (var item in values) { Add(item); }
        }        
    }
    public class XDictionary : System.Collections.Generic.Dictionary<dynamic, dynamic>
    {
        public XDictionary() : base() { }
        public XDictionary(dynamic key, dynamic value) : base()
        {
            Add(key, value);
        }
    }
}
