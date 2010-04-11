using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ecode.PortalSystem.Handlers
{
	public class PortalableRobotHandler : IHttpHandler
	{

		#region IHttpHandler 成员

		public bool IsReusable
		{
			get { return false; }
		}

		public void ProcessRequest(HttpContext context)
		{
			
		}

		#endregion

	}
}
