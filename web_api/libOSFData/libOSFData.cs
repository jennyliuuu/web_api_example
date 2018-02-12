using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libOSFData
{
    public class OSFRequestSystemCall
    {
        public string system_call_id { get; set; }
        public OSFSystemCallData system_call_data { get; set; }
    }
    public class OSFSystemCallData
    {
        public string product_id { get; set; }
        public object data { get; set; }
    }
}
