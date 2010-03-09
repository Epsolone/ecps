using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

namespace Ecode.PortalSystem.Mvc
{
	public class MvcRouteHandler : System.Web.Mvc.MvcRouteHandler
	{
		protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
		{

			return new MvcHandler(requestContext);
		}
	}
}
