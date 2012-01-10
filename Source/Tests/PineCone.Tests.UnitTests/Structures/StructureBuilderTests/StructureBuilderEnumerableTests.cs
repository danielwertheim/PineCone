using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
	[TestFixture]
	public class StructureBuilderEnumerableTests : StructureBuilderBaseTests
	{
		protected override void OnTestInitialize()
		{
			Builder = new StructureBuilder();
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
		public void CreateStructure_WhenCustomNonGenericEnumerableWithComplexElement_ReturnsIndexesForElements()
		{
			var schema = StructureSchemaTestFactory.CreateRealFrom<ModelForMyCustomNonGenericEnumerable>();
			var item = new ModelForMyCustomNonGenericEnumerable();
			item.Items.Add(new MyElement { IntValue = 1, StringValue = "A" });
			item.Items.Add(new MyElement { IntValue = 2, StringValue = "B" });

			var structure = Builder.CreateStructure(item, schema);

			var indices = structure.Indexes.Skip(1).ToList();
			Assert.AreEqual("A", indices[0].Value);
			Assert.AreEqual("B", indices[1].Value);
			Assert.AreEqual(1, indices[2].Value);
			Assert.AreEqual(2, indices[3].Value);
		}

		private class ModelForMyCustomNonGenericEnumerable
		{
			public Guid StructureId { get; set; }
			public MyCustomNonGenericEnumerable Items { get; set; }

			public ModelForMyCustomNonGenericEnumerable()
			{
				Items = new MyCustomNonGenericEnumerable();
			}
		}

		private class MyCustomNonGenericEnumerable : System.Collections.Generic.List<MyElement>
		{
		}

		private class MyElement
		{
			public string StringValue { get; set; }
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