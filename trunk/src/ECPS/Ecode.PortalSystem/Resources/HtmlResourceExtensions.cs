using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecode.PortalSystem.Utils;

namespace Ecode.PortalSystem.Resources
{
	public class HtmlResourceExtensions
	{
		public static string NativeRes(string classKey, string resourceKey)
		{
			return StaticResUtil.GetResourceString(classKey, resourceKey);
		}
	}
}
