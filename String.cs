using Microsoft.VisualBasic;

namespace MyLib
{
    public class String
    {
        private string Str { get; set; }
        public int Length { get; private set; }
        private static List<String>? TempList { get; set; }
        private void SubStringsProcess(string end_of_start, string start_of_end)
        {
            TempList = new();
            String temp = new(Str);
            bool IsFind = true;
            for (; IsFind;)
            {
                int IndexStart = temp.Str.IndexOf(end_of_start) + end_of_start.Length;
                if(IndexStart > 0)
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
        //private void SubStringsProcess(string end_of_start, string start_of_end)
        //{
        //    TempList = new();
        //    String temp = new(Str);
        //    bool IsFind = true;
        //    for (; IsFind;)
        //    {
        //        int IndexStart = temp.Str.IndexOf(end_of_start) + end_of_start.Length;
        //        int IndexEnd = temp.Str.IndexOf(start_of_end) - 1;
        //        string find = temp.SubString(IndexStart, IndexEnd);
        //        IsFind = !string.IsNullOrEmpty(find);
        //        temp = new String(temp.SubString(IndexEnd + start_of_end.Length + 1, temp.Length - 1));
        //        if (find.Length > 0) { TempList.Add(find); }
        //    }
        //}
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
        public String()
        {
            Str = string.Empty;
            Length = 0;
        }
        public String(char ch)
        {
            Str = ch.ToString();
            Length = 1;
        }
        public String(string str)
        {
            Str = str;
            Length = str.Length;
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
            String result = new();
            int IndexStart = Str.IndexOf(end_of_start) + end_of_start.Length;
            int IndexEnd = Str.IndexOf(start_of_end) - 1;            
            result = SubString(IndexStart, IndexEnd);
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
        public void Clear() { Str = string.Empty; }
        public static String operator +(String s1, String s2)
        {
            return new String(s1.Str + s2.Str);
        }
        public static String operator +(String s1, string s2)
        {
            return new String(s1.Str + s2);
        }
        public static String operator +(String s1, char ch)
        {
            return new String(s1.Str + ch);
        }
        public static String operator +(char ch, String s2)
        {
            return new String(ch + s2);
        }
        public static String operator *(String s1, int times)
        {
            string re = "";
            for (int i = 0; i < times; i++)
            {
                re += s1.Str;
            }
            return new String(re);
        }

        public static implicit operator String(char ch)
        {
            return new String(ch);
        }
        public static implicit operator String(string s)
        {
            return new String(s);
        }
        public static implicit operator string(String v)
        {
            return v.Str;
        }

        public override string ToString() => Str;
    }
}
