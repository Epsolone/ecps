using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecode.PortalSystem.Portals;
using System.Web.Routing;
using System.Web;

namespace Ecode.PortalSystem.Mvc
{
	public static class PortalUrlUtil
	{

		public static void FillVirtualPath(VirtualPathData virtualPathData)
		{
			HttpContext context = HttpContext.Current;
			if (context == null)
				throw new ArgumentNullException("HttpContext");
			HttpRequest request = context.Request;
			if (request == null)
				throw new ArgumentNullException("Request");

			string virtualPath = virtualPathData.VirtualPath;

			if (virtualPath == null)
				throw new ArgumentNullException("virtualPath");

			if (virtualPath == string.Empty)
				return;
			if (VirtualPathUtility.IsAppRelative(virtualPath))
				virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);

			PortalAlias alias = PortalManager.GetPortalAlias(request.Url.Host, request.Url.Port, request.IsSecureConnection);
			virtualPath = alias.ToString() + virtualPath;
			virtualPathData.VirtualPath = virtualPath;
		}

		public static void FillRouteData(RouteData data)
		{
			HttpContext context = HttpContext.Current;
			if (context == null)
				throw new ArgumentNullException("HttpContext");
			HttpRequest request = context.Request;
			if (request == null)
				throw new ArgumentNullException("Request");

			PortalAlias alias = PortalManager.GetPortalAlias(request.Url.Host, request.Url.Port, request.IsSecureConnection);

			//data.DataTokens["area"] = alias.Areas[0];
		}

	}
}
