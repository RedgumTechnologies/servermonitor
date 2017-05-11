using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Web.Routing;
using ServerMonitor.Core;

namespace ServerMonitor.Web.Factories
{
    public class RepositoryControllerFactory : DefaultControllerFactory
    {
        private readonly ICacheRepository _Repo;

        public RepositoryControllerFactory(ICacheRepository repo)
            : base()
        {
            _Repo = repo;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController controller = Activator.CreateInstance(controllerType, new[] { _Repo }) as Controller;
            return controller;
        }
    }
}