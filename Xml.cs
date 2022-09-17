using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Serialization;

namespace MyLib
{
    public static class Xml
    {
        private static List<XmlAttribute> StringToAttributes(String attributesInString)
        {
            var result = new List<XmlAttribute>();
            string strType = attributesInString;
            List<string> KeyList = new();
            List<string> ValueList = new();
            string[] attributes = strType.Split("=\"");
            if (attributes.Length == 0) { return result; }
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i] = attributes[i].Trim();
            }
            for (int i = 0; i < attributes.Length; i++)
            {
                if (!attributes[i].Contains('\"'))
                {
                    KeyList.Add(attributes[i]);
                }
                else if (attributes[i].EndsWith('\"'))
                {
                    attributes[i] = attributes[i].Remove(attributes[i].Length - 1);
                    ValueList.Add(attributes[i]);
                }
                else // With " in string
                {
                    if (attributes[i].StartsWith('\"'))
                    {
                        attributes[i] = "null" + attributes[i];
                    }
                    var vk = attributes[i].Split('\"');
                    ValueList.Add(vk[0].Trim());
                    KeyList.Add(vk[1].Trim());
                }
            }
            if (KeyList.Count != ValueList.Count) { return result; }
            for (int i = 0; i < KeyList.Count; i++)
            {
                var attribute = new XmlAttribute(KeyList[i], ValueList[i]);
                result.Add(attribute);
            }
            return result;
        }
        private static string AttributeToString(params XmlAttribute[] attribute)
        {
            string result = "";
            foreach (XmlAttribute attrib in attribute)
            {
                result += $" {attrib.Key}=\"{attrib.Value}\"";
            }
            return result;
        }
        public static string GetString(XmlData data)
        {
            return GetString(data.Tag, data.Data, data.Attribute.ToArray());
        }
        public static string GetString(string tagName, string content, string attribute)
        {
            int count = content.Length;
            string result = $"<{tagName} {attribute}>";
            for (int i = 0; i < count; i++)
            {
                result += content[i];
            }
            if (result.EndsWith(","))
            {
                result = result.Substring(0, result.Length - 2);
            }
            result += $"</{tagName}>\n";
            return result;
        }
        public static string GetString(string tagName, string content, params XmlAttribute[] attributes)
        {
            return $"<{tagName}{AttributeToString(attributes)}>{content}</{tagName}>\n";
        }

        public static XmlAttribute GetAttribute(string key, string? value)
        {
            return new XmlAttribute() { Key = key, Value = value };
        }
        public static XmlAttribute? GetAttribute(List<XmlAttribute> attributes, string key)
        {
            return attributes.Where(a => a.Key == key).FirstOrDefault();
        }
        public static string? GetAttributeValue(List<XmlAttribute> attributes, string key)
        {
            var attribute = GetAttribute(attributes, key);
            return attribute != null ? attribute.Value : null;
        }
        public static XmlAttribute? GetAttribute(XmlData xmlData, string key)
        {
            return xmlData.Attribute.Where(a => a.Key == key).FirstOrDefault();
        }
        public static string GetAttributeValue(XmlData xmlData, string key)
        {
            var attribute = GetAttribute(xmlData, key);
            var result = attribute != null ? attribute.Value : null;
            return result ?? "null";
        }
        public static string GetContent(XmlData xmlData)
        {
            return xmlData.Data;
        }
        public static XmlData ReadTab(string rawData, string tagName)
        {
            XmlData data = new XmlData();
            String s = rawData;
            var attributeString = s.SubString($"<{tagName}", ">");
            data.Attribute = StringToAttributes(attributeString);
            data.Data = s.SubString($"{attributeString}>", $"</{tagName}>");
            data.Tag = tagName;
            return data;
        }
        public static List<XmlData> ReadXml(string fileName, string tagName)
        {
            string rawData = File.ReadAllText(fileName);
            String RawData2 = rawData.Split("\n");
            string tagStart = $"<{tagName}";
            string tagEnd = $"</{tagName}>";
            List<XmlData> data = new List<XmlData>();
            String s = RawData2;
            var singleDatas = s.SubStrings(tagStart, tagEnd);
            foreach (var singleData in singleDatas)
            {
                var xml = ReadTab(tagStart + singleData + tagEnd, tagName);
                if (xml != null) { data.Add(xml); }
            }
            return data;
        }
        public static string ReadTabContent(string rawData, string tagName)
        {
            String str = (String)rawData;
            str += rawData;
            string temp = str.SubString($"<{tagName}>", $"</{tagName}>");
            return temp;
        }

    }
    public class XmlAttribute : BaseAttribute
    {
        public XmlAttribute() : base() { }
        public XmlAttribute(string key, String value) : base(key, value) { }        
    }
    public class XmlData
    {
        public string Tag { get; set; }
        public string Data { get; set; }
        public List<XmlAttribute> Attribute { get; set; }
        public void AddAttribute(params XmlAttribute[] attributes)
        {
            foreach (var attr in attributes)
            {
                Attribute.Add(attr);
            }
        }
        public void AddAttribute(string key, String? value)
        {
            value ??= "null";
            var a = new XmlAttribute(key, value);
            Attribute.Add(a);
        }
        public XmlAttribute? GetAttribute(string name)
        {
            return Attribute.Where(a => a.Key == name).FirstOrDefault();
        }
        public XmlData() : this(null, null, null) 
        {
        }
        public XmlData(string tag) : this(tag, null, null) 
        {
        }
        public XmlData(string? tag, string? data, params XmlAttribute[]? attributes)
        {
            Tag = tag ?? "tag";
            Data = data ?? "N/A";
            Attribute = attributes?.ToList() ?? new List<XmlAttribute>();
        }

        public override string ToString()
        {
            return Xml.GetString(this);
        }
    }
}
