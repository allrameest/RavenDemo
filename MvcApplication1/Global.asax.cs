using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Document;

namespace MvcApplication1
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            var container = BuildContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterInstance(InitializeStore());
            builder.Register(c => c.Resolve<IDocumentStore>().OpenSession()).InstancePerLifetimeScope();
            return builder.Build();
        }

        private static IDocumentStore InitializeStore()
        {
            return new DocumentStore
                {
                    ConnectionStringName = "RavenDB"
                }.Initialize();
        }
    }
}