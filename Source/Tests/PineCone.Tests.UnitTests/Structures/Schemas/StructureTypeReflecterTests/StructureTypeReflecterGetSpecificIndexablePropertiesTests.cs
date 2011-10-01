using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterGetSpecificIndexablePropertiesTests : UnitTestBase
    {
        [Test]
        public void GetSpecificIndexableProperties_WhenCalledWithNullType_ThrowsArgumentNullException()
        {
            var reflecter = new StructureTypeReflecter();

            var ex = Assert.Throws<ArgumentNullException>(() => reflecter.GetSpecificIndexableProperties(null, null));

            Assert.AreEqual("type", ex.ParamName);
        }

        [Test]
        public void GetSpecificIndexableProperties_WhenCalledWithNullExlcudes_ThrowsArgumentException()
        {
            var reflecter = new StructureTypeReflecter();

            var ex = Assert.Throws<ArgumentException>(() => reflecter.GetSpecificIndexableProperties(typeof(WithStructureId), null));

            Assert.AreEqual("indexablePaths", ex.ParamName);
        }

        [Test]
        public void GetSpecificIndexableProperties_WhenCalledWithNoExlcudes_ThrowsArgumentException()
        {
            var reflecter = new StructureTypeReflecter();

            var ex = Assert.Throws<ArgumentException>(() => reflecter.GetSpecificIndexableProperties(typeof(WithStructureId), new string[] { }));

            Assert.AreEqual("indexablePaths", ex.ParamName);
        }

        [Test]
        public void GetSpecificIndexableProperties_WhenIncludingStructureId_PropertyIsNotReturned()
        {
            var reflecter = new StructureTypeReflecter();

            var properties = reflecter.GetSpecificIndexableProperties(typeof(WithStructureId), new[] { "StructureId" });

            Assert.AreEqual(0, properties.Count());
            Assert.IsNull(properties.SingleOrDefault(p => p.Path == "StructureId"));
        }

        [Test]
        public void GetSpecificIndexableProperties_WhenIncludingNonStructureId_StructureIdPropIsNotReturned()
        {
            var reflecter = new StructureTypeReflecter();

            var properties = reflecter.GetSpecificIndexableProperties(typeof(WithStructureId), new[] { "Int1" });

            Assert.AreEqual(1, properties.Count());
            Assert.IsNull(properties.SingleOrDefault(p => p.Path == "StructureId"));
        }

        [Test]
        public void GetSpecificIndexableProperties_WhenIncludingBytesArray_PropertyIsNotReturned()
        {
            var reflecter = new StructureTypeReflecter();

            var properties = reflecter.GetSpecificIndexableProperties(typeof(WithStructureId), new[] { "Bytes1" });

            Assert.AreEqual(0, properties.Count());
            Assert.IsNull(properties.SingleOrDefault(p => p.Path == "Bytes1"));
        }

        [Test]
        public void GetSpecificIndexableProperties_WhenIncludingProperty_PropertyIsReturned()
        {
            var reflecter = new StructureTypeReflecter();

            var properties = reflecter.GetSpecificIndexableProperties(typeof(WithStructureId), new[] { "Bool1" });

            Assert.AreEqual(1, properties.Count());
            Assert.IsNotNull(properties.SingleOrDefault(p => p.Path == "Bool1"));
        }

        [Test]
        public void GetSpecificIndexableProperties_WhenIncludingNestedProperty_PropertyIsReturned()
        {
            var reflecter = new StructureTypeReflecter();

            var properties = reflecter.GetSpecificIndexableProperties(typeof(WithStructureId), new[] { "Nested", "Nested.Int1OnNested" });

            Assert.AreEqual(1, properties.Count());
            Assert.IsNotNull(properties.SingleOrDefault(p => p.Path == "Nested.Int1OnNested"));
        }

        private class WithStructureId
        {
            public int StructureId { get; set; }

            public bool Int1 { get; set; }

            public bool Bool1 { get; set; }

            public DateTime DateTime1 { get; set; }

            public string String1 { get; set; }

            public byte[] Bytes1 { get; set; }

            public Nested Nested { get; set; }
        }

        private class Nested
        {
            public int Int1OnNested { get; set; }
        }
    }
}