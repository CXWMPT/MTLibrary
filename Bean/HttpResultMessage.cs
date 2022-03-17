using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    public class HttpResultMessage<T>
    {
        public bool status { get; set; }
        public int? code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public int? count { get; set; }
        public int? total { get; set; }
    }
}
