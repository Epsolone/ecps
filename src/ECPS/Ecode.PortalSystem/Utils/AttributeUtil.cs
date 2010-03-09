using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Ecode.PortalSystem.Utils
{

	/// <summary>
	/// A class to support the custom attribute operations.
	/// </summary>
	internal static class AttributeUtil
	{

		/// <summary>
		/// Get the types which have the specified attribute matches the function in a assembly.
		/// </summary>
		/// <typeparam name="TAttr">The comtom attribute to be included.</typeparam>
		/// <param name="assembly">The assembly to enumerate types.</param>
		/// <param name="func">The anonymous function to filter the custom attributes.</param>
		/// <returns></returns>
		public static Type[] GetTypes<TAttr>(Assembly assembly, Func<TAttr, bool> func) where TAttr : Attribute
		{
			return assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(TAttr), true).Any(a => func((TAttr)a))).ToArray();
		}

		/// <summary>
		/// Get the types which have the specified attribute in a assembly.
		/// </summary>
		/// <typeparam name="TAttr">The comtom attribute to be included.</typeparam>
		/// <returns></returns>
		public static Type[] GetTypes<TAttr>(Assembly assembly) where TAttr : Attribute
		{
			return GetTypes<TAttr>(assembly, a => true);
		}

		/// <summary>
		/// Get the fields which have the specified attribute matches the function.
		/// </summary>
		/// <typeparam name="T">The type to enumerate the fields.</typeparam>
		/// <typeparam name="TAttr">The comtom attribute to be included.</typeparam>
		/// <param name="func">The anonymous function to filter the custom attributes.</param>
		/// <returns></returns>
		public static FieldInfo[] GetFields<T, TAttr>(Func<TAttr, bool> func) where TAttr : Attribute
		{
			return typeof(T).GetFields().Where(f => f.GetCustomAttributes(typeof(TAttr), true).Any(a => func((TAttr)a))).ToArray();
		}

		/// <summary>
		/// Get the fields which have the specified attribute.
		/// </summary>
		/// <typeparam name="T">The type to enumerate the fields.</typeparam>
		/// <typeparam name="TAttr">The comtom attribute to be included.</typeparam>
		/// <returns></returns>
		public static FieldInfo[] GetFields<T, TAttr>() where TAttr : Attribute
		{
			return GetFields<T, TAttr>(a => true);
		}

		/// <summary>
		/// Get the properties which have the specified attribute matches the function.
		/// </summary>
		/// <typeparam name="T">The type to enumerate the properties.</typeparam>
		/// <typeparam name="TAttr">The comtom attribute to be included.</typeparam>
		/// <param name="func">The anonymous function to filter the custom attributes.</param>
		/// <returns></returns>
		public static PropertyInfo[] GetProperties<T, TAttr>(Func<TAttr, bool> func) where TAttr : Attribute
		{
			return typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(TAttr), true).Any(a => func((TAttr)a))).ToArray();
		}

		/// <summary>
		/// Get the properties which have the specified attribute.
		/// </summary>
		/// <typeparam name="T">The type to enumerate the properties.</typeparam>
		/// <typeparam name="TAttr">The comtom attribute to be included.</typeparam>
		/// <returns></returns>
		public static PropertyInfo[] GetProperties<T, TAttr>() where TAttr : Attribute
		{
			return GetProperties<T, TAttr>(a => true);
		}

	}
}
