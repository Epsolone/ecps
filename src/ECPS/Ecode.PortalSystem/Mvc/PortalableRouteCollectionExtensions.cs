using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Ecode.PortalSystem.Mvc
{
	public static class PortalableRouteCollectionExtensions
	{
		public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
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
