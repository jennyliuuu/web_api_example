using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace mac_agent_int
{
    class Program
    {
        static void Main(string[] args)
        {
            //===
            SystemCallData sysCallData = new SystemCallData();
            Data _data = new Data();

            _data.on_query_data_url = "(on_query)www.jenny.com.tw";
            _data.on_command_data_url = "(on_command)www.jenny.com.tw";

            sysCallData.product_id = "OSF_IPRODUCT_IES";
            sysCallData.data = _data;
            
            string strRequestDataJson = null;
            object objRequestDataJson = null;

            strRequestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(sysCallData);
            objRequestDataJson = Newtonsoft.Json.JsonConvert.DeserializeObject(strRequestDataJson);
            //===

            //===
            SystemCallData sysCallData2 = new SystemCallData();
            Data _data2 = new Data();

            _data2.on_query_data_url = "(on_query)www.jenny.com.tw";
            _data2.on_command_data_url = "(on_command)www.jenny.com.tw";

            sysCallData2.product_id = "OSF_IPRODUCT_IAC";
            sysCallData2.data = _data2;

            string strRequestDataJson2 = null;
            object objRequestDataJson2 = null;

            strRequestDataJson2 = Newtonsoft.Json.JsonConvert.SerializeObject(sysCallData2);
            objRequestDataJson2 = Newtonsoft.Json.JsonConvert.DeserializeObject(strRequestDataJson2);
            //===

            Console.WriteLine("requestSystemCall.system_call_id: {0}\n", sysCallData.product_id);
            Console.WriteLine("requestSystemCall.system_call_id: {0}\n", sysCallData2.product_id);

            //=====================================================================================================================================
            Console.WriteLine("====================================================================================================================");
            
            Class1.SysCall_OnRegister("OSF_SYSCALL_ONREGISTER", objRequestDataJson);
            Console.WriteLine("=== Get_ProductProfile(): {0}\n", Class2.Get_ProductProfile("OSF_IPRODUCT_IES", "on_query_data_url"));

            //Class1.SysCall_OnRegister_test();
            //Console.WriteLine("=== Get_ProductProfile(): {0}\n", Class2.Get_ProductProfile("OSF_IPRODUCT_IXX"));


            
            Class1.SysCall_OnRegister("OSF_SYSCALL_ONREGISTER", objRequestDataJson2);
            Console.WriteLine("=== Get_ProductProfile(): {0}\n", Class2.Get_ProductProfile("OSF_IPRODUCT_IAC", "on_command_data_url"));
            
            /*
            Console.WriteLine("=== Get_ProductProfile(): {0}\n", Class2.Get_ProductProfile("OSF_IPRODUCT_IAC", "on_notify_url"));
            */
            Console.ReadLine();

        }
    }
}
