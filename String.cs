using Microsoft.VisualBasic;
using System;

namespace MyLib
{
    public class String
    {
        private char[] numbers = new char[]
        {
            '0','1','2','3','4','5','6','7','8','9'
        };
        private string Str { get; set; }
        public int Length { get; private set; }
        public bool IsNull { get; private set; }
        private List<String>? TempList { get; set; }        
        private String SubString(string end_of_start, int iEnd)
        {
            String result = new();
            int IndexStart = Str.IndexOf(end_of_start) + end_of_start.Length;
            int IndexEnd = iEnd;
            result = SubString(IndexStart, IndexEnd);
            return result;
        }
        private String SubString(int iStart, string start_of_end)
        {
            String result = new();
            int IndexStart = iStart;
            int IndexEnd = Str.IndexOf(start_of_end) - 1;
            result = SubString(IndexStart, IndexEnd);
            return result;
        } 
        private void SubStringsProcess(string end_of_start, string start_of_end)
        {
            TempList = new();
            String temp = new(Str);
            bool IsFind = true;
            for (; IsFind;)
            {
                int IndexStart = temp.Str.IndexOf(end_of_start) + end_of_start.Length;
                if (IndexStart > 0)
                {
                    temp = new String(temp.SubString(IndexStart, temp.Length - 1));
                }
                int IndexEnd = temp.Str.IndexOf(start_of_end) - 1;
                string find = temp.SubString(0, IndexEnd);
                IsFind = !string.IsNullOrEmpty(find);
                temp = new String(temp.SubString(IndexEnd + start_of_end.Length + 1, temp.Length - 1));
                if (find.Length > 0) { TempList.Add(find); }
            }
        }
        private void SetValue(object? obj)
        {
            Str = ParseString(obj);
            Length = Str.Length;
            IsNull = Length == 0;
        }
        private string ParseNumber()
        {
            string number = "0";
            for(int i=0;i< Str.Length; i++)
            {
                foreach(char n in numbers)
                {
                    if(Str[i] == n)
                    {
                        number += n;
                        break;
                    }
                }
            }
            return number;
        }
        public String()
        {
            Str = "";
            Length = 0;
            SetValue(null);
        }
        public String(char ch) : this() 
        {
            SetValue(ch);
        }
        public String(string? str) : this()
        {
            SetValue(str);
        }
        public String(object? obj) : this()
        {          
            SetValue(obj);
        }
        public static string ParseString(object? obj)
        {
            try
            {
                return obj?.ToString() ?? "";                
            }
            catch
            {                
                return "";
            }            
        }
        public static string? TryParseString(object? obj)
        {
            try
            {
                return obj?.ToString();
            }
            catch
            {
                return null;
            }
        }        
        public static string? GetValidContent(object? obj)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                var str = obj.ToString();
                if (string.IsNullOrEmpty(str)) { return null; }
                else { return str; }
            }
            catch
            {
                return null;
            }
        }
        public String PaddingFront(int length, char ch)
        {
            String c = new String(ch);
            return new String(c * length + Str);
        }
        public String PaddingEnd(int length, char ch)
        {
            String c = new String(ch);
            return new String(Str + c * length);
        }
        public String SubString(int iStrat, int iEnd)
        {
            string result = "";            
            iEnd = iEnd < Length ? iEnd : Length - 1;
            if(iEnd < 0) { return new String(result); }
            if (iEnd < 1 || iEnd > Length-1) { return new String(result); }
            if(iStrat < 0 || iStrat >= Length-1) { return new String(result); }
            if(iEnd < iStrat) { return new String(result); }
            for (int i = iStrat; i <= iEnd; i++) { result += Str[i]; }
            return new String(result);
        }
        public String SubString(string end_of_start, string start_of_end)
        {
            //String result = new();
            //int IndexStart = Str.IndexOf(end_of_start) + end_of_start.Length;
            //int IndexEnd = Str.IndexOf(start_of_end) - 1; 
            //result = SubString(IndexStart, IndexEnd);
            String result = new();
            int iStart = Str.IndexOf(end_of_start);
            int iEnd = Str.IndexOf(end_of_start);
            if (iStart < 0) { return result; }
            if (iEnd < 0) { return result; }
            int IndexStart = Str.IndexOf(end_of_start) + end_of_start.Length;
            if(Str.Length - 1 < IndexStart) { return result; }
            string? temp = Str.Substring(IndexStart);
            if (temp == null) { return result; }
            int IndexEnd = temp.IndexOf(start_of_end);
            if(IndexEnd < 0) { return result; }
            result = temp.Substring(0, IndexEnd);   
            return result;
        }
        public List<String> SubStrings(string end_of_start, string start_of_end)
        {
            TempList = new List<String>();
            SubStringsProcess(end_of_start, start_of_end);
            List<String> result = TempList;
            TempList = new();
            return result;
        }
        public List<String> SubStringsRetain(string end_of_start, string start_of_end)
        {
            TempList = new List<String>();
            SubStringsProcess(end_of_start, start_of_end);
            List<String> result = new();
            foreach(String str in TempList)
            {
                result.Add(end_of_start + str + start_of_end);
            }
            return result;
        }
        public void Clear() 
        { 
            SetValue(null);
        }
        public string[] Split(String @string)
        {
            return Str.Split(@string);
        }
        public static String operator +(String? s1, String? s2)
        {
            return new String(s1?.Str + s2?.Str);
        }
        public static String operator +(String? s1, string? s2)
        {
            return new String(s1?.Str + s2);
        }
        public static String operator +(String? s1, char? ch)
        {
            return new String(s1?.Str + ch);
        }
        public static String operator +(char? ch, String? s2)
        {
            return new String(ch + s2);
        }
        public static String operator *(String? s1, int times)
        {
            string re = "";
            for (int i = 0; i < times; i++)
            {
                re += s1?.Str;
            }
            return new String(re);
        }
        public override bool Equals(object? obj)
        {
            return Str == obj?.ToString();
        }
        public static bool operator ==(String? s1, string? s2)
        {
            return s1?.Str == s2;
        }
        public static bool operator !=(String? s1, string? s2)
        {
            return s1?.Str != s2;
        }
        public static String Empty()
        {
            return new String("");
        }
        public static implicit operator String(char ch)
        {
            return new String(ch);
        }
        public static implicit operator String(string? s)
        {
            return new String(s);
        }
        public static implicit operator String(string[]? strings)
        {
            if(strings == null) { return Empty(); }
            String String = "";
            foreach(string s in strings)
            {
                String += s;
            }
            return String;
        }
        public static implicit operator String(int l)
        {
            return new String(l);
        }
        public static implicit operator String(long l)
        {
            return new String(l);
        }
        public static implicit operator String(double l)
        {
            return new String(l);
        }
        public static implicit operator Number(String v)
        {
            return new Number(v.Str);
        }
        public static implicit operator string(String? v)
        {
            return v?.Str??"";
        }
        public static implicit operator double(String? v)
        {
            return Convert.ToInt64(v?.ParseNumber());
        }
        public static implicit operator long(String? v)
        {
            return Convert.ToInt64(v?.ParseNumber());
        }
        public static implicit operator int(String? v)
        {
            return Convert.ToInt32(v?.ParseNumber());
        }
        public override string ToString()
        {
            return TryParseString(Str) ?? "";
        }
    }
}
