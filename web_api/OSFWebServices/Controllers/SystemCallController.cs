using libOSFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OSF_test.Controllers
{

    public class SystemCallController : ApiController
    {
        [HttpPost]
        public string Post([FromBody]OSFRequestSystemCall SysCall)
        {
            return SysCall.system_call_id;
        }
    }
}
