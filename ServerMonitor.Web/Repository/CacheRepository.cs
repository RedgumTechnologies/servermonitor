using ServerMonitor.Core;
using ServerMonitor.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerMonitor.Web.Repository
{
    public class CacheRepository : ICacheRepository
    {

        #region Constructors and Initialization
        /// <summary>
        /// Initializes a new instance of the Cache Controller - this should pass calls straight through if they don't need any cache goodness. If they do, here is where you do it!
        /// </summary>        
        public CacheRepository(IDataRepository repository)
            : base()
        {
            this._Repository = repository;
            _cachetimeout = Settings.Default.CacheTimeout;
        }
        #endregion

        private TimeSpan _cachetimeout;
        public TimeSpan CacheTimeout { get { return _cachetimeout; } }

        private IDataRepository _Repository;
        public IDataRepository Repository
        {
            get { return _Repository; }
        }

        IRepositoryResult IRepository.UpdateServerInfo(ServerInfo info,SystemSettings settings)
        {
            return Repository.UpdateServerInfo(info,settings);
        }

        IRepositoryResult<ServerInfo> IRepository.GetServerInfo(string serverName)
        {
            return Repository.GetServerInfo(serverName);
        }

        IRepositoryResult<List<ServerInfo>> IRepository.ListServerInfo()
        {
            return Repository.ListServerInfo();
        }

        private static object lockHandleSystemSettings = new object();
        IRepositoryResult<SystemSettings> IRepository.GetSystemSettings()
        {
            var cacheKey = "SystemSettings";
            var lResponse = HttpRuntime.Cache.SetAndGetItemFromCache<IRepositoryResult<SystemSettings>>(
               cacheKey,
               lockHandleSystemSettings,
               c =>
               {
                   var innards = Repository.GetSystemSettings();
                   // only insert innards if the result is good
                   if (innards.StatusCode == RepositoryStatusCode.OK)
                   {
                       c.Insert(
                           cacheKey,
                           innards,
                           null,
                           DateTime.Now.AddMinutes(CacheTimeout.TotalMinutes),
                           System.Web.Caching.Cache.NoSlidingExpiration,
                           System.Web.Caching.CacheItemPriority.Normal,
                           null
                       );
                   }
                   return innards;
               }
           );


            return Repository.GetSystemSettings();
        }
        
        IRepositoryResult ICacheRepository.ClearCache()
        {
            throw new NotImplementedException();
        }
    }
}