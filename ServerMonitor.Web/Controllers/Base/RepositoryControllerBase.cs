using ServerMonitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Humanizer;

namespace ServerMonitor.Web.Controllers.Base
{
    public abstract class RepositoryControllerBase : Controller
    {

        private ICacheRepository _Repository;
        /// <summary>
        /// Gets the repository to access data on the server.
        /// </summary>
        public ICacheRepository Repository
        {
            get { return _Repository; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryControllerBase"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository to access data on the server.
        /// </param>
        public RepositoryControllerBase(ICacheRepository repository)
            : base()
        {
            this._Repository = repository;
        }

        
        protected void ProcessException(Exception ex)
        {
            //Do something useful with the exception
        }

        /// <summary>
        /// This is a helper method to work out a sensible string from a RepositoryResult
        /// </summary>
        internal string ResolveStatusCodeMessage(IRepositoryResult result)
        {
            // the load failed - report this to the user
            if (result.StatusCode == RepositoryStatusCode.UnknownError)
            {
                // this is a special case, if we have the exception, return it
                if (result.HasException)
                {
                    return result.Exception.Message;
                }
                else
                {
                    return result.StatusCode.ToString().Humanize();
                }
            }
            else
            {
                return result.StatusCode.ToString().Humanize();
            }
        }
    }
}