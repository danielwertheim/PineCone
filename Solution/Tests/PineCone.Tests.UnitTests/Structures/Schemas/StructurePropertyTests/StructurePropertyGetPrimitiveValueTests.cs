using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructurePropertyTests
{
    [TestFixture]
    public class StructurePropertyGetPrimitiveValueTests : UnitTestBase
    {
        [Test]
        public void GetValue_WhenAssignedIntOnFirstLevel_ReturnsAssignedValue()
        {
            var propertyInfo = typeof(Dummy).GetProperty("Int1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            const int expected = 33;
            var item = new Dummy { Int1 = expected };
            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenAssignedNullableIntOnFirstLevel_ReturnsAssignedValue()
        {
            var propertyInfo = typeof(Dummy).GetProperty("NullableInt1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            const int expected = 33;
            var item = new Dummy { NullableInt1 = expected };
            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenUnAssignedNullableIntOnFirstLevel_ReturnsNull()
        {
            var propertyInfo = typeof(Dummy).GetProperty("NullableInt1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            var item = new Dummy { NullableInt1 = null };
            var actual = property.GetValue(item);

            Assert.IsNull(actual);
        }

        [Test]
        public void GetValue_WhenAssignedBoolOnFirstLevel_ReturnsAssignedValue()
        {
            var propertyInfo = typeof(Dummy).GetProperty("Bool1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            const bool expected = true;
            var item = new Dummy { Bool1 = expected };
            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenAssignedNullableBoolOnFirstLevel_ReturnsAssignedValue()
        {
            var propertyInfo = typeof(Dummy).GetProperty("NullableBool1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            const bool expected = true;
            var item = new Dummy { NullableBool1 = expected };
            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenUnAssignedNullableBoolOnFirstLevel_ReturnsNull()
        {
            var propertyInfo = typeof(Dummy).GetProperty("NullableBool1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            var item = new Dummy { NullableBool1 = null };
            var actual = property.GetValue(item);

            Assert.IsNull(actual);
        }

        [Test]
        public void GetValue_WhenAssignedDecimalOnFirstLevel_ReturnsAssignedValue()
        {
            var propertyInfo = typeof(Dummy).GetProperty("Decimal1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            const decimal expected = 1.33m;
            var item = new Dummy { Decimal1 = expected };
            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenAssignedNullableDecimalOnFirstLevel_ReturnsAssignedValue()
        {
            var propertyInfo = typeof(Dummy).GetProperty("NullableDecimal1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            const decimal expected = 1.33m;
            var item = new Dummy { NullableDecimal1 = expected };
            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenUnAssignedNullableDecimalOnFirstLevel_ReturnsNull()
        {
            var propertyInfo = typeof(Dummy).GetProperty("NullableDecimal1");
            var property = StructureProperty.CreateFrom(propertyInfo);

            var item = new Dummy { NullableDecimal1 = null };
            var actual = property.GetValue(item);

            Assert.IsNull(actual);
        }

        private class Dummy
        {
            public int Int1 { get; set; }

            public bool Bool1 { get; set; }

            public decimal Decimal1 { get; set; }

            public int? NullableInt1 { get; set; }

            public bool? NullableBool1 { get; set; }

            public decimal? NullableDecimal1 { get; set; }
        }
    }
}