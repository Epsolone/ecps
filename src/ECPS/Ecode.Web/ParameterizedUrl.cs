using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using Ecode.Collections;

namespace Ecode.Web
{

	/// <summary>
	/// 一个参数化的 URL
	/// </summary>
	public class ParameterizedUrl
	{

		#region Private Fields
		private NameValueCollection m_Queries = new NameValueCollection();
		#endregion

		#region .ctor()
		/// <summary>
		/// 创建一个 ParameterizedUrl 类的新实例。
		/// </summary>
		/// <param name="uri"></param>
		public ParameterizedUrl(Uri uri)
			: this(uri.OriginalString)
		{
		}

		/// <summary>
		/// 创建一个 ParameterizedUrl 类的新实例。
		/// </summary>
		/// <param name="url"></param>
		public ParameterizedUrl(string url)
		{
			TryParseUrl(url);
		}

		/// <summary>
		/// 创建一个 ParameterizedUrl 类的新实例。
		/// </summary>
		public ParameterizedUrl()
		{
		}
		#endregion

		#region TryParseUrl()
		/// <summary>
		/// 尝试解析 URL
		/// </summary>
		/// <param name="url"></param>
		private void TryParseUrl(string url)
		{
			if (url == null)
				return;
			int sharpIndex = url.IndexOf('#');
			if (sharpIndex >= 0)
			{
				m_Anchor = url.Substring(sharpIndex + 1);
				url = url.Remove(sharpIndex);
			}
			int questionIndex = url.IndexOf('?');
			if (questionIndex >= 0)
			{
				string[] queries = url.Substring(questionIndex + 1).Split('&');
				for (int i = 0; i < queries.Length; i++)
				{
					if (queries[i].Length == 0)
						continue;
					int equalIndex = queries[i].IndexOf('=');
					if (equalIndex >= 0)
						m_Queries.Add(queries[i].Remove(equalIndex), HttpUtility.UrlDecode(queries[i].Substring(equalIndex + 1)));
					else
						m_Queries.Add(queries[i], null);
				}
				url = url.Remove(questionIndex);
			}
			int lastSplashIndex = url.LastIndexOf('/');
			if (lastSplashIndex >= 0)
			{
				m_FileName = url.Substring(lastSplashIndex + 1);
				m_Path = (lastSplashIndex < url.Length - 1) ? url.Remove(lastSplashIndex + 1) : url;
			}
			else
			{
				m_FileName = url;
			}
		}
		#endregion

		#region Path URL 的路径，不含文件名。
		private string m_Path;
		/// <summary>
		/// URL 的路径
		/// </summary>
		/// <remarks>
		/// 不含文件名，以'/'结尾。
		/// </remarks>
		public string Path
		{
			get
			{
				return m_Path;
			}
			set
			{
				m_Path = value;
				if (!string.IsNullOrEmpty(m_Path))
				{
					m_Path = m_Path.Trim();
					if (m_Path[m_Path.Length - 1] != '/')
					{
						m_Path += "/";
					}
				}
			}
		}
		#endregion

		#region FileName URL 的文件名
		private string m_FileName;
		/// <summary>
		/// URL 的文件名
		/// </summary>
		public string FileName
		{
			get
			{
				return m_FileName;
			}
			set
			{
				if (value.IndexOf('/') >= 0)
					throw new ArgumentOutOfRangeException("The \"/\" is not a valid character.");
				m_FileName = value;
			}
		}
		#endregion

		#region Queries 查询参数集
		private NameValueCollection m_ReadonlyQueries;
		/// <summary>
		/// 查询参数集
		/// </summary>
		public NameValueCollection Queries
		{
			get
			{
				lock (this)
				{
					return m_Queries;
					//if (m_ReadonlyQueries == null)
					//{
					//    m_ReadonlyQueries = new ReadOnlyNameValueCollection(m_Queries);
					//}
					//return m_ReadonlyQueries;
				}
			}
		}
		#endregion

		#region Anchor 锚点
		private string m_Anchor;
		/// <summary>
		/// 锚点
		/// </summary>
		public string Anchor
		{
			get
			{
				return m_Anchor;
			}
		}
		#endregion

		#region GetQuery() 获取查询参数值
		/// <summary>
		/// 获取查询参数值
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <returns>查询参数值</returns>
		public string GetQuery(string queryName)
		{
			return Queries[queryName];
		}
		#endregion

		#region GetQueries() 获取查询参数值
		/// <summary>
		/// 获取查询参数值
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <returns>查询参数值</returns>
		public string[] GetQueries(string queryName)
		{
			return Queries.GetValues(queryName);
		}
		#endregion

		#region GetQueryString() 获取查询字符串

		/// <summary>
		/// 获取查询字符串
		/// </summary>
		/// <returns></returns>
		public string GetQueryString()
		{
			if (m_Queries.Keys.Count == 0)
			{
				return null;
			}
			StringBuilder sb = new StringBuilder();
			List<string> keys = new List<string>();
			for (int i = 0; i < m_Queries.Keys.Count; i++)
			{
				keys.Add(m_Queries.Keys[i]);
			}
			keys.Sort();
			for (int i = 0; i < keys.Count; i++)
			{
				string[] values = m_Queries.GetValues(keys[i]);
				for (int j = 0; j < values.Length; j++)
				{
					sb.Append(keys[i] + '=' + HttpUtility.UrlEncode(values[j]) + '&');
				}
			}
			sb.Length--;
			return sb.ToString();
		}

		#endregion

		#region SetQuery() 设置查询参数

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, string value)
		{
			lock (this)
			{
				if (value == null)
					m_Queries.Remove(queryName);
				else
					m_Queries[queryName] = value;
				m_ReadonlyQueries = null;
			}
			return this;
		}

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, int value)
		{
			return SetQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, Guid value)
		{
			return SetQuery(queryName, value.ToString("N"));
		}

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, decimal value)
		{
			return SetQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, double value)
		{
			return SetQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, float value)
		{
			return SetQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <param name="containTime">是否包含时间</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, DateTime value, bool containTime)
		{
			return SetQuery(queryName, value.ToString(containTime ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd"));
		}

		/// <summary>
		/// 设置查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl SetQuery(string queryName, DateTime date)
		{
			return SetQuery(queryName, date, false);
		}

		#endregion

		#region AddQuery() 添加查询参数

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, string value)
		{
			lock (this)
			{
				m_Queries.Add(queryName, value);
				m_ReadonlyQueries = null;
			}
			return this;
		}

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, int value)
		{
			return AddQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, Guid value)
		{
			return AddQuery(queryName, value.ToString("N"));
		}

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, decimal value)
		{
			return AddQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, double value)
		{
			return AddQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, float value)
		{
			return AddQuery(queryName, value.ToString());
		}

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, DateTime value, bool containTime)
		{
			return AddQuery(queryName, value.ToString(containTime ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd"));
		}

		/// <summary>
		/// 添加查询参数
		/// </summary>
		/// <param name="queryName">查询参数名</param>
		/// <param name="value">查询参数值</param>
		/// <returns>返回设置后的 <c>Ecode.Web.ParameterizedUrl</c></returns>
		public ParameterizedUrl AddQuery(string queryName, DateTime date)
		{
			return AddQuery(queryName, date, false);
		}

		#endregion

		#region ToString() 转化成字符串形式
		/// <summary>
		/// 转化成字符串形式
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(m_Path);
			sb.Append(m_FileName);

			if (m_Queries.Keys.Count > 0)
			{
				sb.Append('?');
				List<string> keys = new List<string>();
				for (int i = 0; i < m_Queries.Keys.Count; i++)
				{
					keys.Add(m_Queries.Keys[i]);
				}
				keys.Sort();
				for (int i = 0; i < keys.Count; i++)
				{
					string[] values = m_Queries.GetValues(keys[i]);
					for (int j = 0; j < values.Length; j++)
					{
						sb.Append(keys[i] + '=' + HttpUtility.UrlEncode(values[j]) + '&');
					}
				}
				sb.Length--;
			}
			if (!string.IsNullOrEmpty(m_Anchor))
			{
				sb.Append('#' + m_Anchor);
			}

			return sb.ToString();
		}
		#endregion

		#region implicit operator () 重载隐式转换运算符实现与字符串类型互转

		/// <summary>
		/// 重载隐式转换运算符实现与字符串类型互转
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static implicit operator ParameterizedUrl(string url)
		{
			return new ParameterizedUrl(url);
		}

		/// <summary>
		/// 重载隐式转换运算符实现与字符串类型互转
		/// </summary>
		/// <param name="qUrl"></param>
		/// <returns></returns>
		public static implicit operator string(ParameterizedUrl qUrl)
		{
			return qUrl.ToString();
		}

		#endregion

	}
}
