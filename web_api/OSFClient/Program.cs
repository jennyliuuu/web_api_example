using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Http;
using System.Web;

namespace OSFClient
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
    public class RootObject
    {
        public string system_call_id { get; set; }
        public SystemCallData system_call_data { get; set; }
    }


    public class OSFWebClient : WebClient
    {
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public OSFWebClient()
        {
            Console.WriteLine("OSFWebClient() >>>");
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            Console.WriteLine("OSFWebClient() <<<");
        }
        static void Main(string[] args)
        {
            using (OSFWebClient webClient = new OSFWebClient())
            {
                // ======================================== prepare json data ======================================== 
                RootObject root = new RootObject();
                SystemCallData systemCallData = new SystemCallData();
                //systemCallData.product_id = Encoding.UTF8.GetString(Encoding.Unicode.GetBytes("許工蓋"));
                //byte[] bytes = Encoding.Default.GetBytes("許工蓋");
                
                systemCallData.product_id = "iprodict";
                systemCallData.cookie = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes("session-id=aabbcc@; session-id2=12.56"));

                /*  another method
                UTF8Encoding utf8 = new UTF8Encoding();
                string unicodeString = "session-id=abc; session-id2=aaa";
                byte[] encodedBytes = utf8.GetBytes(unicodeString);
                systemCallData.cookie = Encoding.UTF8.GetString(encodedBytes);
                */

                root.system_call_id = "jenny";
                root.system_call_data = systemCallData;

                string strRequestDataJson = null;
                strRequestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(root);
                Console.WriteLine("systemCallData.product_id: {0}", strRequestDataJson);
                // ======================================== prepare json data ======================================== 



                // ======================================== set http header ======================================== 
                webClient.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflat");
                webClient.Headers.Set(HttpRequestHeader.ContentType, "application/json; charset=utf-8");

                //string str = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes("session-id=aabbcc//; session-id2=zzz"));
                //string str2 = Encoding.UTF8.GetString(Encoding.GetEncoding("big5").GetBytes("session-id2=我是big5"));
                
                string strcookie = "session-id=aabbcc@@~~; session-id2=12.56";
                string utf8strcookie = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(strcookie));

                //Console.WriteLine("cookie: {0}", strcookie);
                webClient.Headers.Add(HttpRequestHeader.Cookie, utf8strcookie);

                //byte[] bytecookie = Encoding.UTF8.GetBytes(str);

                //string strcookie = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(str));
                //string cookie = ByteArrayToString(Encoding.GetEncoding("utf-8").GetBytes("session-id=abc; session-id2=aaa"));
                //string cookie = Encoding.UTF8.GetString(ByteArrayToString(bcookie));
                //string cookie = HttpContext.Current.Server.UrlEncode("session-id=abc/aaaa; ");
                //webClient.Headers.Add(HttpRequestHeader.Cookie, "session-id2=" + Encoding.UTF8.GetString(Encoding.GetEncoding("big5").GetBytes("我是big5")));

                // ======================================== set http header ======================================== 



                // ======================================== send http request ========================================
                webClient.Encoding = Encoding.UTF8;
                string response = webClient.UploadString("http://localhost:43754/api/systemcall", "POST", strRequestDataJson);
                Console.WriteLine("response: {0}", response);
                // ======================================== send http request ========================================

                Console.ReadLine();
            }

        }
    }
}
