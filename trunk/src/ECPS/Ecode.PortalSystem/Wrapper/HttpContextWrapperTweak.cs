using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ecode.PortalSystem.Wrapper
{
	public class HttpContextWrapperTweak : HttpContextWrapper
	{
		private readonly HttpContext _context;

		public HttpContextWrapperTweak(HttpContext httpContext)
			: base(httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			this._context = httpContext;
		}


		public override HttpResponseBase Response
		{
			get
			{
				return new HttpResponseWrapperTweak(this._context.Response);
			}
		}
	}
}
