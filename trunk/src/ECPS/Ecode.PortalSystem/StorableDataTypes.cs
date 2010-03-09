using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecode.PortalSystem
{

	/// <summary>
	/// data type can be stored in the file or database.
	/// </summary>
	public enum StorableDataTypes
	{

		/// <summary>
		/// char string
		/// </summary>
		String = 0,

		/// <summary>
		/// boolean
		/// </summary>
		Boolean = 1,
		/// <summary>
		/// bit (same as boolean)
		/// </summary>
		Bit = Boolean,

		/// <summary>
		/// signed byte
		/// </summary>
		SByte = 10,
		/// <summary>
		/// 16 bits integer
		/// </summary>
		Int16 = 11,
		/// <summary>
		/// 32 bits integer
		/// </summary>
		Int32 = 12,
		/// <summary>
		/// 64 bits integer
		/// </summary>
		Int64 = 13,

		/// <summary>
		/// byte
		/// </summary>
		Byte = 20,
		/// <summary>
		/// unsigned 16 bits integer
		/// </summary>
		UInt16 = 21,
		/// <summary>
		/// unsigned 32 bits integer
		/// </summary>
		UInt32 = 22,
		/// <summary>
		/// unsigned 64 bits integer
		/// </summary>
		UInt64 = 23,

		/// <summary>
		/// single float
		/// </summary>
		Single = 30,
		/// <summary>
		/// double float
		/// </summary>
		Double = 31,
		/// <summary>
		/// decimal
		/// </summary>
		Decimal = 32,

		/// <summary>
		/// data and time
		/// </summary>
		DateTime = 40,

		/// <summary>
		/// GUID
		/// </summary>
		Guid = 50,

	}
}
