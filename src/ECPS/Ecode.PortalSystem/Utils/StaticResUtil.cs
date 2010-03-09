using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Resources;
using System.IO;

namespace Ecode.PortalSystem.Utils
{

	/// <summary>
	/// The class with resource utility.
	/// </summary>
	internal static class StaticResUtil
	{
		private static char[] invalidFileNameChars  = Path.GetInvalidFileNameChars();

		/// <summary>
		/// Get the global resource object.
		/// </summary>
		/// <param name="classKey">The class key(connected by ".").</param>
		/// <param name="resourceKey">The resource key in class.</param>
		/// <param name="culture">The culture of resource.</param>
		/// <returns></returns>
		public static object GetResourceObject(string classKey, string resourceKey, CultureInfo culture)
		{
			if (classKey == null)
				throw new ArgumentNullException("classKey");
			if (classKey.Any(c => invalidFileNameChars.Any(ic => ic == c)))
				throw new ArgumentException("class key must be a valid path name", "classKey");
			ResourceManager resmgr;
			lock (CacheUtil.LockerInternal)
			{
				resmgr = (ResourceManager)CacheUtil.CacheInternal[classKey];
				if (resmgr == null)
				{
					string execDir = AppDomain.CurrentDomain.BaseDirectory;
					string resDir = Path.Combine(execDir, "res");
					int lastDotIndex = classKey.LastIndexOf('.');
					if (lastDotIndex >= 0)
					{
						resDir = Path.Combine(resDir, classKey.Remove(lastDotIndex).Replace('.', Path.DirectorySeparatorChar));
						classKey = classKey.Substring(classKey.IndexOf('.') + 1);
					}
					resmgr = ResourceManager.CreateFileBasedResourceManager(classKey, resDir, null);
					CacheUtil.CacheInternal.Insert(classKey, resmgr);
				}
			}
			return resmgr.GetObject(resourceKey, culture);
		}

		/// <summary>
		/// Get the global resource object.
		/// </summary>
		/// <param name="classKey">The class key(connected by ".").</param>
		/// <param name="resourceKey">The resource key in class.</param>
		/// <returns></returns>
		public static object GetResourceObject(string classKey, string resourceKey)
		{
			return GetResourceObject(classKey, resourceKey, null);
		}

		/// <summary>
		/// Get the global resource string.
		/// </summary>
		/// <param name="classKey">The class key(connected by ".").</param>
		/// <param name="resourceKey">The resource key in class.</param>
		/// <param name="culture">The culture of resource.</param>
		/// <returns></returns>
		public static string GetResourceString(string classKey, string resourceKey, CultureInfo culture)
		{
			return GetResourceObject(classKey, resourceKey, culture) as string;
		}

		/// <summary>
		/// Get the global resource string.
		/// </summary>
		/// <param name="classKey">The class key(connected by ".").</param>
		/// <param name="resourceKey">The resource key in class.</param>
		/// <returns></returns>
		public static string GetResourceString(string classKey, string resourceKey)
		{
			return GetResourceObject(classKey, resourceKey, null) as string;
		}

	}
}
