using System;
using NCore.Reflections;

namespace PineCone.Structures
{
	[Serializable]
	public enum DataTypeCode
	{
		Unknown,
		IntegerNumber,
		FractalNumber,
		Bool,
		DateTime,
		Guid,
		String,
		Enum
	}

	public static class DataTypeExtensions
	{
		public static DataTypeCode ToDataType(this Type type)
		{
			if (type.IsAnyIntegerNumberType())
				return DataTypeCode.IntegerNumber;

			if (type.IsAnyFractalNumberType())
				return DataTypeCode.FractalNumber;

			if (type.IsAnyBoolType())
				return DataTypeCode.Bool;

			if (type.IsAnyDateTimeType())
				return DataTypeCode.DateTime;

			if (type.IsAnyGuidType())
				return DataTypeCode.Guid;

			if (type.IsStringType())
				return DataTypeCode.String;

			if (type.IsAnyEnumType())
				return DataTypeCode.Enum;

			return DataTypeCode.Unknown;
		}
	}
}