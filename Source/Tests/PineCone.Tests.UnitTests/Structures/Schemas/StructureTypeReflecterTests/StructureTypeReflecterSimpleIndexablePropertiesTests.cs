using System;
using System.Linq;
using NCore.Reflections;
using NUnit.Framework;
using PineCone.Annotations;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterSimpleIndexablePropertiesTests : UnitTestBase
    {
        [Test]
        public void GetSimpleIndexableProperties_WhenMultiplePublicSimplePropertiesExistsAndNoExclusions_ReturnsAllPublicSimpleProperties()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithSimpleProperties).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            var names = properties.Select(p => p.Name).ToArray();
            Assert.AreEqual(6, properties.Count());
            CollectionAssert.Contains(names, "StructureId");
            CollectionAssert.Contains(names, "Age");
            CollectionAssert.Contains(names, "Name");
            CollectionAssert.Contains(names, "DateOfBirth");
            CollectionAssert.Contains(names, "Wage");
            CollectionAssert.Contains(names, "Byte");
        }

        [Test]
        public void GetSimpleIndexableProperties_WhenByteArray_NotReturned()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithNonSimpleProperties).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            Assert.AreEqual(0, properties.Count());
        }

        [Test]
        public void GetSimpleIndexableProperties_WhenExclusionIsPassed_DoesNotReturnExcludedProperties()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithSimpleProperties).GetProperties(StructureTypeReflecter.PropertyBindingFlags),
                null,
                nonIndexablePaths: new[] { "StructureId", "Name" });

            var names = properties.Select(p => p.Name).ToArray();
            CollectionAssert.DoesNotContain(names, "StructureId");
            CollectionAssert.DoesNotContain(names, "Name");
        }

        [Test]
        public void GetSimpleIndexableProperties_WhenExclusionIsPassed_DoesReturnNonExcludedProperties()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithSimpleProperties).GetProperties(StructureTypeReflecter.PropertyBindingFlags),
                null,
                nonIndexablePaths: new[] { "StructureId", "Name" });

            var names = properties.Select(p => p.Name).ToArray();
            CollectionAssert.Contains(names, "Age");
            CollectionAssert.Contains(names, "DateOfBirth");
            CollectionAssert.Contains(names, "Wage");
        }

        [Test]
        public void GetSimpleIndexableProperties_WhenSimplePrivatePropertyExists_PrivatePropertyIsNotReturned()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithPrivateProperty).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            Assert.AreEqual(0, properties.Count());
        }

        [Test]
        public void GetSimpleIndexableProperties_WhenSimpleAndComplexPropertiesExists_ReturnsOnlySimpleProperties()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithSimpleAndComplexProperties).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            var complex = properties.Where(p => !p.PropertyType.IsSimpleType());
            var names = properties.Select(p => p.Name).ToArray();
            Assert.AreEqual(0, complex.Count());
            Assert.AreEqual(2, properties.Count());
            CollectionAssert.Contains(names, "SimpleIntProperty");
            CollectionAssert.Contains(names, "SimpleStringProperty");
        }

        [Test]
        public void GetSimpleIndexableProperties_WhenUniquesExists_ReturnsSimpleUniqueProperties()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithUniqueIndexes).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            var names = properties.Select(p => p.Name).ToArray();
            Assert.AreEqual(2, properties.Count());
            CollectionAssert.Contains(names, "UqInt");
            CollectionAssert.Contains(names, "UqString");
        }

        [Test]
        public void GetSimpleIndexableProperties_WhenMultiplePublicNullableSimplePropertiesExistsAndNoExclusions_ReturnsAllPublicSimpleProperties()
        {
            var properties = StructureTypeReflecter.GetSimpleIndexablePropertyInfos(
                typeof(WithNullableValueTypes).GetProperties(StructureTypeReflecter.PropertyBindingFlags));

            var names = properties.Select(p => p.Name).ToArray();
            Assert.AreEqual(5, properties.Count());
            CollectionAssert.Contains(names, "StructureId");
            CollectionAssert.Contains(names, "NullableInt");
            CollectionAssert.Contains(names, "NullableDecimal");
            CollectionAssert.Contains(names, "NullableBool");
            CollectionAssert.Contains(names, "NullableDateTime");
        }

        private class WithNullableValueTypes
        {
            public Guid StructureId { get; set; }

            public int? NullableInt { get; set; }

            public bool? NullableBool { get; set; }

            public decimal? NullableDecimal { get; set; }

            public DateTime? NullableDateTime { get; set; }
        }

        private class WithSimpleProperties
        {
            public Guid StructureId { get; set; }

            public int Age { get; set; }

            public string Name { get; set; }

            public DateTime DateOfBirth { get; set; }

            [Unique(UniqueModes.PerInstance)]
            public decimal Wage { get; set; }

            public byte Byte { get; set; }
        }

        private class WithPrivateProperty
        {
            private int Int { get; set; }
        }

        private class WithNonSimpleProperties
        {
            public byte[] Bytes { get; set; }
        }

        private class WithSimpleAndComplexProperties
        {
            public string SimpleStringProperty { get; set; }

            public int SimpleIntProperty { get; set; }

            public WithSimpleProperties ComplexProperty { get; set; }
        }

        private class WithUniqueIndexes
        {
            [Unique(UniqueModes.PerInstance)]
            public int UqInt { get; set; }

            [Unique(UniqueModes.PerInstance)]
            public string UqString { get; set; }

            [Unique(UniqueModes.PerInstance)]
            public WithSimpleProperties UqComplex1 { get; set; }

            public WithSimpleProperties UqComplex2 { get; set; }
        }
    }
}