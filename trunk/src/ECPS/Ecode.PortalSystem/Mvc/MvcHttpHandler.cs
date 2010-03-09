using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using Ecode.PortalSystem.Wrapper;

namespace Ecode.PortalSystem.Mvc
{
	public class MvcHttpHandler : System.Web.Mvc.MvcHttpHandler
	{
		// Methods
		protected override void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext)
		{
			if (httpHandler == null)
			{
				throw new ArgumentNullException("httpHandler");
			}
			httpHandler.ProcessRequest(HttpContext.Current);
		}

		protected override IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
		{
			HttpContextBase base2 = new HttpContextWrapperTweak(httpContext);
			return this.BeginProcessRequest(base2, callback, state);
		}

		protected override void ProcessRequest(HttpContext httpContext)
		{
			ProcessRequest(new HttpContextWrapperTweak(httpContext));
		}

	}
}
