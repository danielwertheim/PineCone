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
            var mappedValue = UniqueMode.PerInstance.ToStructureIndexType();

            Assert.AreEqual(StructureIndexType.UniquePerInstance, mappedValue);
        }

        [Test]
        public void ToStructureIndexType_WhenPerType_MapsToUniquePerType()
        {
            var mappedValue = UniqueMode.PerType.ToStructureIndexType();

            Assert.AreEqual(StructureIndexType.UniquePerType, mappedValue);
        }
    }
}