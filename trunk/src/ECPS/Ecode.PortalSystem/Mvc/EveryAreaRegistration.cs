﻿using System.Web.Mvc;

namespace Ecode.PortalUI.Areas
{
	public class EveryAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Secure";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Secure_default",
				"Secure/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}