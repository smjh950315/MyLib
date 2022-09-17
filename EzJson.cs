using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class JsonAttribute : BaseAttribute
    {
        public JsonAttribute() : base() { }
        public JsonAttribute(string key, String value) : base(key, value) { }
    }
    public static class EzJson
    {
        public static JsonData ReadRawString(String rawString)
        {
            JsonData jsonData = new JsonData(rawString);
            return jsonData;
        }
    }
    public class JsonData
    {
        public String RawData { get; private set; }
        public List<JsonAttribute> Attribute { get; private set; }
        private JsonAttribute? ReadSingleAttribute(string input)
        {
            string temp = "";
            JsonAttribute jsonAttribute = new JsonAttribute();
            bool hasKey = false;
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c == ':' && !hasKey)
                {
                    if (temp.StartsWith('"'))
                    {
                        temp = temp.Substring(1);
                    }
                    if (temp.EndsWith('"'))
                    {
                        temp = temp.Substring(0, temp.Length - 1);
                    }
                    jsonAttribute.Key = temp;
                    hasKey = true;
                    temp = "";
                }
                else
                {
                    temp += c;
                }
                if (i == input.Length - 1)
                {
                    if (temp.StartsWith('"'))
                    {
                        temp = temp.Substring(1);
                    }
                    if (temp.EndsWith('"'))
                    {
                        temp = temp.Substring(0, temp.Length - 1);
                    }
                    jsonAttribute.Value = temp;
                }
            }
            if (jsonAttribute.Key == null)
            {
                return null;
            }
            return jsonAttribute;
        }
        private String ToLinear(String rawString)
        {
            string? str = (string?)rawString;
            if (str == null) { return new String(); }
            String str1 = rawString.Split('\n');
            String str2 = str1.Split('\r');
            String str3 = str2.Split(' ');
            return str3;
        }
        private string GetBracketContent(string input)
        {
            input = input.Trim();
            if (!input.StartsWith('{'))
            {
                return input;
            }
            int cbStart = 0;
            string result = "";
            foreach (char c in input)
            {
                if (c == '}')
                {
                    cbStart--;
                }
                if (cbStart > 0)
                {
                    result += c;
                }
                if (c == '{')
                {
                    cbStart++;
                }
            }
            return result;
        }
        private string[]? SplitElements(string input)
        {
            int cbStart = 0;
            int sbStart = 0;
            List<string> elements = new List<string>();
            string temp = "";
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '}')
                {
                    cbStart--;
                }
                if (c == '{')
                {
                    cbStart++;
                }
                if (c == ']')
                {
                    sbStart--;
                }
                if(c == '[')
                {
                    sbStart++;
                }
                if (c == ',' && cbStart == 0 && sbStart == 0)
                {
                    elements.Add(temp);
                    temp = "";
                }
                else
                {
                    temp += c;
                }
                if (i == input.Length - 1)
                {
                    elements.Add(temp);
                }
            }
            return elements.ToArray();
        }
        private void ReadContent(String rawText)
        {
            RawData = ToLinear(rawText);
        }
        private void Process(string? rawText)
        {
            if (rawText == null) { return; }
            if (rawText.StartsWith('{'))
            {
                rawText = GetBracketContent(rawText);
                Process(rawText);
            }
            else
            {
                var attrs = SplitElements(rawText);
                if (attrs == null) { return; }
                if (attrs.Length == 1)
                {
                    var attr = ReadSingleAttribute(attrs[0]);
                    if (attr != null)
                    {
                        Attribute.Add(attr);
                        Process(attr.Value);
                    }
                }
                else
                {
                    foreach (string attr in attrs)
                    {
                        Process(attr);
                    }
                }
            }
        }
        public void Load(String rawJsonString)
        {
            ReadContent(rawJsonString);
            Process(RawData);
        }
        public JsonAttribute? GetAttribute(string attributeName)
        {
            return Attribute.Where(a => a.Key == attributeName).FirstOrDefault();
        }
        public string? GetAttributeValue(string attributeName)
        {
            return GetAttribute(attributeName)?.Value;
        }
        public override string ToString()
        {
            string result = "";
            foreach(var attr in Attribute)
            {
                result += $"{attr.Key}:{attr.Value}\n";
            }
            return result;
        }
        public JsonData()
        {
            RawData = new String();
            Attribute = new List<JsonAttribute>();
        }
        public JsonData(String rawString):this()
        {
            Load(rawString);
        }
    }
}
