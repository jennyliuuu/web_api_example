using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class OSFWebClient : WebClient
{
    // A static constructor is used to initialize any static data, or to perform a particular action that needs to be performed once only.
    // It is called automatically before the first instance is created or any static members are referenced.
    // https://msdn.microsoft.com/en-us/library/k9x6w0hc(v=vs.110).aspx
    static OSFWebClient()
    {
        Console.WriteLine("Static OSFWebClient() >>>");
        try
        {
            /* Change to use local one. See GetWebRequest().
            // Note that ServicePointManager.ServerCertificateValidationCallback is global setting
            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;
            */
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.Message);
        }
        Console.WriteLine("Static OSFWebClient() <<<");
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

    protected override WebRequest GetWebRequest(Uri address)
    {
        Console.WriteLine("GetWebRequest() >>>");

        HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
        try
        {
            // HttpWebRequest.ServerCertificateValidationCallback is available from .NET 4.5
            request.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;

            // Hidden key to control whether to provide client certificate
            //string osf_client_cert = "1";
            //if (ConfigurationManager.AppSettings["osf_client_cert"] != null)
            //{
            //    osf_client_cert = ConfigurationManager.AppSettings["osf_client_cert"].ToString();
            //}
            if (true)
            {
                // Find OSCE-generated client certificate from store.
                // https://wiki.jarvis.trendmicro.com/pages/viewpage.action?pageId=98765056#OfficeScanProServiceFramework(OSF)-2.4Authentication(Certificatebasedmutualauthentication)
                Console.WriteLine("Adding certificate...");
                X509Store store = new X509Store("OfcOSF", StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindBySubjectName, "OfcOSFWebApp", true /*false*/);
                request.ClientCertificates.Add(certificates[0]);
                store.Close();
            }
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
    public bool RemoteCertificateValidationCallback(
        object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors
    )
    {
        Console.WriteLine("RemoteCertificateValidationCallback() >>>");

        bool result = true;

        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    Console.WriteLine("ChainStatus[{0}].Status: {1}", i, chain.ChainStatus[i].Status);
                    Console.WriteLine("ChainStatus[{0}].StatusInformation: {1}", i, chain.ChainStatus[i].StatusInformation);
                }
            }

            result = (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors) ? true : false;
        }

        Console.WriteLine("RemoteCertificateValidationCallback() <<<");
        return result;
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

            string request = "{\"system_call_id\":\"OSF_SYSCALL_ONREGISTER\",\"system_call_data\":{\"product_id\":\"OSF_IPRODUCT_xxx\",\"data\":{\"on_query_data_url\":\"https://jenny-server3:4343/officescan_iatas/osf/iatas_api/v1/resourcedata\",\"on_command_data_url\":\"https://jenny-server3:4343/officescan_iatas/osf/iatas_api/v1/commandhub\",\"on_notify_url\":\"https://jenny-server3:4343/officescan_iatas/osf/iatas_api/v1/notifyhub\",\"on_log_url\":\"https://jenny-server3:4343/officescan_iatas/osf/iatas_api/v1/loghub\",\"interested_command_ids\":[{\"cmd_id\":\"OSF_ONCOMMAND_MDR_COMMAND\",\"cmd_type\":\"tmik\"},{\"cmd_id\":\"OSF_ONCOMMAND_MDR_COMMAND\",\"cmd_type\":\"collect_file\"},{\"cmd_id\":\"OSF_ONCOMMAND_MDR_COMMAND\",\"cmd_type\":\"attk_assess\"}],\"reverse_proxy_rules\":[\"https://jenny-server3:4343/officescan_iatas/Shared/Assessments/GetConfig.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Assessments/UploadReport.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Assessments/GetSampleUploadList.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Forensics/DownloadTmfk.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Forensics/GetCommand.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Forensics/GetDedupFileList.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Forensics/SetCommandStatus.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Forensics/UploadCollectedFiles.ashx\",\"https://jenny-server3:4343/officescan_iatas/Shared/Forensics/UploadTmfkReport.ashx\"],\"server_certificate_pem\":\"\"}}}";

            Console.WriteLine("request: {0}", request);
            string response = webClient.UploadString("https://jenny-server3:4343/officescan/osfwebapp/api/v1/SystemCall", "POST", request);
            Console.WriteLine("response: {0}", response);
        }

    }
}