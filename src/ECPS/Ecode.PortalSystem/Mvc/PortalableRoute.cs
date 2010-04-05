using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Reflection;
using System.Web;

namespace Ecode.PortalSystem.Mvc
{
	public class PortalableRoute : Route
	{

		public PortalableRoute(string url, IRouteHandler routeHandler)
			: this(url, null, routeHandler)
		{
			//Route
		}

		public PortalableRoute(string url, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: base(url, null, constraints, routeHandler)
		{
			//Url = url;
			//Constraints = constraints;
			//RouteHandler = routeHandler;
		}

		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			RouteData data = base.GetRouteData(httpContext);
			PortalUrlUtil.FillRouteData(data);
			return data;
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			VirtualPathData data = base.GetVirtualPath(requestContext, values);
			if (data == null)
				return null;
			PortalUrlUtil.FillVirtualPath(base.GetVirtualPath(requestContext, values));
			return data;
		}
	}
}
