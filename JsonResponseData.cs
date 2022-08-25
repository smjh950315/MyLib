using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using String = MyLib.String;

namespace MyLib
{
    public class JsonResponseData
    {
        public string Method { get; set; }
        public string Item { get; set; }
        public string Id { get; set; }
        public JsonResponseData()
        {
            Method = "";
            Item = "";
            Id = "";
        }
        public JsonResponseData(string rawText)
        {
            String raw = rawText;
            Method = raw.SubStrings("\"method\":\"", "\"")[0];
            Item = raw.SubStrings("\"item\":\"", "\"")[0];
            Id = raw.SubStrings("\"id\":\"", "\"")[0];
        }
    }
}
