using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Reflection;
using Ecode.PortalSystem.Wrapper;

namespace Ecode.PortalSystem.Mvc
{
	public class PortalableView : System.Web.Mvc.ViewPage
	{

		public override void RenderView(System.Web.Mvc.ViewContext viewContext)
		{
			viewContext.HttpContext = new HttpContextWrapperTweak(viewContext.HttpContext.ApplicationInstance.Context);
			Type t = typeof(RequestContext);
			PropertyInfo pi = t.GetProperty("HttpContext", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance);
			pi.SetValue(viewContext.RequestContext, viewContext.HttpContext, null);
			base.RenderView(viewContext);
		}

	}
}
