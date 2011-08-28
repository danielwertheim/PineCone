using System;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
    [TestFixture]
    public class StructureBuilderNestedStructuresTests : UnitTestBase
    {
        private StructureBuilder _builder;

        protected override void OnTestInitialize()
        {
            _builder = new StructureBuilder(
                new StructureIndexesFactory());
        }

        [Test]
        public void CreateStructure_WhenNestedStructureExists_NestedStructureWillNotBePartOfStructure()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<Structure1>();
            var item = new Structure1 { IntOnStructure1 = 142, Nested = new Structure2 { IntOnStructure2 = 242 } };

            var structure = _builder.CreateStructure(item, schema);

            Assert.AreEqual(1, structure.Indexes.Count);
            Assert.AreEqual("IntOnStructure1", structure.Indexes[0].Path);
        }

        private class Structure1
        {
            public Guid StructureId { get; set; }

            public int IntOnStructure1 { get; set; }

            public Structure2 Nested { get; set; }
        }

        private class Structure2
        {
            public Guid StructureId { get; set; }

            public int IntOnStructure2 { get; set; }
        }
    }
}