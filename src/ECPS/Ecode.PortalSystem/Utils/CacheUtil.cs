using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;

namespace Ecode.PortalSystem.Utils
{
	internal class CacheUtil
	{
		internal static object LockerInternal = new object();
		internal static Cache CacheInternal
		{
			get
			{
				return HttpContext.Current.Cache;
			}
		}

	}
}
