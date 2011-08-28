using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureIndexTypeTests : UnitTestBase
    {
        [Test]
        public void IsUnique_WhenNormalNonUnique_ReturnsFalse()
        {
            Assert.IsFalse(StructureIndexType.Normal.IsUnique());
        }

        [Test]
        public void IsUnique_WhenUniquePerInstance_ReturnsTrue()
        {
            Assert.IsTrue(StructureIndexType.UniquePerInstance.IsUnique());
        }

        [Test]
        public void IsUnique_WhenUniquePerType_ReturnsTrue()
        {
            Assert.IsTrue(StructureIndexType.UniquePerType.IsUnique());
        }
    }
}