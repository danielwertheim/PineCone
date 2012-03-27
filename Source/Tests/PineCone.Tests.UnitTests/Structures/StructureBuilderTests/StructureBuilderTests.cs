using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PineCone.Serializers;
using PineCone.Structures;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
    [TestFixture]
	public class StructureBuilderTests : StructureBuilderBaseTests
	{
		protected override void OnTestInitialize()
		{
			Builder = new StructureBuilder();
		}

		[Test]
		public void CreateStructures_WhenProcessing50Items_ItemsAreGettingGeneratedInCorrectOrder()
		{
			var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();
			var items = CreateGuidItems(50);

			var structures = Builder.CreateStructures(items, schema).ToArray();

			CollectionAssert.AreEqual(
				items.Select(i => i.StructureId).ToArray(),
				structures.Select(s => (Guid)s.Id.Value).ToArray());
		}

		[Test]
		public void CreateStructures_WhenProcessing1500Items_ItemsAreGettingGeneratedInCorrectOrder()
		{
			var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();
			var items = CreateGuidItems(1500);

			var structures = Builder.CreateStructures(items, schema).ToArray();

			CollectionAssert.AreEqual(
				items.Select(i => i.StructureId).ToArray(),
				structures.Select(s => (Guid)s.Id.Value).ToArray());
		}

		[Test]
		public void CreateStructure_WhenIdIsNotAssigned_IdIsAssigned()
		{
			var id = StructureId.Create(Guid.Parse("B551349B-53BD-4455-A509-A9B68B58700A"));
			var idGeneratorStub = new Mock<IStructureIdGenerator>();
			idGeneratorStub.Setup(s => s.Generate(It.IsAny<IStructureSchema>())).Returns(id);

			var item = new GuidItem { StructureId = Guid.Empty };
			var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();

			Builder.StructureIdGenerator = idGeneratorStub.Object;
			var structure = Builder.CreateStructure(item, schema);

			Assert.AreEqual(id, structure.Id);
			Assert.AreEqual(id.Value, item.StructureId);
		}

		[Test]
		public void CreateStructure_WhenIdIsAssigned_IdIsOverWritten()
		{
			var initialId = StructureId.Create(Guid.Parse("B551349B-53BD-4455-A509-A9B68B58700A"));
			var item = new GuidItem { StructureId = (Guid)initialId.Value };
			var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();

			var structure = Builder.CreateStructure(item, schema);

			Assert.AreNotEqual(initialId, structure.Id);
			Assert.AreNotEqual(initialId.Value, item.StructureId);
		}

		[Test]
		public void CreateStructures_WhenSerializerIsSpecified_SerializerIsConsumed()
		{
			var schema = StructureSchemaTestFactory.CreateRealFrom<GuidItem>();
			var serializer = new Mock<IStructureSerializer>();
			Func<GuidItem, object> serializerFunc = s => s.StructureId + ";" + s.Value;
			serializer.Setup<object>(s => s.Serialize(It.IsAny<GuidItem>())).Returns(serializerFunc);
			var items = CreateGuidItems(3).ToArray();

			Builder.StructureSerializer = serializer.Object;
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
		public void CreateStructure_WhenIntOnSecondLevel_ReturnsSimpleValue()
		{
			var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForSecondLevel>();
			var item = new TestItemForSecondLevel { Container = new Container { IntValue = 42 } };

			var structure = Builder.CreateStructure(item, schema);

			var actual = structure.Indexes.Single(si => si.Path == "Container.IntValue").Value;
			Assert.AreEqual(42, actual);
		}

		[Test]
		public void CreateStructure_WhenSpecificIdGeneratorIsPassed_SpecificIdGeneratorIsConsumed()
		{
			var idValue = new Guid("A058FCDE-A3D9-4EAA-AA41-0CE9D4A3FB1E");
			var idGeneratorMock = new Mock<IStructureIdGenerator>();
			idGeneratorMock.Setup(m => m.Generate(It.IsAny<IStructureSchema>())).Returns(StructureId.Create(idValue));

			var item = new TestItemForFirstLevel { IntValue = 42 };
			var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();

			Builder.StructureIdGenerator = idGeneratorMock.Object;
			var structure = Builder.CreateStructure(item, schema);

			idGeneratorMock.Verify(m => m.Generate(schema), Times.Once());
		}

		[Test]
		public void CreateStructures_WhenSpecificIdGeneratorIsPassed_SpecificIdGeneratorIsConsumed()
		{
			var idValues = new Queue<IStructureId>();
			idValues.Enqueue(StructureId.Create(Guid.Parse("A058FCDE-A3D9-4EAA-AA41-0CE9D4A3FB1E")));
			idValues.Enqueue(StructureId.Create(Guid.Parse("91D77A9D-C793-4F3D-9DD0-F1F336362C5C")));
		
			var items = new[] { new TestItemForFirstLevel { IntValue = 42 }, new TestItemForFirstLevel { IntValue = 43 } };
			var schema = StructureSchemaTestFactory.CreateRealFrom<TestItemForFirstLevel>();
			var idGeneratorMock = new Mock<IStructureIdGenerator>();
			idGeneratorMock
				.Setup(m => m.Generate(schema))
				.Returns(idValues.Dequeue);

			Builder.StructureIdGenerator = idGeneratorMock.Object;
			var structures = Builder.CreateStructures(items, schema).ToArray();

			idGeneratorMock.Verify(m => m.Generate(schema), Times.Exactly(2));
		}

		

		[Test]
		public void CreateStructure_WhenStructureContainsStructWithValue_ValueOfStructIsRepresentedInIndex()
		{
			var schema = StructureSchemaTestFactory.CreateRealFrom<StructContainer>();
			var item = new StructContainer { Content = "My content" };

			var structure = Builder.CreateStructure(item, schema);

			Assert.AreEqual(2, structure.Indexes.Count);
			Assert.AreEqual("Content", structure.Indexes[1].Path);
			Assert.AreEqual(typeof(Text), structure.Indexes[1].DataType);
			Assert.AreEqual(new Text("My content"), structure.Indexes[1].Value);
		}

        private class TestItemForFirstLevel
		{
			public Guid StructureId { get; set; }

			public int IntValue { get; set; }
		}

		private class TestItemForSecondLevel
		{
			public Guid StructureId { get; set; }

			public Container Container { get; set; }
		}

		private class Container
		{
			public int IntValue { get; set; }
		}

		private class StructContainer
		{
			public Guid StructureId { get; set; }

			public Text Content { get; set; }
		}

		[Serializable]
		private struct Text
		{
			private readonly string _value;

			public Text(string value)
			{
				_value = value;
			}

			public static Text Parse(string value)
			{
				return value == null ? null : new Text(value);
			}

			public static implicit operator Text(string value)
			{
				return new Text(value);
			}

			public static implicit operator string(Text item)
			{
				return item._value;
			}

			public override string ToString()
			{
				return _value;
			}
		}
	}
}