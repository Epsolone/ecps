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

		public HttpResponseWrapperTweak(HttpResponse httpResponse)
			: base(httpResponse)
		{
		}

		static Regex re_FullUrl = new Regex(@"^[a-zA-Z_0-9]+\:.*$", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
		public override string ApplyAppPathModifier(string virtualPath)
		{
			if (re_FullUrl.IsMatch(virtualPath))
				return virtualPath;
			else
				return base.ApplyAppPathModifier(virtualPath);
		}
	}
}
