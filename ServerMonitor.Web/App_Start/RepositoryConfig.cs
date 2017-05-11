using ServerMonitor.Core;
using ServerMonitor.Data;
using ServerMonitor.Web.Factories;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace ServerMonitor.Web
{
    public static class RepositoryConfig
    {
        public static IRepository Register(HttpConfiguration config)
        {
            //Site Repo Injection
            var dataRepo = new SqlDataRepository(Properties.Settings.Default.ConnectionString);
            var cacheRepo = new ServerMonitor.Web.Repository.CacheRepository(dataRepo);
            IControllerFactory factory = new RepositoryControllerFactory(cacheRepo);
            ControllerBuilder.Current.SetControllerFactory(factory);

            //API Repo Injection
            SimpleInjector.Container SimpleInjectContainer = new SimpleInjector.Container();
            //SimpleInjectContainer.Register<IDataRepository>(() => new SqlDataRepository(Properties.Settings.Default.KronologicaDB));
            SimpleInjectContainer.Register<ICacheRepository>(() => new ServerMonitor.Web.Repository.CacheRepository(dataRepo));
            config.DependencyResolver = new RepositoryDependencyResolver(SimpleInjectContainer);

            return cacheRepo;
        }
    }
}