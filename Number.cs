using System.Runtime.InteropServices;

namespace MyLib
{
    public class Number
    {
        private static char[] numbers = new char[] {
        '0','1','2','3','4','5','6','7','8','9'
        };
        public double? Value { get; private set; }
        public bool IsNull { get; private set; }
        public String String { get; private set; }
        public string @string { get; private set; }
        public ChangeLogger IsChanged { get; set; }
        public static double? TryParse(object? obj)
        {
            try
            {
                double value = Convert.ToInt64(obj);
                return value;
            }
            catch
            {
                return null;
            }
        }
        public void SetValue(object? obj)
        {
            double? value = TryParse(obj);
            if (Value != value)
            {
                Value = value;
                String = new(Value);
                @string = String.ToString();
                IsChanged = true;
            }
            else
            {
                IsChanged = false;
            }
            IsNull = Value == null;
        }
        public Number()
        {
            String = new String();
            @string = "";
            IsChanged = false;
            IsNull = true;
        }
        public Number(object? obj) : this()
        {
            SetValue(obj);
        }
        public Number(string? str) : this()
        {
            if (str == null)
            {
                SetValue(null);
                return;
            }
            str = str.Trim();
            string[] strs = str.Split('.');
            if (strs.Length > 2)
            {
                SetValue(null);
                return;
            }
            if (strs.Length == 2)
            {
                string r1 = "";
                string r2 = "";
                var s1 = strs[0].Trim();
                var s2 = strs[1].Trim();
                for (int i = 0; i < s1.Length; i++)
                {
                    foreach (char num in numbers)
                    {
                        if (num == s1[i])
                        {
                            r1 += num;
                            break;
                        }
                    }
                }
                for (int i = 0; i < s2.Length; i++)
                {
                    foreach (char num in numbers)
                    {
                        if (num == s2[i])
                        {
                            r2 += num;
                            break;
                        }
                    }
                }
                double d1 = Convert.ToInt64(r1);
                double d2 = Convert.ToInt64(r1);
                d2 = d2 / Math.Pow(10, r2.Length);
                SetValue(d1 + d2);
                return;
            }
            if (strs.Length == 1)
            {
                string r1 = "";
                var s1 = strs[0].Trim();
                for (int i = 0; i < s1.Length; i++)
                {
                    foreach (char num in numbers)
                    {
                        if (num == s1[i])
                        {
                            r1 += num;
                            break;
                        }
                    }
                }
                double d = Convert.ToInt64(r1);
                SetValue(d);
                return;
            }
        }
        public static Number operator +(Number n1, Number n2)
        {
            return new Number(n1.Value + n2.Value);
        }
        public static Number operator +(Number n1, long? n2)
        {
            return new Number(n1.Value + n2);
        }
        public static Number operator -(Number n1, Number n2)
        {
            return new Number(n1.Value - n2.Value);
        }
        public static Number operator -(Number n1, long? n2)
        {
            return new Number(n1.Value - n2);
        }
        public static Number operator *(Number n1, Number n2)
        {
            return new Number(n1.Value * n2.Value);
        }
        public static Number operator *(Number n1, long? n2)
        {
            return new Number(n1.Value * n2);
        }
        public static Number operator /(Number n1, Number n2)
        {
            return new Number(n1.Value / n2.Value);
        }
        public static Number operator /(Number n1, long? n2)
        {
            return new Number(n1.Value / n2);
        }
        public static Number operator ^(Number n1, Number n2)
        {
            return new Number(Math.Pow(n1, n2));
        }
        public static Number operator ^(Number n1, long? n2)
        {
            return new Number(Math.Pow(n1, Convert.ToDouble(n2)));
        }
        public static bool operator ==(Number? n1, double? n2)
        {
            return n1?.Value == n2;
        }
        public static bool operator !=(Number? n1, double? n2)
        {
            return n1?.Value != n2;
        }
        public static implicit operator Number(long? @long)
        {
            return new Number(@long);
        }
        public static implicit operator Number(double? @double)
        {
            return new Number(@double);
        }
        public static implicit operator Number(int? @int)
        {
            return new Number(@int);
        }
        public static implicit operator Number(string? str)
        {
            return new Number(str);
        }
        public static implicit operator int(Number? n)
        {
            if (n == null) { return 0; }
            return Convert.ToInt32(n.Value);
        }
        public static implicit operator double(Number? n)
        {
            if (n == null) { return 0; }
            return Convert.ToDouble(n.Value);
        }
        public static implicit operator long(Number? n)
        {
            if (n == null) { return 0; }
            return Convert.ToInt64(n.Value);
        }
        public static implicit operator string(Number? n)
        {
            if (n == null) { return ""; }
            return n.@string;
        }
        public static implicit operator String(Number? n)
        {
            if (n == null) { return new String(); }
            return n.String;
        }
        public override string ToString()
        {
            return String.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
