using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Reflection;

namespace Ecode.Web.Security
{
	public static class SecurityUtil
	{
		public static int FormsAuthenticationTimeOut
		{
			get
			{
				FormsAuthentication.Initialize();
				Type t = typeof(FormsAuthentication);
				return (int)t.GetField("_Timeout", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.SetField
					 | BindingFlags.Static).GetValue(null);
			}
			set
			{
				FormsAuthentication.Initialize();
				Type t = typeof(FormsAuthentication);
				t.GetField("_Timeout", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.SetField
					 | BindingFlags.Static).SetValue(null, value);
			}
		}
	}
}
