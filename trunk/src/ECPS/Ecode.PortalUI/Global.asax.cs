using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using Ecode.PortalSystem.Portals;
using Ecode.PortalSystem.Mvc;
using Ecode.PortalSystem.Extensions;

namespace Ecode.PortalUI
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.PortalableMapRoute(
			    "Default",                                              // Route name
			    "{controller}/{action}/{id}",                           // URL with parameters
			    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
			);


			//RouteBase route = new Route("", null);
			//HttpContextBase context;

			//routes.Add("test", new PortalableRoute("{controller}/{action}/{id}", new MvcRouteHandler()));

		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
		}
	}

}