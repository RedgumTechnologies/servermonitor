using ServerMonitor.Core;
using ServerMonitor.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerMonitor.Web.Controllers
{
    public class HomeController : RepositoryControllerBase
    {
        public HomeController(ICacheRepository repository)
            : base(repository)
        {
            // Nowt here to see
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index","Server");
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
