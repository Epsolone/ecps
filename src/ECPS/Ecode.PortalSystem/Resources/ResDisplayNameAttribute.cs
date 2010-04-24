using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ecode.PortalSystem.Utils;

namespace Ecode.PortalSystem.Resources
{
	public class ResDisplayNameAttribute : DisplayNameAttribute
	{
		private string m_ResKey;
		private string m_ClassKey;

		public ResDisplayNameAttribute(string classKey, string resourceKey)
		{
			m_ClassKey = classKey;
			m_ResKey = resourceKey;
		}

		public override string DisplayName
		{
			get
			{
				return StaticResUtil.GetResourceString(m_ClassKey, m_ResKey);
			}
		}
	}
}
