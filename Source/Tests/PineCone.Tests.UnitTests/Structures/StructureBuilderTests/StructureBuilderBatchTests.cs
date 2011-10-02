using System.Linq;
using NUnit.Framework;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
    [TestFixture]
    public class StructureBuilderBatchTests : StructureBuilderBaseTests
    {
        [Test]
        public void CreateStructures_WhenProcessing2900Items_ItemsAreGettingGeneratedInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();
            var items = CreateGuidItems(2900);

            var structuresBatches = Builder.CreateStructures(items, schema, 1000);

            var previousTotalStructuresRead = 0;
            foreach (var structuresBatch in structuresBatches)
            {
                var structuresIds = structuresBatch.Select(s => s.Id).ToArray();
                var itemIds = items.Skip(previousTotalStructuresRead).Take(structuresIds.Length).Select(i => i.StructureId).ToArray();

                previousTotalStructuresRead += structuresIds.Length;

                CollectionAssert.AreEqual(itemIds, structuresIds);
            }
        }
    }
}