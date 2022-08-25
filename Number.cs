using System.Runtime.InteropServices;

namespace MyLib
{
    public class Number
    {
        public double? Value { get; private set; }
        public bool IsNull { get; private set; }
        public ChangeLogger IsChanged { get; set; }
        private void CheckNull()
        {
            IsNull = Value == null;
        }
        public void Set(double? value)
        {
            if (Value != value)
            {
                Value = value;
                IsChanged = true;
            }
            else
            {
                IsChanged = false;
            }
            CheckNull();
        }
        public Number()
        {
            IsChanged = false;
            IsNull = true;
        }
        public Number(double? value)
        {
            IsChanged = false;
            IsNull = value == null;
            Value = (long?)value;
        }
        public Number(long? value)
        {
            IsChanged = false;
            IsNull = value == null;
            Value = value;
        }        
        public void UseNotNull(params long[] value)
        {
            var val = LazyConvert.Value(value);
            Set(val);
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
            return new Number(n1.Value - n2);
        }
        public static Number operator -(Number n1, long? n2)
        {
            return new Number(n1.Value - n2);
        }
        public static Number operator *(Number n1, Number n2)
        {
            return new Number(n1.Value * n2);
        }
        public static Number operator *(Number n1, long? n2)
        {
            return new Number(n1.Value * n2);
        }
        public static Number operator /(Number n1, Number n2)
        {
            return new Number(n1.Value / n2);
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

        public static implicit operator Number(long? @long)
        {
            Number n = new Number();
            n.Set(@long);
            return n;
        }
        public static implicit operator Number(double? @double)
        {
            Number n = new Number();
            n.Set(@double);
            return n;
        }
        public static implicit operator Number(int? @int)
        {
            Number n = new Number();
            n.Set(@int);
            return n;
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
    }
}
