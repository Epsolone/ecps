using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Ecode.PortalSystem.Mvc;

namespace Ecode.PortalSystem.Extensions
{
	public static class RouteCollectionExtensions
	{
		public static Route PortalableMapRoute(this RouteCollection routes, string name, string url)
		{
			return routes.PortalableMapRoute(name, url, null, null);
		}

		public static Route PortalableMapRoute(this RouteCollection routes, string name, string url, object defaults)
		{
			return routes.PortalableMapRoute(name, url, defaults, null);
		}

		public static Route PortalableMapRoute(this RouteCollection routes, string name, string url, string[] namespaces)
		{
			return routes.PortalableMapRoute(name, url, null, null, namespaces);
		}

		public static Route PortalableMapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
		{
			return routes.PortalableMapRoute(name, url, defaults, constraints, null);
		}

		public static Route PortalableMapRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
		{
			return routes.PortalableMapRoute(name, url, defaults, null, namespaces);
		}

		public static Route PortalableMapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			Route item = new PortalableRoute(url, new MvcRouteHandler())
			{
				Defaults = new RouteValueDictionary(defaults),
				Constraints = new RouteValueDictionary(constraints),
				DataTokens = new RouteValueDictionary()
			};
			if ((namespaces != null) && (namespaces.Length > 0))
			{
				item.DataTokens["Namespaces"] = namespaces;
			}

			routes.Add(name, item);
			return item;
		}
    

	}
}
