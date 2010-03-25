using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Ecode.PortalSystem.Wrapper
{
	class HttpResponseWrapperTweak : HttpResponseWrapper
	{
		HttpResponse m_HttpResponse;
		HttpContextBase m_HttpContext;
		public HttpResponseWrapperTweak(HttpContextBase httpContext, HttpResponse httpResponse)
			: base(httpResponse)
		{
			m_HttpResponse = httpResponse;
			m_HttpContext = httpContext;
		}

		static Regex re_FullUrl = new Regex(@"^\/[a-zA-Z_0-9]+\:.*$", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
		public override string ApplyAppPathModifier(string virtualPath)
		{
			if (re_FullUrl.IsMatch(virtualPath))
				return virtualPath.Substring(1);
			else
				return base.ApplyAppPathModifier(virtualPath);
		}
	}
}
