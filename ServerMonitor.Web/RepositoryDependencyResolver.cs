using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using SimpleInjector;

namespace ServerMonitor.Web
{
    public class RepositoryDependencyResolver : IDependencyResolver
    {
        private readonly Container container;
        public RepositoryDependencyResolver(Container container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            IServiceProvider provider = this.container;
            return provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.GetAllInstances(serviceType);
        }

        public void Dispose()
        {
        }
    }
}