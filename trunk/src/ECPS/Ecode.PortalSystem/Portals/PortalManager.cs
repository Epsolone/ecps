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
		public List<PortalAlias> PortalAliases { get; set; }
		public string DefaultController { get; set; }
		public string DefaultAction { get; set; }
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
					throw new ArgumentOutOfRangeException("port range is 0~65535");
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

		public override string ToString()
		{
			return string.Format("{0}://{1}{2}/", IsSsl ? "https" : "http", Host, (Port == 80) ? "" : (":" + Port.ToString()));
		}

	}
	public static class PortalManager
	{
		public static Portal GetPortal(string host, int port)
		{
			return new Portal() { PortalID = 0, DefaultController = "Default", DefaultAction = "Index" };
		}

		public static PortalAlias GetPortalAlias(Portal portal, string controller)
		{
			return null;
		}
	}
}
