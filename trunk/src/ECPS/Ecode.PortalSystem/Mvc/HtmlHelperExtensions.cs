using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Ecode.PortalSystem.Portals;

namespace Ecode.PortalSystem.Mvc
{
	public static class HtmlHelperExtensions
	{

		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName)
		{
			return ActionLink(htmlHelper, linkText, actionName, controllerName, areaName, new RouteValueDictionary(), new RouteValueDictionary());
		}

		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, RouteValueDictionary routeValues)
		{
			return ActionLink(htmlHelper, linkText, actionName, controllerName, areaName, routeValues, new RouteValueDictionary());
		}

		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, object routeValues)
		{
			return ActionLink(htmlHelper, linkText, actionName, controllerName, areaName, new RouteValueDictionary(routeValues), new RouteValueDictionary());
		}

		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, object routeValues, object htmlAttributes)
		{
			return ActionLink(htmlHelper, linkText, actionName, controllerName, areaName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
		}

		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (!string.IsNullOrEmpty(areaName))
				routeValues.Add("area", areaName);
			
			return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, routeValues, htmlAttributes));
		}

	}
}
