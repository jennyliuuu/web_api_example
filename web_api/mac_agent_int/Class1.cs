using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mac_agent_int
{
    
    public class Data
    {
        public string on_query_data_url { get; set; }
        public string on_command_data_url { get; set; }
    }
    public class SystemCallData
    {
        public string product_id { get; set; }
        public object data { get; set; }
    }
    /*
    public class RequestSystemCall
    {
        public string system_call_id { get; set; }
        public object system_call_data { get; set; }
    }
    */
    public class Class1
    {
        static Class1()
        {
            system_call_data = new List<SystemCallData>();
            data = new List<string>();
            data.Add("on_query_data_url");
            data.Add("on_command_data_url");
            data.Add("on_notify_url");
            data.Add("on_log_url");
            data.Add("interested_command_ids");
            data.Add("reverse_proxy_rules");
            data.Add("server_certificate_pem");
        }

        public static int SysCall_OnRegister(string SysCallCommandID, object RequestDataJson)
        {
            return Set_ProductProfile(SysCallCommandID, RequestDataJson);
        }

        /*
        public static void SysCall_OnRegister_test()
        {
            Console.WriteLine("SysCall_OnRegister_test()");
        }
        */

        public static int Set_ProductProfile(string SysCallCommandID, object RequestDataJson)
        {
            //RequestSystemCall requestSystemCall2 = new RequestSystemCall();
            SystemCallData sysCallData2 = new SystemCallData();
            sysCallData2.product_id = ((dynamic)RequestDataJson).product_id;
            sysCallData2.data = ((dynamic)RequestDataJson).data;
            Console.WriteLine("requestSystemCall2.system_call_id: {0}\nrequestSystemCall2.system_call_data: {1}", sysCallData2.product_id, sysCallData2.data);

            system_call_data.Add(sysCallData2);
            return 0;
        }
        public static string _Get_ProductProfile(string productName, string dataName)
        {
            Console.WriteLine("products.Count: {0}\n", system_call_data.Count);
            SystemCallData sysData = system_call_data.Find(x => x.product_id == productName);   //need error handling
            string dataName1 = data.Find(x => x == dataName);

            if (dataName1 == "on_query_data_url")
                return ((dynamic)sysData).data.on_query_data_url;
            else if (dataName1 == "on_command_data_url")
                return ((dynamic)sysData).data.on_command_data_url;
            else
                return "nono~";
        }
        public static List<SystemCallData> system_call_data;
        public static List<string> data;
    }
}
