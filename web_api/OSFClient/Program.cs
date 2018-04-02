using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace OSFClient
{
    public class OSFWebClient : WebClient
    {
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
                webClient.Encoding = Encoding.UTF8;
                //webClient.Headers.Add("", );
                webClient.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflat");
                webClient.Headers.Set(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
                //webClient.Headers.Set(HttpRequestHeader.ContentLength, request.Length.ToString());

                string request = "{\r\n    \"system_call_id\": \"jenny\",\r\n    \"system_call_data\": {\r\n        \"product_id\": \"jennyyyyy\",\r\n        \"data\": {\r\n        }\r\n    }\r\n}";

                Console.WriteLine("request: {0}", request);
                string response = webClient.UploadString("http://localhost:43754/api/systemcall", "POST", request);
                Console.WriteLine("response: {0}", response);
                Console.ReadLine();
            }

        }
    }
}
