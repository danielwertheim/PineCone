using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PineCone.Serializers;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
    [TestFixture]
    public class StructureBuilderTests : StructureBuilderBaseTests
    {
        [Test]
        public void CreateStructure_WhenIdIsAssigned_IdIsOverWritten()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();
            var initialId = StructureIdGenerator.CreateId();
            var item = new GuidItem { StructureId = initialId };

            var structure = Builder.CreateStructure(item, schema);

            Assert.AreNotEqual(initialId, structure.Id);
        }

        [Test]
        public void CreateStructures_WhenSerializerIsSpecified_SerializerIsConsumed()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();

            var serializer = new Mock<ISerializer>();
            Func<GuidItem, object> serializerFunc = s => s.StructureId + ";" + s.Value;
            serializer.Setup<object>(s => s.Serialize(It.IsAny<GuidItem>())).Returns(serializerFunc);
            Builder.Serializer = serializer.Object;

            var items = CreateGuidItems(3).ToArray();
            var structures = Builder.CreateStructures(items, schema).ToList();

            Assert.AreEqual(3, structures.Count);
            Assert.AreEqual(items[0].StructureId + ";" + items[0].Value, structures[0].Data);
            Assert.AreEqual(items[1].StructureId + ";" + items[1].Value, structures[1].Data);
            Assert.AreEqual(items[2].StructureId + ";" + items[2].Value, structures[2].Data);
        }

        [Test]
        public void CreateStructure_WhenIntOnFirstLevel_ReturnsSimpleValue()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
            var item = new TestItemForFirstLevel { IntValue = 42 };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.Single(si => si.Path == "IntValue").Value;
            Assert.AreEqual(42, actual);
        }

        [Test]
        public void CreateStructure_WhenEnumerableIntsOnFirstLevel_ReturnsOneIndexPerElement()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
            var item = new TestItemForFirstLevel { IntArray = new[] { 5, 6, 7 } };

            var structure = Builder.CreateStructure(item, schema);

            var firstIndex = structure.Indexes.Single(si => si.Path == "IntArray[0]").Value;
            var secondIndex = structure.Indexes.Single(si => si.Path == "IntArray[1]").Value;
            var lastIndex = structure.Indexes.Single(si => si.Path == "IntArray[2]").Value;

            Assert.AreEqual(5, firstIndex);
            Assert.AreEqual(6, secondIndex);
            Assert.AreEqual(7, lastIndex);
        }

        [Test]
        public void CreateStructure_WhenIntOnSecondLevel_ReturnsSimpleValue()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForSecondLevel>();
            var item = new TestItemForSecondLevel { Container = new Container { IntValue = 42 } };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.Single(si => si.Path == "Container.IntValue").Value;
            Assert.AreEqual(42, actual);
        }

        [Test]
        public void CreateStructure_WhenEnumerableIntsOnSecondLevelAreNull_ReturnsNoIndex()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForSecondLevel>();
            var item = new TestItemForSecondLevel { Container = new Container() };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.SingleOrDefault(si => si.Path.StartsWith("Container.IntArray"));
            Assert.IsNull(actual);
        }

        [Test]
        public void CreateStructure_WhenEnumerableIntsOnSecondLevel_ReturnsOneIndexPerElement()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForSecondLevel>();
            var item = new TestItemForSecondLevel { Container = new Container { IntArray = new[] { 5, 6, 7 } } };

            var structure = Builder.CreateStructure(item, schema);

            var firstIndex = structure.Indexes.Single(si => si.Path == "Container.IntArray[0]").Value;
            var secondIndex = structure.Indexes.Single(si => si.Path == "Container.IntArray[1]").Value;
            var lastIndex = structure.Indexes.Single(si => si.Path == "Container.IntArray[2]").Value;

            Assert.AreEqual(5, firstIndex);
            Assert.AreEqual(6, secondIndex);
            Assert.AreEqual(7, lastIndex);
        }

        private class TestItemForFirstLevel
        {
            public Guid StructureId { get; set; }

            public int IntValue { get; set; }

            public int[] IntArray { get; set; }
        }

        private class TestItemForSecondLevel
        {
            public Guid StructureId { get; set; }

            public Container Container { get; set; }
        }

        private class Container
        {
            public int IntValue { get; set; }

            public int[] IntArray { get; set; }
        }
    }
}