using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecode.PortalSystem.Configuration
{

	/// <summary>
	/// Interface of configuration
	/// </summary>
	public interface IConfiguration
	{
		/// <summary>
		/// Gets the specified configuration.
		/// </summary>
		/// <param name="configKey">The configuration key.</param>
		/// <returns>The object representing the configuration.</returns>
		object GetConfig(string configKey);

		/// <summary>
		/// Used for initialization.
		/// </summary>
		void Init();

	}
}
