using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mac_agent_int
{
    class Class2
    {
        public static string Get_ProductProfile(string productName, string dataName)
        {
            return Class1._Get_ProductProfile(productName, dataName);
        }
    }
}
