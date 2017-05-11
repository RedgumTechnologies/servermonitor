using ServerMonitor.Core;
using ServerMonitor.Web.Controllers.Base;
using ServerMonitor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerMonitor.Web.Controllers
{
    [Authorize]
    public class ServerController : RepositoryControllerBase
    {

         // Constructors.
        public ServerController(ICacheRepository repository)
            : base(repository)
        {

        }

        //
        // GET: /Server/

        public ActionResult Index()
        {
            var model = new ServerListModel();
            var lServerSettingsResult = Repository.GetSystemSettings();
            if (lServerSettingsResult.StatusCode != RepositoryStatusCode.OK)
            {
                ModelState.AddModelError("", ResolveStatusCodeMessage(lServerSettingsResult));
                return View(model);
            }

            // Go get the list of servers
            IRepositoryResult<List<ServerInfo>> lRepoResult = Repository.ListServerInfo();

            if (lRepoResult.StatusCode != RepositoryStatusCode.OK)
            {
                model.StatusMessage = ResolveStatusCodeMessage(lRepoResult);
                return View(model);
            }

            foreach (ServerInfo si in lRepoResult.Result)
            {
                model.Servers.Add(new ServerModel(si, lServerSettingsResult.Result));
            }

            return View(model);
        }


        public ActionResult ViewServer(string serverName)
        {
            var model = new ServerModel();
            var lServerSettingsResult = Repository.GetSystemSettings();
            if (lServerSettingsResult.StatusCode != RepositoryStatusCode.OK)
            {
                ModelState.AddModelError("", ResolveStatusCodeMessage(lServerSettingsResult));
                return View(model);
            }

            if ((serverName != null) && (serverName != String.Empty))
            {
                // Go find the tag
                IRepositoryResult<ServerInfo> lRepoResult = Repository.GetServerInfo(serverName);
                if (lRepoResult.StatusCode != RepositoryStatusCode.OK)
                {
                    ModelState.AddModelError("", ResolveStatusCodeMessage(lRepoResult));
                    return View(model);
                }
                model = new ServerModel(lRepoResult.Result, lServerSettingsResult.Result );
            }
            
            return View(model);
        }

    }
}
