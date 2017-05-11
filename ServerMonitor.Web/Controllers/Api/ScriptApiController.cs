using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Net.Http.Headers;
//using ServerMonitor.Core;
//using ServerMonitor.Web.Controllers.Base;
//using ServerMonitor.Web.Repository;

namespace ServerMonitor.Web.Controllers.Api
{
    public class ScriptApiController : ServerMonitor.Web.Controllers.Base.RepositoryApiControllerBase
    {
        //ctors
        public ScriptApiController(ServerMonitor.Core.ICacheRepository repository)
            : base(repository)
        {
            // Nowt here to see
        }

        [HttpGet]
        public HttpResponseMessage GetSystemStatusScript()
        {
            var lFilePath = "~/App_Data/Get-SystemStatus.ps1";

            var lResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var foo = HttpContext.Current.Server.MapPath(lFilePath);
            var lstream = new FileStream(foo, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            lResponse.Content = new StreamContent(lstream);
            lResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return lResponse;
        }

        [HttpGet]
        public HttpResponseMessage GetWindowsUpdateScript()
        {
            var lFilePath = "~/App_Data/Get-WindowsUpdateStatus.ps1";

            var lResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var foo = HttpContext.Current.Server.MapPath(lFilePath);
            var lstream = new FileStream(foo, FileMode.Open, FileAccess.Read, FileShare.Read);

            lResponse.Content = new StreamContent(lstream);
            lResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return lResponse;
        }

        [HttpGet]
        public HttpResponseMessage GetUpdateScript()
        {
            var lFilePath = "~/App_Data/Update-SystemStatus.ps1";

            var lResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var foo = HttpContext.Current.Server.MapPath(lFilePath);
            var lstream = new FileStream(foo, FileMode.Open, FileAccess.Read, FileShare.Read);

            lResponse.Content = new StreamContent(lstream);
            lResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return lResponse;
        }

        [HttpGet]
        public HttpResponseMessage  GetInstallScripts()
        {
            var lFilePath = "~/App_Data/Install-ScriptsAndScheduledTask.ps1";

            var lResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var foo = HttpContext.Current.Server.MapPath(lFilePath);
            var lstream = new FileStream(foo, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            lResponse.Content = new StreamContent(lstream);
            lResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return lResponse;
        }


       
    }
}
