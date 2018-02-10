using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hello_web_api_client
{
    class Program
    {
        static void Main(string[] args)
        {
            var greetingServiceAddress =
            new Uri("http://localhost:1884/api/greeting/Testgreeting1");
            var client = new HttpClient();
            var result = client.GetAsync(greetingServiceAddress).Result;
            var greeting = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(greeting);
            Console.ReadKey();
        }
    }
}
