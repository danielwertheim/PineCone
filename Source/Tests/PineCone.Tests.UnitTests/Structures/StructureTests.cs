using System.Collections.Generic;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenIndexesContainsNonUniqueUniqueIndex_ThrowsPineConeException()
        {
            var structureId = StructureId.Create(1);
            var indexes = new List<IStructureIndex>
            {
                new StructureIndex(structureId, "UniqueIndex1", "Value1", StructureIndexType.UniquePerInstance),
                new StructureIndex(structureId, "UniqueIndex1", "Value1", StructureIndexType.UniquePerInstance)
            };
            
            Assert.Throws<PineConeException>(() => new Structure("Name", structureId, indexes));
        }
    }
}