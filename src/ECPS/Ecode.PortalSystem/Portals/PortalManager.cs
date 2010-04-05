using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Ecode.PortalSystem.Portals
{

	public class Portal
	{
		public Portal() { }
		public int PortalID { get; set; }
		public string PortalName { get; set; }
		public string DefaultArea { get; set; }

		public List<PortalAlias> PortalAliases { get; set; }
	}

	public class PortalAlias
	{

		public PortalAlias(string host)
			: this(host, 80, false)
		{
		}

		public PortalAlias(string host, int port)
			: this(host, port, false)
		{
		}
		public PortalAlias(string host, int port, bool isSsl)
		{
			Host = host;
			Port = port;
			IsSsl = isSsl;
		}

		public Portal Portal { get; set; }

		public string Host { get; set; }

		[DefaultValue(80)]
		public int Port
		{
			get { return m_Port; }
			set
			{
				if (value < 0 || value > 65535)
					throw new ArgumentOutOfRangeException("the port range is from 1 to 65535");
				if (value == 0)
					value = 80;
				m_Port = value;
			}
		} private int m_Port = 80;

		[DefaultValue(false)]
		public bool IsSsl
		{
			get { return m_IsSsl; }
			set { m_IsSsl = value; }
		} private bool m_IsSsl = false;

		public string AreaName { get; set; }

		/// <summary>
		/// HTTP 前缀如http,https
		/// </summary>
		public string Protocol
		{
			get
			{
				return IsSsl ? "https" : "http";
			}
			set
			{
				if (value == null)
					m_Protocol = "http";
				else
				{
					value = value.ToLower();
					if (value == "http" || value == "https")
						m_Protocol = value;
					else
						throw new ArgumentOutOfRangeException("value", "Protocol value must be \"http\" or \"https\".");
				}
			}
		} private string m_Protocol = "http";

		/// <summary>
		/// 端口表达式（包含“:”）。
		/// </summary>
		public string PortExpression
		{
			get
			{
				return ((!IsSsl && Port == 80) || (IsSsl && Port == 443)) ? "" : (":" + Port.ToString());
			}
		}

		public override string ToString()
		{
			return string.Format("{0}://{1}{2}", Protocol, Host, PortExpression);
		}

	}

	public static class PortalManager
	{
		public static Portal GetPortal(string host, int port)
		{
			return AllPortals.FirstOrDefault();
		}

		public static PortalAlias GetPortalAlias(Portal portal, string areaName)
		{
			PortalAlias alias = portal.PortalAliases.Where(pa => pa.AreaName.Contains(areaName)).FirstOrDefault();
			if (alias == null)
				alias = portal.PortalAliases.Where(pa => pa.AreaName.Contains("*")).First();
			return alias;
		}

		private static Regex s_RegPanDomain = new Regex(@"^\*\.[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]+)+$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
		//private static Regex s_RegWildcardDomain = new Regex(@"^(\{\d\}){1,10}.[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]+)+$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
		public static PortalAlias GetPortalAlias(string host, int port, bool isSsl)
		{
			var aliases =
				from pa in AllPortalAliases
				where port == pa.Port && isSsl == pa.IsSsl
					&& (host == pa.Host || (s_RegPanDomain.IsMatch(pa.Host) && string.Compare("*" + host.Substring(host.IndexOf(".")), pa.Host, true) == 0))
				select pa;

			return aliases.FirstOrDefault();
		}

		public static PortalAlias GetPortalAlias(Portal portal, string area)
		{
			return portal.PortalAliases.Where(pa => pa.AreaName == area).SingleOrDefault()
				?? portal.PortalAliases.Where(pa => pa.AreaName == "*").SingleOrDefault();
		}

		public static IEnumerable<PortalAlias> AllPortalAliases
		{
			get
			{
				if (s_AllPortalAliases == null)
				{
					List<PortalAlias> pas = new List<PortalAlias>();
					foreach (var p in AllPortals)
					{
						pas.AddRange(p.PortalAliases);
					}
					s_AllPortalAliases = pas;
				}
				return s_AllPortalAliases;
			}
		} private static IEnumerable<PortalAlias> s_AllPortalAliases;

		public static IEnumerable<Portal> AllPortals
		{
			get
			{
				if (s_AllPortals == null)
				{
					s_AllPortals = new List<Portal>() {
						new Portal() {
							PortalID = 0,
							PortalName = "Default",
							DefaultArea = null,
							PortalAliases = new List<PortalAlias>() {
								new PortalAlias("localhost", 307){
									AreaName = "*"
								},
								new PortalAlias("localhost", 308)
								{
									AreaName = "Secure"
								}
							}
						}
					};
				}
				return s_AllPortals;
			}
		} private static IEnumerable<Portal> s_AllPortals;
	}
}
