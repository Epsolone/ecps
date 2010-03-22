using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Caching;
using Ecode.PortalSystem.Utils;
using System.Collections;

namespace Ecode.PortalSystem.Configuration
{

	/// <summary>
	/// system configuration
	/// </summary>
	public class SystemConfiguration : IConfiguration
	{
		private SystemConfiguration()
		{
		}

		private static SystemConfiguration m_Current = new SystemConfiguration();
		/// <summary>
		/// current SystemConfiguation
		/// </summary>
		public static SystemConfiguration Current
		{
			get
			{
				return m_Current;
			}
		}

		/// <summary>
		/// Gets the specified configuration.
		/// </summary>
		/// <typeparam name="T">configuration value type</typeparam>
		/// <param name="configKey">The configuration key.</param>
		/// <returns>The object representing the configuration.</returns>
		public T GetConfig<T>(string configKey)
		{
			return (T)GetConfig(configKey);
		}

		/// <summary>
		/// Gets the specified configuration.
		/// </summary>
		/// <param name="configKey">The configuration key.</param>
		/// <returns>The object representing the configuration.</returns>
		public object GetConfig(string configKey)
		{
			if (CacheUtil.CacheInternal.Get("CONF_SYSTEM") == null)
			{
				Dictionary<string, object> conf = new Dictionary<string, object>();

				DataSet ds = new DataSet();
				string confFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conf\\system.config");
				ds.ReadXml(confFileName);
				DataTable dt = ds.Tables["item"];
				foreach (DataRow dr in dt.Rows)
				{
					conf[dr["name"].ToString()] = dr["data"];
				}
				CacheUtil.CacheInternal.Insert("CONF_SYSTEM", conf, new CacheDependency(confFileName));
			}
			return ((Dictionary<string, object>)CacheUtil.CacheInternal["CONF_SYSTEM"])[configKey];
		}

		#region implement interface IConfiguration

		object IConfiguration.GetConfig(string configKey)
		{
			return GetConfig(configKey);
		}

		void IConfiguration.Init()
		{

		}

		#endregion

	}

}
