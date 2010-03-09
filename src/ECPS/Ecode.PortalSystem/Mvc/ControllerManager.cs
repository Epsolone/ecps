using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Compilation;
using System.Collections;
using System.Reflection;

namespace Ecode.PortalSystem.Mvc
{
	/// <summary>
	/// 控制器管理器
	/// </summary>
	public static class ControllerManager
	{

		private static object _locker = new object();

		private static IDictionary<string, Type> g_AllControllerTypes;
		/// <summary>
		/// 获取所有 Controller 类型。
		/// </summary>
		public static IDictionary<string, Type> AllControllerTypes
		{
			get
			{
				lock (_locker)
				{
					if (g_AllControllerTypes == null)
					{
						g_AllControllerTypes = new Dictionary<string, Type>();
						var types = GetAllControllerTypes();
						foreach (var t in types)
						{
							g_AllControllerTypes.Add(t.Name.Remove(t.Name.Length - "Controller".Length), t);
						}
					}
				}
				return g_AllControllerTypes;
			}
		}

		/// <summary>
		/// 获取所有 Controller 类型。
		/// </summary>
		/// <returns>返回一个包含所有 Controller 类型的 IList 对象。</returns>
		private static IList<Type> GetAllControllerTypes()
		{
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
			{
				Type[] types;
				try
				{
					types = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException exception)
				{
					types = exception.Types;
				}
				list.AddRange(types.Where<Type>(t =>
							t != null
							&& t.IsPublic
							&& t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
							&& !t.IsAbstract
							&& typeof(IController).IsAssignableFrom(t)
						)
					);
			}
			return list;
		}
	}
}
