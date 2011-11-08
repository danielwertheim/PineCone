using System;
using System.Collections.Generic;
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
        public void CreateStructure_WhenIdIsAssignedAndOptionsSaysOverrideId_IdIsOverWritten()
        {
            var initialId = StructureId.Create(Guid.Parse("B551349B-53BD-4455-A509-A9B68B58700A"));
            var item = new GuidItem { StructureId = (Guid)initialId.Value };
            var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();

            var structure = Builder.CreateStructure(item, schema, new StructureBuilderOptions { KeepStructureId = false });

            Assert.AreNotEqual(initialId, structure.Id);
            Assert.AreNotEqual(initialId.Value, item.StructureId);
        }

        [Test]
        public void CreateStructure_WhenIdIsAssignedAndOptionsSaysKeepId_IdIsNotOverWritten()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();
            var initialId = StructureId.Create(Guid.Parse("B551349B-53BD-4455-A509-A9B68B58700A"));
            var item = new GuidItem { StructureId = (Guid)initialId.Value };
            
            var structure = Builder.CreateStructure(item, schema, new StructureBuilderOptions { KeepStructureId = true });

            Assert.AreEqual(initialId, structure.Id);
            Assert.AreEqual(initialId.Value, item.StructureId);
        }

        [Test]
        public void CreateStructures_WhenSerializerIsSpecified_SerializerIsConsumed()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();
            var serializer = new Mock<ISerializer>();
            Func<GuidItem, object> serializerFunc = s => s.StructureId + ";" + s.Value;
            serializer.Setup<object>(s => s.Serialize(It.IsAny<GuidItem>())).Returns(serializerFunc);
            var items = CreateGuidItems(3).ToArray();

            var structures = Builder.CreateStructures(items, schema, new StructureBuilderOptions { Serializer = serializer.Object }).ToList();

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
        public void CreateStructure_WhenEnumerableIntsOnFirstLevel_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
            var item = new TestItemForFirstLevel { IntArray = new[] { 5, 6, 7 } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path == "IntArray").ToList();
            Assert.AreEqual(5, indices[0].Value);
            Assert.AreEqual(6, indices[1].Value);
            Assert.AreEqual(7, indices[2].Value);
        }

        [Test]
        public void CreateStructure_WhenEnumerableIntsOnFirstLevelAreNull_ReturnsNoIndex()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
            var item = new TestItemForFirstLevel { IntArray = null };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.SingleOrDefault(si => si.Path.StartsWith("IntArray"));
            Assert.IsNull(actual);
            Assert.AreEqual(2, structure.Indexes.Count);
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
            var item = new TestItemForSecondLevel { Container = new Container { IntArray = null } };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.SingleOrDefault(si => si.Path.StartsWith("Container.IntArray"));
            Assert.IsNull(actual);
            Assert.AreEqual(2, structure.Indexes.Count);
        }

        [Test]
        public void CreateStructure_WhenEnumerableIntsOnSecondLevel_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForSecondLevel>();
            var item = new TestItemForSecondLevel { Container = new Container { IntArray = new[] { 5, 6, 7 } } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path == "Container.IntArray").ToList();
            Assert.AreEqual(5, indices[0].Value);
            Assert.AreEqual(6, indices[1].Value);
            Assert.AreEqual(7, indices[2].Value);
        }

        [Test]
        public void CreateStructure_WhenSpecificIdGeneratorIsPassed_SpecificIdGeneratorIsConsumed()
        {
            var idValue = new Guid("A058FCDE-A3D9-4EAA-AA41-0CE9D4A3FB1E");
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
            var idGeneratorMock = new Mock<IStructureIdGenerator>();
            idGeneratorMock.Setup(m => m.CreateId(schema)).Returns(StructureId.Create(idValue));
            var item = new TestItemForFirstLevel { IntValue = 42 };

            var structure = Builder.CreateStructure(item, schema, new StructureBuilderOptions { IdGenerator = idGeneratorMock.Object });

            idGeneratorMock.Verify(m => m.CreateId(schema), Times.Once());
        }

        [Test]
        public void CreateStructures_WhenSpecificIdGeneratorIsPassed_SpecificIdGeneratorIsConsumed()
        {
            var idValue = new Guid("A058FCDE-A3D9-4EAA-AA41-0CE9D4A3FB1E");
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
            var idGeneratorMock = new Mock<IStructureIdGenerator>();
            idGeneratorMock.Setup(m => m.CreateIds(1, schema)).Returns(new[] { StructureId.Create(idValue) });

            var item = new TestItemForFirstLevel { IntValue = 42 };

            var structures = Builder.CreateStructures(new[] { item }, schema, new StructureBuilderOptions { IdGenerator = idGeneratorMock.Object }).ToArray();

            idGeneratorMock.Verify(m => m.CreateIds(1, schema), Times.Once());
        }

        [Test]
        public void CreateStructureBatches_WhenSpecificIdGeneratorIsPassed_SpecificIdGeneratorIsConsumed()
        {
            var idValue = new Guid("A058FCDE-A3D9-4EAA-AA41-0CE9D4A3FB1E");
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
            var idGeneratorMock = new Mock<IStructureIdGenerator>();
            idGeneratorMock.Setup(m => m.CreateIds(1, schema)).Returns(new[] { StructureId.Create(idValue) });

            var item = new TestItemForFirstLevel { IntValue = 42 };

            var structures = Builder.CreateStructureBatches(new[] { item }, schema, 1, new StructureBuilderOptions { IdGenerator = idGeneratorMock.Object }).ToArray();

            idGeneratorMock.Verify(m => m.CreateIds(1, schema), Times.Once());
        }

        [Test]
        public void CreateStructure_WhenHashSetOfInts_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithHashSet>();
            var item = new TestItemWithHashSet { HashSetOfInts = new HashSet<int> { 5, 6, 7 } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path == "HashSetOfInts").ToList();
            Assert.AreEqual(5, indices[0].Value);
            Assert.AreEqual(6, indices[1].Value);
            Assert.AreEqual(7, indices[2].Value);
        }

        [Test]
        public void CreateStructure_WhenHashSetOfIntsIsNull_ReturnsNoIndex()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithHashSet>();
            var item = new TestItemWithHashSet { HashSetOfInts = null };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.SingleOrDefault(si => si.Path.StartsWith("HashSetOfInts"));
            Assert.IsNull(actual);
            Assert.AreEqual(1, structure.Indexes.Count);
        }

        [Test]
        public void CreateStructure_WhenISetOfInts_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithISet>();
            var item = new TestItemWithISet { SetOfInts = new HashSet<int> { 5, 6, 7 } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path == "SetOfInts").ToList();
            Assert.AreEqual(5, indices[0].Value);
            Assert.AreEqual(6, indices[1].Value);
            Assert.AreEqual(7, indices[2].Value);
        }

        [Test]
        public void CreateStructure_WhenSetOfIntsIsNull_ReturnsNoIndex()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithISet>();
            var item = new TestItemWithISet { SetOfInts = null };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.SingleOrDefault(si => si.Path.StartsWith("SetOfInts"));
            Assert.IsNull(actual);
            Assert.AreEqual(1, structure.Indexes.Count);
        }

        [Test]
        public void CreateStructure_WhenHashSetOfComplex_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithHashSetOfComplex>();
            var item = new TestItemWithHashSetOfComplex { HashSetOfComplex = new HashSet<Value> { new Value { Is = 5 }, new Value { Is = 6 }, new Value { Is = 7 } } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path == "HashSetOfComplex.Is").ToList();
            Assert.AreEqual(5, indices[0].Value);
            Assert.AreEqual(6, indices[1].Value);
            Assert.AreEqual(7, indices[2].Value);
        }

        [Test]
        public void CreateStructure_WhenHashSetOfComplex_HasThreeNullItems_ReturnsOneIndexRepresentingNull()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithHashSetOfComplex>();
            var item = new TestItemWithHashSetOfComplex { HashSetOfComplex = new HashSet<Value> { null, null, null } };

            var structure = Builder.CreateStructure(item, schema);

            var actual = structure.Indexes.SingleOrDefault(si => si.Path.StartsWith("HashSetOfComplex.Is"));
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, structure.Indexes.Count);
        }

        [Test]
        public void CreateStructure_WhenIDictionary_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithIDictionary>();
            var item = new TestItemWithIDictionary { KeyValues = new Dictionary<string, int> { { "Key1", 5 }, { "Key2", 6 }, { "Key3", 7 } } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path.StartsWith("KeyValues")).ToList();
            Assert.AreEqual(6, indices.Count);

            Assert.AreEqual("KeyValues.Key", indices[0].Path);
            Assert.AreEqual("Key1", indices[0].Value);
            Assert.AreEqual("KeyValues.Key", indices[1].Path);
            Assert.AreEqual("Key2", indices[1].Value);
            Assert.AreEqual("KeyValues.Key", indices[2].Path);
            Assert.AreEqual("Key3", indices[2].Value);

            Assert.AreEqual("KeyValues.Value", indices[3].Path);
            Assert.AreEqual(5, indices[3].Value);
            Assert.AreEqual("KeyValues.Value", indices[4].Path);
            Assert.AreEqual(6, indices[4].Value);
            Assert.AreEqual("KeyValues.Value", indices[5].Path);
            Assert.AreEqual(7, indices[5].Value);
        }

        [Test]
        public void CreateStructure_WhenDictionary_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithDictionary>();
            var item = new TestItemWithDictionary { KeyValues = new Dictionary<string, int> { { "Key1", 5 }, { "Key2", 6 }, { "Key3", 7 } } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path.StartsWith("KeyValues")).ToList();
            Assert.AreEqual(6, indices.Count);

            Assert.AreEqual("KeyValues.Key", indices[0].Path);
            Assert.AreEqual("Key1", indices[0].Value);
            Assert.AreEqual("KeyValues.Key", indices[1].Path);
            Assert.AreEqual("Key2", indices[1].Value);
            Assert.AreEqual("KeyValues.Key", indices[2].Path);
            Assert.AreEqual("Key3", indices[2].Value);

            Assert.AreEqual("KeyValues.Value", indices[3].Path);
            Assert.AreEqual(5, indices[3].Value);
            Assert.AreEqual("KeyValues.Value", indices[4].Path);
            Assert.AreEqual(6, indices[4].Value);
            Assert.AreEqual("KeyValues.Value", indices[5].Path);
            Assert.AreEqual(7, indices[5].Value);
        }

        [Test]
        public void CreateStructure_WhenIDictionaryWithComplex_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithIDictionaryOfComplex>();
            var item = new TestItemWithIDictionaryOfComplex { KeyValues = new Dictionary<string, Value> { { "Key1", new Value { Is = 5 } }, { "Key2", new Value { Is = 6 } }, { "Key3", new Value { Is = 7 } } } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path.StartsWith("KeyValues")).ToList();
            Assert.AreEqual(6, indices.Count);

            Assert.AreEqual("KeyValues.Key", indices[0].Path);
            Assert.AreEqual("Key1", indices[0].Value);
            Assert.AreEqual("KeyValues.Key", indices[1].Path);
            Assert.AreEqual("Key2", indices[1].Value);
            Assert.AreEqual("KeyValues.Key", indices[2].Path);
            Assert.AreEqual("Key3", indices[2].Value);

            Assert.AreEqual("KeyValues.Value.Is", indices[3].Path);
            Assert.AreEqual(5, indices[3].Value);
            Assert.AreEqual("KeyValues.Value.Is", indices[4].Path);
            Assert.AreEqual(6, indices[4].Value);
            Assert.AreEqual("KeyValues.Value.Is", indices[5].Path);
            Assert.AreEqual(7, indices[5].Value);
        }

        [Test]
        public void CreateStructure_WhenDictionaryWithComplex_ReturnsOneIndexPerElementInCorrectOrder()
        {
            var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemWithDictionaryOfComplex>();
            var item = new TestItemWithDictionaryOfComplex { KeyValues = new Dictionary<string, Value> { { "Key1", new Value { Is = 5 } }, { "Key2", new Value { Is = 6 } }, { "Key3", new Value { Is = 7 } } } };

            var structure = Builder.CreateStructure(item, schema);

            var indices = structure.Indexes.Where(i => i.Path.StartsWith("KeyValues")).ToList();
            Assert.AreEqual(6, indices.Count);

            Assert.AreEqual("KeyValues.Key", indices[0].Path);
            Assert.AreEqual("Key1", indices[0].Value);
            Assert.AreEqual("KeyValues.Key", indices[1].Path);
            Assert.AreEqual("Key2", indices[1].Value);
            Assert.AreEqual("KeyValues.Key", indices[2].Path);
            Assert.AreEqual("Key3", indices[2].Value);

            Assert.AreEqual("KeyValues.Value.Is", indices[3].Path);
            Assert.AreEqual(5, indices[3].Value);
            Assert.AreEqual("KeyValues.Value.Is", indices[4].Path);
            Assert.AreEqual(6, indices[4].Value);
            Assert.AreEqual("KeyValues.Value.Is", indices[5].Path);
            Assert.AreEqual(7, indices[5].Value);
        }

        private class Value
        {
            public int Is { get; set; }
        }

        private class TestItemWithHashSetOfComplex
        {
            public Guid StructureId { get; set; }

            public HashSet<Value> HashSetOfComplex { get; set; }
        }

        private class TestItemWithHashSet
        {
            public Guid StructureId { get; set; }

            public HashSet<int> HashSetOfInts { get; set; }
        }

        private class TestItemWithISet
        {
            public Guid StructureId { get; set; }

            public ISet<int> SetOfInts { get; set; }
        }

        private class TestItemWithIDictionary
        {
            public Guid StructureId { get; set; }

            public IDictionary<string, int> KeyValues { get; set; }
        }

        private class TestItemWithIDictionaryOfComplex
        {
            public Guid StructureId { get; set; }

            public IDictionary<string, Value> KeyValues { get; set; }
        }

        private class TestItemWithDictionary
        {
            public Guid StructureId { get; set; }

            public Dictionary<string, int> KeyValues { get; set; }
        }

        private class TestItemWithDictionaryOfComplex
        {
            public Guid StructureId { get; set; }

            public Dictionary<string, Value> KeyValues { get; set; }
        }

        private class TestItemWithIntAsId
        {
            public int StructureId { get; set; }

            public int IntValue { get; set; }
        }

        private class TestItemWithLongAsId
        {
            public long StructureId { get; set; }

            public int IntValue { get; set; }
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