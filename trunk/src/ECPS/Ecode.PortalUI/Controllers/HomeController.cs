using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecode.PortalSystem.Mvc;

namespace Ecode.PortalUI.Controllers
{
	[HandleError]
	public class HomeController : PortalableController
	{
		public ActionResult Index()
		{
			ViewData["Message"] = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult About()
		{
			return View();
		}

	}
}
