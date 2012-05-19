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
            const int expected = 33;
            var item = new Dummy { Int1 = expected };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("Int1");

            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenAssignedNullableIntOnFirstLevel_ReturnsAssignedValue()
        {
            const int expected = 33;
            var item = new Dummy { NullableInt1 = expected };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("NullableInt1");

            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenUnAssignedNullableIntOnFirstLevel_ReturnsNull()
        {
            var item = new Dummy { NullableInt1 = null };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("NullableInt1");

            var actual = property.GetValue(item);

            Assert.IsNull(actual);
        }

        [Test]
        public void GetValue_WhenAssignedBoolOnFirstLevel_ReturnsAssignedValue()
        {
            const bool expected = true;
            var item = new Dummy { Bool1 = expected };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("Bool1");

            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenAssignedNullableBoolOnFirstLevel_ReturnsAssignedValue()
        {
            const bool expected = true;
            var item = new Dummy { NullableBool1 = expected };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("NullableBool1");

            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenUnAssignedNullableBoolOnFirstLevel_ReturnsNull()
        {
            var item = new Dummy { NullableBool1 = null };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("NullableBool1");

            var actual = property.GetValue(item);

            Assert.IsNull(actual);
        }

        [Test]
        public void GetValue_WhenAssignedDecimalOnFirstLevel_ReturnsAssignedValue()
        {
            const decimal expected = 1.33m;
            var item = new Dummy { Decimal1 = expected };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("Decimal1");

            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenAssignedNullableDecimalOnFirstLevel_ReturnsAssignedValue()
        {
            const decimal expected = 1.33m;
            var item = new Dummy { NullableDecimal1 = expected };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("NullableDecimal1");

            var actual = property.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetValue_WhenUnAssignedNullableDecimalOnFirstLevel_ReturnsNull()
        {
            var item = new Dummy { NullableDecimal1 = null };
            var property = StructurePropertyTestFactory.GetPropertyByPath<Dummy>("NullableDecimal1");

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