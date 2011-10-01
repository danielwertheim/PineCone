using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructurePropertyTests
{
    [TestFixture]
    public class StructurePropertyGetIdentityValueTests : UnitTestBase
    {
        [Test]
        public void GetIdValue_WhenIntOnFirstLevel_ReturnsInt()
        {
            var intPropertyInfo = typeof(IdentityOnRoot).GetProperty("StructureId");
            var intProperty = StructureProperty.CreateFrom(intPropertyInfo);

            const int expected = 42;
            var item = new IdentityOnRoot { StructureId = expected };

            var actual = intProperty.GetValue(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetIdValue_WhenNullableIntOnFirstLevel_ReturnsInt()
        {
            var intPropertyInfo = typeof(NullableIdentityOnRoot).GetProperty("StructureId");
            var intProperty = StructureProperty.CreateFrom(intPropertyInfo);

            const int expectedInt = 42;
            var item = new NullableIdentityOnRoot { StructureId = expectedInt };

            var actual = intProperty.GetValue(item);

            Assert.AreEqual(expectedInt, actual);
        }

        [Test]
        public void GetIdValue_WhenNullAssignedNullableIntOnFirstLevel_ReturnsInt()
        {
            var intPropertyInfo = typeof(NullableIdentityOnRoot).GetProperty("StructureId");
            var intProperty = StructureProperty.CreateFrom(intPropertyInfo);

            var item = new NullableIdentityOnRoot { StructureId = null };

            var actual = intProperty.GetValue(item);

            Assert.IsNull(actual);
        }

        private class IdentityOnRoot
        {
            public int StructureId { get; set; }
        }

        private class NullableIdentityOnRoot
        {
            public int? StructureId { get; set; }
        }
    }
}