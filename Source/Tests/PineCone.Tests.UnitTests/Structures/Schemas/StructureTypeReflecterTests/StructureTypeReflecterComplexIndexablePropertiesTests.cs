using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PineCone.Annotations;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterComplexIndexablePropertiesTests : UnitTestBase
    {
        private readonly TestableReflecter _reflecter = new TestableReflecter();

        [Test]
        public void GetComplexIndexableProperties_WhenRootWithSimpleAndComplexProperties_ReturnsOnlyComplexProperties()
        {
            var properties = _reflecter.InvokeGetComplexIndexablePropertyInfos(
                typeof(WithSimpleAndComplexProperties).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            Assert.AreEqual(1, properties.Count());
        }

        [Test]
        public void GetComplexProperties_WhenRootWithUniqeAndNonUniqueComplexProperties_ReturnsComplexUniqueProperties()
        {
            var properties = _reflecter.InvokeGetComplexIndexablePropertyInfos(
                typeof(WithUniqueAndNonUniqueComplexProperties).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            Assert.AreEqual(2, properties.Count());
        }

        [Test]
        public void GetComplexProperties_WhenRootWithEnumerable_EnumerableMemberIsNotReturnedAsComplex()
        {
            var properties = _reflecter.InvokeGetComplexIndexablePropertyInfos(
                typeof(WithEnumerable).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            Assert.AreEqual(0, properties.Count());
        }

        [Test]
        public void GetComplexProperties_WhenItIsContainedStructure_NotExtracted()
        {
            var properties = _reflecter.InvokeGetComplexIndexablePropertyInfos(
                typeof(WithContainedStructure).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            Assert.AreEqual(0, properties.Count());
        }

        private class Item
        {}

        private class WithSimpleAndComplexProperties
        {
            public string SimpleStringProperty { get; set; }

            public int SimpleIntProperty { get; set; }

            public Item ComplexProperty { get; set; }
        }

        private class WithUniqueAndNonUniqueComplexProperties
        {
            [Unique(UniqueModes.PerInstance)]
            public Item UqComplex1 { get; set; }

            public Item UqComplex2 { get; set; }
        }

        private class WithEnumerable
        {
            public IEnumerable<Item> Dummies { get; set; }
        }

        private class WithContainedStructure
        {
            public int StructureId { get; set; }

            public Structure ContainedStructure { get; set; }
        }

        private class Structure
        {
            public int StructureId { get; set; }
        }
    }
}