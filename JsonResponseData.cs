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
        public bool IsValid { get; set; }
        public string? Target { get; set; }
        public string? Method { get; set; }
        public string? ParamName { get; set; }
        public string? Id { get; set; }
        public void CheckValid()
        {
            if(Target == null || Method == null)
            {
                IsValid=false;
            }
            else
            {
                IsValid = true;
            }
        }
        public JsonResponseData()
        {
            Target = "";
            Method = "";
        }
        public JsonResponseData(string rawText)
        {
            String raw = rawText;
            Target = raw.SubString("\"target\":\"", "\"");
            Method = raw.SubString("\"method\":\"", "\"");
            ParamName = raw.SubString("\"parameter\":\"", "\"");
            Id = raw.SubString("\"id\":\"", "\"");
            CheckValid();
        }  
    }
}
//{"Logging":{"LogLevel":{"Default":"Information","Microsoft.AspNetCore":"Warning"}},"AllowedHosts":"*"}