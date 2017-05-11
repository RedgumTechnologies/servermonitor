using ServerMonitor.Core;
using ServerMonitor.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ServerMonitor.Web.Controllers.Base
{
    public abstract class RepositoryApiControllerBase : ApiController
    {

        public RepositoryApiControllerBase(ICacheRepository repository)
            : base()
        {
            this._Repository = repository;
        }

        private ICacheRepository _Repository;
        public ICacheRepository Repository
        {
            get { return _Repository; }
        }

        protected void ProcessException(Exception ex)
        {
            //Do something useful with the exception
        }
    }
}