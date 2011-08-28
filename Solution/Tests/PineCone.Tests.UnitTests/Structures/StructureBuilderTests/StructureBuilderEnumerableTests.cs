using System;
using System.Linq;
using NUnit.Framework;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
    [TestFixture]
    public class StructureBuilderEnumerableTests : StructureBuilderBaseTests
    {
        [Test]
        public void CreateStructure_WhenEnumerableIntsOnFirstLevel_ReturnsOneIndexPerElementAndNotJagged()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<EnumerableTestItem>();
            var item = new EnumerableTestItem {IntArray = new[] {1, 2, 3}};

            var structure = Builder.CreateStructure(item, schema);

            CollectionAssert.AreEqual(new[]{1,2,3}, structure.Indexes.Where(si => si.Path == "IntArray").Select(si => si.Value));
        }

        private class EnumerableTestItem
        {
            public Guid StructureId { get; set; }

            public int[] IntArray { get; set; }
        }
    }
}