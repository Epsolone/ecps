using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecode.PortalSystem.Mvc
{
	public class PortalableView : System.Web.Mvc.ViewPage
	{
		public override void InitHelpers()
		{
			this.Ajax = new System.Web.Mvc.AjaxHelper<object>(this.ViewContext, this);
			this.Html = new System.Web.Mvc.HtmlHelper<object>(this.ViewContext, this);
			this.Url = new System.Web.Mvc.UrlHelper(this.ViewContext.RequestContext);
		}
	}
}
