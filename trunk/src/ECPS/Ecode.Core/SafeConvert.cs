using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecode
{
	public static class SafeConvert
	{
		/// <summary>
		/// 转为日期时间类型
		/// </summary>
		/// <param name="value">字符串值</param>
		/// <returns></returns>
		public static DateTime ToDateTime(string value)
		{
			if (string.IsNullOrEmpty(value))
				return DateTime.MinValue;
			try
			{
				return DateTime.Parse(value);
			}
			catch { }
			return DateTime.MinValue;
		}

		/// <summary>
		/// 转为整数类型
		/// </summary>
		/// <param name="value">字符串值</param>
		/// <returns></returns>
		public static int ToInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 0;
			try
			{
				return int.Parse(value);
			}
			catch { }
			return 0;
		}

		/// <summary>
		/// 转为整数类型
		/// </summary>
		/// <param name="value">值</param>
		/// <returns></returns>
		public static int ToInt(object value)
		{
			if (value == null)
				return 0;
			if (value is string)
				return ToInt((string)value);
			if (value is long)
				return (int)(long)value;
			if (value is ulong)
				return (int)(ulong)value;
			if (value is int)
				return (int)value;
			if (value is uint)
				return (int)(uint)value;
			if (value is short)
				return (int)(short)value;
			if (value is ushort)
				return (int)(ushort)value;
			if (value is sbyte)
				return (int)(sbyte)value;
			if (value is byte)
				return (int)(byte)value;
			if (value is char)
				return (int)(char)value;
			if (value is decimal)
				return (int)(decimal)value;
			if (value is double)
				return (int)(double)value;
			if (value is float)
				return (int)(float)value;
			if (value is bool)
				return ((bool)value) ? 1 : 0;
			if (value is IntPtr)
				return ((IntPtr)value).ToInt32();
			if (value is UIntPtr)
				return (int)((UIntPtr)value).ToUInt32();
			try { return Convert.ToInt32(value); }
			catch { }
			return 0;
		}

		/// <summary>
		/// 转为布尔类型
		/// </summary>
		/// <param name="value">字符串值</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns></returns>
		public static bool ToBoolean(string value, bool defaultValue)
		{
			if (string.IsNullOrEmpty(value))
				return defaultValue;
			value = value.ToLower();
			return defaultValue ? (value != "false" && value != "no" && value != "0") : (value == "true" || value == "yes" || value == "1");
		}

		/// <summary>
		/// 转为布尔类型
		/// </summary>
		/// <param name="value">字符串值</param>
		/// <returns></returns>
		public static bool ToBoolean(string value)
		{
			return ToBoolean(value, false);
		}

		/// <summary>
		/// 转为十进制数类型
		/// </summary>
		/// <param name="value">字符串值</param>
		/// <returns></returns>
		public static decimal ToDecimal(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 0m;
			try
			{
				return decimal.Parse(value);
			}
			catch { }
			return 0m;
		}

		/// <summary>
		/// 转为双精度类型
		/// </summary>
		/// <param name="value">字符串值</param>
		/// <returns></returns>
		public static double ToDouble(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 0;
			try
			{
				return double.Parse(value);
			}
			catch { }
			return 0;
		}

		/// <summary>
		/// 转为 GUID 类型
		/// </summary>
		/// <param name="value">字符串值</param>
		/// <returns></returns>
		public static Guid ToGuid(string value)
		{
			if (string.IsNullOrEmpty(value))
				return Guid.Empty;
			try
			{
				return new Guid(value);
			}
			catch { }
			return Guid.Empty;
		}

		public static string ToShortDateOrTime(DateTime value)
		{
			DateTime today = DateTime.Today;
			return value.ToString(((value.Year == today.Year) ? (value.Month == today.Month) ? (value.Day == today.Day) ? "HH时mm分" : "d日HH时" : "M月d日" : "yyyy年M月"));
		}

	}
}
