// web client do 2-way auth

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


public class Data
{
}
public class SystemCallData
{
    public string product_id { get; set; }
    public Data data { get; set; }
}
public class RootObject
{
    public string system_call_id { get; set; }
    public SystemCallData system_call_data { get; set; }
}
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
    protected override WebRequest GetWebRequest(Uri address)
    {
        Console.WriteLine("GetWebRequest() >>>");

        HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
        try
        {
            //ServicePointManager.
            request.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            Console.WriteLine("Adding certificate...");
            X509Store store = new X509Store("OfcOSF", StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindBySubjectName, "OfcOSFWebApp", true /*false*/);
            request.ClientCertificates.Add(certificates[0]);
            store.Close();
            
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.Message);
            throw;
        }
        Console.WriteLine("GetWebRequest() <<<");
        return request;
    }

    // Define server certificate validation callback so we can check for errors for debugging
    // https://msdn.microsoft.com/en-us/library/system.net.security.remotecertificatevalidationcallback(v=vs.110).aspx
    public static bool ValidateServerCertificate(
        object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors
    )
    {
        Console.WriteLine("RemoteCertificateValidationCallback() >>>");
        Console.WriteLine("server certificate: {0}", certificate);

        X509Store store = new X509Store("TrustedPeople", StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly);

        
        foreach (var s in store.Certificates)
        {
            Console.WriteLine("s.Subject: {0}, ", s.Subject);
            Console.WriteLine("certificate.Subject: {0}, ", certificate.Subject);
            if (s.Subject == certificate.Subject)
            {
                Console.WriteLine("certificate.Subject: {0}, ", certificate.Subject);
                store.Close();
                return true;
            }
        }
        
        //X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindBySubjectName, "OfcOSFWebApp", true /*false*/);

        Console.WriteLine("RemoteCertificateValidationCallback() <<<");
        store.Close();
        return false;
    }
    static void Main(string[] args)
    {
        //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

        //using (OSFWebClient webClient = new OSFWebClient())
        //{
        OSFWebClient webClient = new OSFWebClient();
            // ======================================== prepare json data ======================================== 
            RootObject root = new RootObject();
            SystemCallData systemCallData = new SystemCallData();

            systemCallData.product_id = "iprodict";
            
            root.system_call_id = "jenny";
            root.system_call_data = systemCallData;

            string strRequestDataJson = null;
            strRequestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(root);
            Console.WriteLine("systemCallData.product_id: {0}", strRequestDataJson);
            // ======================================== prepare json data ======================================== 
            
            // ========================================= set http header ========================================= 
            webClient.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflat");
            webClient.Headers.Set(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
            // ========================================= set http header ========================================= 
            
            // ======================================== send http request ========================================
            webClient.Encoding = Encoding.UTF8;
            string urlPath = "https://win-427umrfjf9t:4343/officescan/osfwebapp/api/v1/SystemCall";
            string response = webClient.UploadString(urlPath, "POST", strRequestDataJson);
            Console.WriteLine("response: {0}", response);
            // ======================================== send http request ========================================

            Console.ReadLine();
        //}

    }
}