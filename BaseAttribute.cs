using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public interface IAttribute
    {
        public string? Key { get; set; }
        public String? Value { get; set; }
    }
    public class BaseAttribute : IAttribute
    {
        public string? Key { get; set; }
        public String? Value { get; set; }
        public BaseAttribute() { }
        public BaseAttribute(string key, String value)
        {
            Key = key;
            Value = value;
        }
    }
}
