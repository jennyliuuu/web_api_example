using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace test
{
    /* 
           class Program
        {
            public String Text1 { get; set; }
            //宣告自定義的整數變數(屬性)
            public int number1 { get; set; }
            //初始化自定義類別
            public Program()
            {

            }

            static void Main(string[] args)
            {

                Program mc = new Program();
                mc.Text1 = "Jenny~";
                String text = mc.Text1;//取得文字
                mc.number1 = 10;//設定整數
                int number = mc.number1;//取得整數
                Console.WriteLine("Text1 = {0}, number = {1}", text, number);

                Func<string, string> convert = s => s.ToUpper();

                string name = "Dakota";
                Console.WriteLine(convert(name));

            }
        }
    */

    /*
    class Program
    {
        static void Main(string[] args)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            X509Certificate2 certificate = null;

            var certificateName = "CN=tw-jennyliu.client.tw.trendnet.org";
            store.Open(OpenFlags.ReadOnly);
            foreach (var s in store.Certificates)
            {
                if (s.SubjectName.Name == certificateName)
                {
                    certificate = s;
                    Console.WriteLine("Subject Name : " + certificate.SubjectName.Name);
                    Console.WriteLine("\nhas Private Key :  " + certificate.HasPrivateKey);
                    Console.WriteLine("\nvaild : {0} - {1} ", certificate.NotBefore, certificate.NotAfter);
                    Console.WriteLine("\nSerialNumber : " + certificate.SerialNumber);
                    Console.WriteLine("\nVersion : " + certificate.Version);
                    Console.WriteLine("\nAlgorithm : " + certificate.SignatureAlgorithm.FriendlyName);
                    Console.WriteLine("\nPrivate Key : " + certificate.PrivateKey.ToXmlString(false));
                    Console.WriteLine("\nPublic Key : " + certificate.PublicKey.Key.ToXmlString(false));
                    break;
                }
            }
            store.Close();
        }
    }
    */

    class Program
    {
        static void Main(string[] args)
        {
            //Create new X509 store from local certificate store.
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadWrite);
            X509Chain ch = new X509Chain();
            X509Certificate2 certificate = null;

            var certificateName = "CN=currentuser3";
            foreach (var s in store.Certificates)
            {
                if (s.SubjectName.Name == certificateName)
                {
                    certificate = s;
                    ch.Build(certificate);
                }
            }

            Console.WriteLine("Chain Information");
            ch.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            Console.WriteLine("Chain revocation flag: {0}", ch.ChainPolicy.RevocationFlag);
            Console.WriteLine("Chain revocation mode: {0}", ch.ChainPolicy.RevocationMode);
            Console.WriteLine("Chain verification flag: {0}", ch.ChainPolicy.VerificationFlags);
            Console.WriteLine("Chain verification time: {0}", ch.ChainPolicy.VerificationTime);
            Console.WriteLine("Chain status length: {0}", ch.ChainStatus.Length);
            Console.WriteLine("Chain application policy count: {0}", ch.ChainPolicy.ApplicationPolicy.Count);
            Console.WriteLine("Chain certificate policy count: {0} {1}", ch.ChainPolicy.CertificatePolicy.Count, Environment.NewLine);
            //Output chain element information.
            Console.WriteLine("Chain Element Information");
            Console.WriteLine("Number of chain elements: {0}", ch.ChainElements.Count);
            Console.WriteLine("Chain elements synchronized? {0} {1}", ch.ChainElements.IsSynchronized, Environment.NewLine);

            foreach (X509ChainElement element in ch.ChainElements)
            {
                Console.WriteLine("Element SubjectName: {0}", element.Certificate.SubjectName.Name);
                Console.WriteLine("Element issuer name: {0}", element.Certificate.Issuer);
                Console.WriteLine("Element certificate valid until: {0}", element.Certificate.NotAfter);
                Console.WriteLine("Element certificate is valid: {0}", element.Certificate.Verify());
                Console.WriteLine("Element error status length: {0}", element.ChainElementStatus.Length);
                Console.WriteLine("Element information: {0}", element.Information);
                Console.WriteLine("Number of element extensions: {0}{1}", element.Certificate.Extensions.Count, Environment.NewLine);

                if (ch.ChainStatus.Length > 1)
                {
                    for (int index = 0; index < element.ChainElementStatus.Length; index++)
                    {
                        Console.WriteLine(element.ChainElementStatus[index].Status);
                        Console.WriteLine(element.ChainElementStatus[index].StatusInformation);
                    }
                }
            }
            store.Close();
        }
    }
}
