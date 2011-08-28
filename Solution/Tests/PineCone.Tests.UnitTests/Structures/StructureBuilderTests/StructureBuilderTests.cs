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
    }
}