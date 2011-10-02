using System.Collections.Generic;
using NUnit.Framework;
using PineCone.Annotations;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructurePropertyTests
{
    [TestFixture]
    public class StructurePropertyTests : UnitTestBase
    {
        [Test]
        public void IsUnique_WhenPrimitiveMarkedAsUnique_ReturnsTrue()
        {
            var property = GetProperty<DummyForUniquesTests>("Uq1");

            Assert.IsTrue(property.IsUnique);
        }

        [Test]
        public void IsUnique_WhenPrimitiveMarkedAsNonUnique_ReturnsFalse()
        {
            var property = GetProperty<DummyForUniquesTests>("Int1");

            Assert.IsFalse(property.IsUnique);
        }

        [Test]
        public void IsEnumerable_WhenIEnumerableOfSimpleType_ReturnsTrue()
        {
            var property = GetProperty<DummyForEnumerableTests>("Ints");

            Assert.IsTrue(property.IsEnumerable);
        }

        [Test]
        public void IsEnumerable_WhenPrimitiveType_ReturnsFalse()
        {
            var property = GetProperty<DummyForEnumerableTests>("Int1");

            Assert.IsFalse(property.IsEnumerable);
        }

        private static StructureProperty GetProperty<T>(string name)
        {
            return StructurePropertyTestHelper.GetProperty<T>(name);
        }

        private class DummyForUniquesTests
        {
            public int Int1 { get; set; }

            [Unique(UniqueModes.PerInstance)]
            public int Uq1 { get; set; }
        }

        private class DummyForEnumerableTests
        {
            public int Int1 { get; set; }

            public IEnumerable<int> Ints { get; set; }
        }
    }
}