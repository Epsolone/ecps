using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Ecode.PortalSystem.Mvc
{
	public class PortalableController : System.Web.Mvc.Controller
	{
		protected override void Initialize(RequestContext requestContext)
		{
			this.ControllerContext = new System.Web.Mvc.ControllerContext(requestContext, this);
			this.Url = new System.Web.Mvc.UrlHelper(requestContext);
		}

	}
}
