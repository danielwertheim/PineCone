using System;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructurePropertyTests
{
    [TestFixture]
    public class StructurePropertyGetGuidValueTests : UnitTestBase
    {
        [Test]
        public void GetIdValue_WhenGuidOnFirstLevel_ReturnsGuid()
        {
            var guidPropertyInfo = typeof(GuidOnRoot).GetProperty("StructureId");
            var guidProperty = StructureProperty.CreateFrom(guidPropertyInfo);

            var expected = Guid.Parse("4217F3B7-6DEB-4DFA-B195-D111C1297988");
            var item = new GuidOnRoot { StructureId = expected };

            var actual = guidProperty.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetIdValue_WhenNullableGuidOnFirstLevel_ReturnsGuid()
        {
            var intPropertyInfo = typeof(NullableGuidOnRoot).GetProperty("StructureId");
            var intProperty = StructureProperty.CreateFrom(intPropertyInfo);

            var expected = Guid.Parse("4217F3B7-6DEB-4DFA-B195-D111C1297988");
            var item = new NullableGuidOnRoot { StructureId = expected };

            var actual = intProperty.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetIdValue_WhenNullAssignedNullableGuidOnFirstLevel_ReturnsNull()
        {
            var intPropertyInfo = typeof(NullableGuidOnRoot).GetProperty("StructureId");
            var intProperty = StructureProperty.CreateFrom(intPropertyInfo);

            var item = new NullableGuidOnRoot { StructureId = null };

            var actual = intProperty.GetValue(item);

            Assert.IsNull(actual);
        }

        

        private class GuidOnRoot
        {
            public Guid StructureId { get; set; }
        }

        private class NullableGuidOnRoot
        {
            public Guid? StructureId { get; set; }
        }

        private class Container
        {
            public GuidOnRoot GuidOnRootItem { get; set; }
        }
    }
}