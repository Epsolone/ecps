using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Ecode.PortalSystem.Wrapper;
using System.Reflection;

namespace Ecode.PortalSystem.Mvc
{
	public class PortalableController : System.Web.Mvc.Controller
	{
		protected override void Initialize(RequestContext requestContext)
		{
			Type t = typeof(RequestContext);
			PropertyInfo pi = t.GetProperty("HttpContext", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance);
			pi.SetValue(requestContext, new HttpContextWrapperTweak(requestContext.HttpContext.ApplicationInstance.Context), null);
			this.ControllerContext = new System.Web.Mvc.ControllerContext(requestContext, this);
			this.Url = new System.Web.Mvc.UrlHelper(requestContext);
		}

	}
}
