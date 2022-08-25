using String = MyLib.String;

namespace MyLib
{
    public static class Time
    {
        private static string _NumFormat(int n)
        {
            return n < 10 ? "0" + n : "" + n;
        }
        private static long _DateTimeToLong(DateTime Date)
        {
            var IntType = Date.Year + _NumFormat(Date.Month) + _NumFormat(Date.Day) + _NumFormat(Date.Hour) + _NumFormat(Date.Minute) + _NumFormat(Date.Second);
            return Convert.ToInt64(IntType);
        }
        private static string _LongToStr(long? timeInt)
        {
            string temp = "";
            if (timeInt == null) { timeInt = 10000000000001; }
            for (int i = 0; i < 14; i++)
            {
                long t = (long)timeInt % 10;
                timeInt = timeInt / 10;
                temp = t + temp;
            }
            String s = (String)temp;
            return s.SubString(0, 3) + "-" + s.SubString(4, 5) + "-" + s.SubString(6, 7) + "  "
            + s.SubString(8, 9) + ":" + s.SubString(10, 11) + ":" + s.SubString(12, 13);
        }

        public static long TimeNowInt()
        {
            var Date = DateTime.Now;
            return ToLong(Date);
        }
        public static long ToLong(DateTime Date)
        {
            return _DateTimeToLong(Date);
        }

        public static string ToString(DateTime Date)
        {
            return ToString(_DateTimeToLong(Date));
        }
        public static string ToString(long? timeInt)
        {
            return _LongToStr(timeInt);
        }
        public static string LastEditStr(string fileName)
        {
            return ToString(LastEditLong(fileName));
        }
        public static long LastEditLong(string fileName)
        {
            DateTime dt = new();
            if (Directory.Exists(fileName))
            {                
                dt = File.GetLastWriteTime(fileName);
            }
            return ToLong(dt);
        }
    }
}
