using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecode.PortalSystem.Configuration
{

	/// <summary>
	/// The base class of portalable configuration.
	/// </summary>
	public abstract class PortalableConfiguration
	{

		/// <summary>
		/// Create a new instance of class PortalableConfiguration
		/// </summary>
		/// <param name="portalID">portal id</param>
		public PortalableConfiguration(int portalID)
		{
			PortalID = portalID;
		}

		/// <summary>
		/// Portal identity.
		/// </summary>
		public int PortalID
		{
			get;
			set;
		}

	}
}
