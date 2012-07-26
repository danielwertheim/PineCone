using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PineCone.Annotations;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterComplexIndexablePropertiesTests : StructureTypeReflecterTestsBase
    {
        [Test]
        public void GetIndexableProperties_WhenItemWithComplexProperty_ReturnsComplexProperties()
        {
            var properties = ReflecterFor<WithComplexProperty>().GetIndexableProperties();

            Assert.AreEqual(2, properties.Count());
            Assert.IsTrue(properties.Any(p => p.Path == "Complex.IntValue"));
            Assert.IsTrue(properties.Any(p => p.Path == "Complex.StringValue"));
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithUniqeAndNonUniqueComplexProperties_ReturnsBothComplexUniqueAndNonUniqueProperties()
        {
            var properties = ReflecterFor<WithUniqueAndNonUniqueComplexProperties>().GetIndexableProperties();

            Assert.AreEqual(4, properties.Count());
            Assert.IsTrue(properties.Any(p => p.Path == "UqComplex.IntValue"));
            Assert.IsTrue(properties.Any(p => p.Path == "UqComplex.StringValue"));
            Assert.IsTrue(properties.Any(p => p.Path == "NonUqComplex.IntValue"));
            Assert.IsTrue(properties.Any(p => p.Path == "NonUqComplex.StringValue"));
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithEnumerable_EnumerableMemberIsNotReturnedAsComplex()
        {
            var properties = ReflecterFor<WithEnumerableOfComplex>().GetIndexableProperties();

            Assert.AreEqual(2, properties.Count());
            Assert.IsTrue(properties.Any(p => p.Path == "Items.IntValue"));
            Assert.IsTrue(properties.Any(p => p.Path == "Items.StringValue"));
        }
        
        private class WithComplexProperty
        {
            public Item Complex { get; set; }
        }

        private class WithUniqueAndNonUniqueComplexProperties
        {
            public ItemWithUniques UqComplex { get; set; }
            public Item NonUqComplex { get; set; }
        }

        private class WithEnumerableOfComplex
        {
            public IEnumerable<Item> Items { get; set; }
        }

        private class Item
        {
            public int IntValue { get; set; }
            public string StringValue { get; set; }
        }

        private class ItemWithUniques
        {
            [Unique(UniqueModes.PerInstance)]
            public int IntValue { get; set; }
            [Unique(UniqueModes.PerType)]
            public string StringValue { get; set; }
        }
    }
}