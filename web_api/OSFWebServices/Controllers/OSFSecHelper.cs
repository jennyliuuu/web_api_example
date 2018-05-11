using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OSFWebServices
{
    public class SecMsgHandler : DelegatingHandler
    {
        public static string SessionIdToken = "session-id";
        public static string test = "test";
        protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // =====================================================================
            // ===                         get cookies                          ====
            // =====================================================================
            string sessionId;

            // Try to get the session ID from the request; otherwise create a new ID.
            //var cookie = request.Headers.GetCookies(SessionIdToken).FirstOrDefault();
            CookieHeaderValue cookie = request.Headers.GetCookies().FirstOrDefault();
            
            if (cookie == null)
            {
                sessionId = "aaaa";
            }
            else
            {
                sessionId = cookie[SessionIdToken].Value;
                // Store the session ID in the request property bag.
                request.Properties[SessionIdToken] = sessionId;
                request.Properties[test] = cookie.ToString();
                
            }

            // =====================================================================
            // ===                                                              ====
            // =====================================================================

            
            // Continue processing the HTTP request.
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}