using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using Ecode.PortalSystem.Wrapper;

namespace Ecode.PortalSystem.Mvc
{
	public class MvcHandler : System.Web.Mvc.MvcHandler
	{
		public MvcHandler(RequestContext requestContext)
			: base(requestContext)
		{
		}
		protected override void ProcessRequest(HttpContext httpContext)
		{
			HttpContextBase base2 = new HttpContextWrapperTweak(httpContext);
			this.ProcessRequest(base2);
		}

		protected override IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
		{
			HttpContextBase base2 = new HttpContextWrapperTweak(httpContext);
			return this.BeginProcessRequest(base2, callback, state);

		}
	}
}
