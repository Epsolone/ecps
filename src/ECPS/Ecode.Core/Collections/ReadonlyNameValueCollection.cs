using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Ecode.Collections
{

	/// <summary>
	/// 只读的 NameValueCollection
	/// </summary>
	public class ReadOnlyNameValueCollection : NameValueCollection
	{

		/// <summary>
		/// 创建一个 ReadOnlyNameValueCollection 类的新实例
		/// </summary>
		/// <param name="value"></param>
		public ReadOnlyNameValueCollection(NameValueCollection value)
			: base(value)
		{
			SetReadOnly();
		}

		/// <summary>
		/// 设置为只读
		/// </summary>
		private void SetReadOnly()
		{
			base.IsReadOnly = true;
		}
	}
}
