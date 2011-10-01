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
            var structureId = StructureIdGenerator.CreateId();
            var indexes = new List<IStructureIndex>
            {
                new StructureIndex(structureId, "UniqueIndex1", "Value1", true),
                new StructureIndex(structureId, "UniqueIndex1", "Value1", true)
            };
            
            Assert.Throws<PineConeException>(() => new Structure("Name", structureId, indexes));
        }
    }
}