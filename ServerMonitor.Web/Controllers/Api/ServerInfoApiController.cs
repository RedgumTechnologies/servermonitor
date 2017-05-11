using ServerMonitor.Core;
using ServerMonitor.Web.Controllers.Base;
using ServerMonitor.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerMonitor.Web.Controllers.Api
{
    public class ServerInfoApiController : RepositoryApiControllerBase
    {
        //ctors
        public ServerInfoApiController(ICacheRepository repository)
            : base(repository)
        {
            // Nowt here to see
        }

        [HttpPost]
        public HttpResponseMessage Info(ServerInfo info)
        {

            HttpResponseMessage lResult = default(HttpResponseMessage);
            bool suppressErrors = false;
            try
            {
                // load the settings object
                var lServerSettingsResult = Repository.GetSystemSettings();
                if (lServerSettingsResult.StatusCode != RepositoryStatusCode.OK)
                {
                    lResult = Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                else
                {
                    //Process the Server Info
                    IRepositoryResult result = Repository.UpdateServerInfo(info, lServerSettingsResult.Result);
                    if (result.StatusCode == RepositoryStatusCode.OK)
                    {
                        lResult = Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        //Failed the insert into the db
                        lResult = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
            }
            catch (Exception ex)
            {

                if (suppressErrors)
                {
                    lResult = Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    lResult = Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                this.ProcessException(ex);

            }
            return lResult;
        }

        [HttpGet]
        public HttpResponseMessage List()
        {
            HttpResponseMessage lResult = default(HttpResponseMessage);
            bool suppressErrors = false;
            try
            {
                //Process the Server Info
                IRepositoryResult<List<ServerInfo>> result = Repository.ListServerInfo();
                if (result.StatusCode == RepositoryStatusCode.OK)
                {
                    // Process the data for the object here

                    lResult = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    //Failed to retrieve the list
                    lResult = Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {

                if (suppressErrors)
                {
                    lResult = Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    lResult = Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                this.ProcessException(ex);

            }
            return lResult;
        }

    }
}
