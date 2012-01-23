using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Document;
using StructureMap;

namespace MvcApplication1
{
    public class MvcApplication : HttpApplication
    {
        private static IDocumentStore _documentStore = new DocumentStore { ConnectionStringName = "RavenDB" }.Initialize();

        public static IDocumentSession DocumentSession { get; private set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ObjectFactory.Initialize(x =>
                                         {
                                             x.Scan(a => a.WithDefaultConventions());
                                             x.For<Func<IDocumentSession>>().Use(() => DocumentSession);
                                         });
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            DocumentSession = _documentStore.OpenSession();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var session = DocumentSession;
            if (session != null)
            {
                session.Dispose();
            }
        }
    }
}