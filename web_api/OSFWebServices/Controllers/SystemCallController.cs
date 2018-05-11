using libOSFData;
using OSFWebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace OSF_test.Controllers
{

    public class SystemCallController : ApiController
    {
        [HttpPost]
        public string Post([FromBody]OSFRequestSystemCall SysCall)
        {
            // =====================================================================
            // ===                         get cookies                          ====
            // =====================================================================

            //  another method
            /*
            UnicodeEncoding unicode = new UnicodeEncoding();
            byte[] encodedBytes = unicode.GetBytes(SysCall.system_call_data.cookie);
            string return_code = Encoding.Unicode.GetString(encodedBytes);
            return return_code;
            */

            string sessionId = Request.Properties[SecMsgHandler.test] as string;
            string desessionId = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(sessionId));
            return System.Web.HttpUtility.UrlDecode(desessionId);     //need decode!

            // =====================================================================
            // ===                                                              ====
            // =====================================================================

            
        }

    }
}
