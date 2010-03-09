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
			Url = url;
			Constraints = constraints;
			RouteHandler = routeHandler;
		}

		public string Url { get; set; }

		public RouteValueDictionary Constraints { get; set; }

		public IRouteHandler RouteHandler { get; set; }

		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			string host = httpContext.Request.Url.Host;
			int port = httpContext.Request.Url.Port;
			bool isSecureConnection = httpContext.Request.IsSecureConnection;

			string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
			//PortalManager.GetPortal(host, port).DefaultController

			FieldInfo fi = GetType().BaseType.GetField("_parsedRoute", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField);
			MethodInfo mi = fi.FieldType.GetMethod("Match", new Type[] { typeof(string), typeof(RouteValueDictionary) });
			var values = (RouteValueDictionary)mi.Invoke(fi.GetValue(this), new object[] { virtualPath, new RouteValueDictionary(new { controller = "Home", action = "Index", id = "" }) });

			if (values == null)
			{
				return null;
			}
			RouteData data = new RouteData(this, this.RouteHandler);

			foreach (KeyValuePair<string, object> pair in values)
			{
				data.Values.Add(pair.Key, pair.Value);
			}
			return data;
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			FieldInfo fi = GetType().BaseType.GetField("_parsedRoute", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField);
			MethodInfo mi = fi.FieldType.GetMethod("Bind", new Type[] { typeof(RouteValueDictionary), typeof(RouteValueDictionary), typeof(RouteValueDictionary), typeof(RouteValueDictionary) });
			object url = mi.Invoke(fi.GetValue(this), new object[] { requestContext.RouteData.Values, values, new RouteValueDictionary(new { controller = "Home", action = "Index", id = "" }), this.Constraints });
			if (url == null)
			{
				return null;
			}

			string host = requestContext.HttpContext.Request.Url.Host;
			int port = requestContext.HttpContext.Request.Url.Port;
			bool isSecureConnection = requestContext.HttpContext.Request.IsSecureConnection;
			//requestContext.RouteData.Values["

			//GetPortalAliasByController()
			string vp = (string)url.GetType().GetProperty("Url").GetValue(url, null);
			VirtualPathData data = new VirtualPathData(this, vp);

			return data;
		}
	}
}
