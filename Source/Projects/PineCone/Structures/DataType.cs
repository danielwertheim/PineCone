using System;
using NCore.Reflections;

namespace PineCone.Structures
{
	[Serializable]
	public enum DataType
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
		public static DataType ToDataType(this Type type)
		{
			if (type.IsAnyIntegerNumberType())
				return DataType.IntegerNumber;

			if (type.IsAnyFractalNumberType())
				return DataType.FractalNumber;

			if (type.IsAnyBoolType())
				return DataType.Bool;

			if (type.IsAnyDateTimeType())
				return DataType.DateTime;

			if (type.IsAnyGuidType())
				return DataType.Guid;

			if (type.IsStringType())
				return DataType.String;

			if (type.IsAnyEnumType())
				return DataType.Enum;

			return DataType.Unknown;
		}
	}
}