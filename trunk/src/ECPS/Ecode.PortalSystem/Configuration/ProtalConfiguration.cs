using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecode.PortalSystem.Configuration
{
	/// <summary>
	/// the configuration for portal
	/// </summary>
    public class ProtalConfiguration : PortalableConfiguration
    {
		/// <summary>
		/// create a instance of class ProtalConfiguration.
		/// </summary>
		/// <param name="portalID">portal id</param>
		public ProtalConfiguration(int portalID) : base(portalID)
		{
		}
    }
}
