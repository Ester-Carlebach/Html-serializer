using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2
{
    public class Helper
    {
        public static readonly Helper _instance=new Helper();
        public static Helper Instance=>_instance;
        public string[] MessageList { get; set; }
        private Helper()
        {
            
        }
    }
} 
