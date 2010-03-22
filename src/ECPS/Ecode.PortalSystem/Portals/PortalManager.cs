using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecode.PortalSystem.Portals
{

	public class Portal
	{
		public Portal() { }
		public int PortalID { get; set; }
		public string PortalName { get; set; }
		public string DefaultController { get; set; }
		public string DefaultAction { get; set; }

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

		public bool IsSsl
		{
			get { return m_IsSsl; }
			set { m_IsSsl = value; }
		} private bool m_IsSsl = false;

		public string[] Controllers { get; set; }

		/// <summary>
		/// HTTP 前缀(包含“://”)
		/// </summary>
		public string Prefix
		{
			get
			{
				return IsSsl ? "https://" : "http://";
			}
		}

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
			return string.Format("{0}{1}{2}/", Prefix, Host, PortExpression);
		}

	}

	public static class PortalManager
	{
		public static Portal GetPortal(string host, int port)
		{
			return AllPortals.First();
		}

		public static PortalAlias GetPortalAlias(Portal portal, string controller)
		{
			PortalAlias alias = portal.PortalAliases.Where(pa => pa.Controllers.Contains(controller)).FirstOrDefault();
			if (alias == null)
				alias = portal.PortalAliases.Where(pa => pa.Controllers.Contains("*")).First();
			return alias;
		}

		public static PortalAlias GetPortalAlias(string host, int port, bool isSsl)
		{
			throw new NotImplementedException();
		}

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
							DefaultController = "Home",
							DefaultAction = "Index",
							PortalAliases = new List<PortalAlias>() {
								new PortalAlias("localhsot", 307){
									Controllers = new string[]{"*"}
								},
								new PortalAlias("localhost", 308)
								{
									Controllers = new string[]{"Account"}
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
