using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class ChangeLogger
    {
        public bool IsChanged { get; private set; }
        public ChangeLogger() 
        {
            IsChanged = false;
        }
        public ChangeLogger(bool @bool)
        {
            IsChanged = @bool;
        }
        public static implicit operator ChangeLogger(bool @bool)
        {
            return new ChangeLogger(@bool);
        }
        public static implicit operator bool (ChangeLogger l)
        {
            bool temp = l.IsChanged;
            l.IsChanged = false;
            return temp;
        }
    }
}
