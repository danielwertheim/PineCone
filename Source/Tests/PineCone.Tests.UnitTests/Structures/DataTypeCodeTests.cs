using System;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
	[TestFixture]
	public class DataTypeCodeTests : UnitTestBase
	{
		[Test]
		[TestCase(typeof(int))]
		[TestCase(typeof(int?))]
		[TestCase(typeof(short))]
		[TestCase(typeof(short?))]
		public void ToDataType_TypeIsIntegerFamily_ReturnsIntegerNumber(Type type)
		{
			Assert.AreEqual(DataTypeCode.IntegerNumber, type.ToDataType());
		}

		[Test]
		[TestCase(typeof(Single))]
		[TestCase(typeof(Single?))]
		[TestCase(typeof(double))]
		[TestCase(typeof(double?))]
		[TestCase(typeof(decimal))]
		[TestCase(typeof(decimal?))]
		[TestCase(typeof(float))]
		[TestCase(typeof(float?))]
		public void ToDataType_TypeIsFractalFamily_ReturnsFractalNumber(Type type)
		{
			Assert.AreEqual(DataTypeCode.FractalNumber, type.ToDataType());
		}

		[Test]
		[TestCase(typeof(bool))]
		[TestCase(typeof(bool?))]
		public void ToDataType_TypeIsBool_ReturnsBool(Type type)
		{
			Assert.AreEqual(DataTypeCode.Bool, type.ToDataType());
		}

		[Test]
		[TestCase(typeof(DateTime))]
		[TestCase(typeof(DateTime?))]
		public void ToDataType_TypeIsDateTime_ReturnsDateTime(Type type)
		{
			Assert.AreEqual(DataTypeCode.DateTime, type.ToDataType());
		}

		[Test]
		[TestCase(typeof(Guid))]
		[TestCase(typeof(Guid?))]
		public void ToDataType_TypeIsGuid_ReturnsGuid(Type type)
		{
			Assert.AreEqual(DataTypeCode.Guid, type.ToDataType());
		}

		[Test]
		[TestCase(typeof(string))]
		public void ToDataType_TypeIsString_ReturnsString(Type type)
		{
			Assert.AreEqual(DataTypeCode.String, type.ToDataType());
		}

		[Test]
		[TestCase(typeof(DataTypeCode))]
		[TestCase(typeof(DataTypeCode?))]
		public void ToDataType_TypeIsEnum_ReturnsEnum(Type type)
		{
			Assert.AreEqual(DataTypeCode.Enum, type.ToDataType());
		}
	}
}