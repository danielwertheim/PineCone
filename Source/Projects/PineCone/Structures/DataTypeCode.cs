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
		Enum,
		Text
	}

	public static class DataTypeExtensions
	{
		private static readonly Type TextType;

		static DataTypeExtensions()
		{
			TextType = typeof (Text);
		}

		public static DataTypeCode ToDataTypeCode(this Type type)
		{
			if(type == TextType)
				return DataTypeCode.Text;
			
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