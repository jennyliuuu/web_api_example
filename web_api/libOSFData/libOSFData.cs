using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libOSFData
{
    public class Data
    {
    }
    public class SystemCallData
    {
        public string product_id { get; set; }
        public Data data { get; set; }
        public string cookie { get; set; }
    }
    public class OSFRequestSystemCall
    {
        public string system_call_id { get; set; }
        public SystemCallData system_call_data { get; set; }
    }
}
