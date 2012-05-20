using System;

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
        Text,
		Enum
	}
}