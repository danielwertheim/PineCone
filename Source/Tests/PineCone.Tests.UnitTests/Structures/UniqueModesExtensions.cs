using NUnit.Framework;
using PineCone.Annotations;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class UniqueModesExtensions : UnitTestBase
    {
        [Test]
        public void ToStructureIndexType_WhenPerInstance_MapsToUniquePerInstance()
        {
            var mappedValue = UniqueModes.PerInstance.ToStructureIndexType();

            Assert.AreEqual(StructureIndexType.UniquePerInstance, mappedValue);
        }

        [Test]
        public void ToStructureIndexType_WhenPerType_MapsToUniquePerType()
        {
            var mappedValue = UniqueModes.PerType.ToStructureIndexType();

            Assert.AreEqual(StructureIndexType.UniquePerType, mappedValue);
        }
    }
}