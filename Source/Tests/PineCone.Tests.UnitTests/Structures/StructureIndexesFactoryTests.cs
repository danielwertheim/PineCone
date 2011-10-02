using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureIndexesFactoryTests : UnitTestBase
    {
        [Test]
        public void GetIndexes_WhenItemWithAssignedString_ReturnsIndexWithStringValue()
        {
            var item = new WithNoArray { StringValue = "A" };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithNoArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual("A", indexes[0].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithAssignedInt_ReturnsIndexWithIntValue()
        {
            var item = new WithNoArray { IntValue = 42 };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithNoArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual(42, indexes[1].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithOneString_ReturnsIndexWithString()
        {
            var item = new WithArray { StringValues = new[] { "A" } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual("A", indexes[0].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithOneInt_ReturnsIndexWithInt()
        {
            var item = new WithArray { IntValues = new[] { 42 } };
            var schema = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schema, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual(42, indexes[0].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoDifferentStrings_ReturnsTwoStringIndexes()
        {
            var item = new WithArray { StringValues = new[] { "A", "B" } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual("A", indexes[0].Value);
            Assert.AreEqual("B", indexes[1].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoDifferentInts_ReturnsTwoIntIndexes()
        {
            var item = new WithArray { IntValues = new[] { 42, 43 } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual(42, indexes[0].Value);
            Assert.AreEqual(43, indexes[1].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoEqualElements_ReturnsTwoStringIndexes()
        {
            var item = new WithArray { StringValues = new[] { "A", "A" } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual("A", indexes[0].Value);
            Assert.AreEqual("A", indexes[1].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoEqualElements_ReturnsTwoIntIndexes()
        {
            var item = new WithArray { IntValues = new[] { 42, 42 } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, StructureIdGenerator.CreateId()).ToList();

            Assert.AreEqual(42, indexes[0].Value);
            Assert.AreEqual(42, indexes[1].Value);
        }

        private class WithNoArray
        {
            public Guid StructureId { get; set; }

            public string StringValue { get; set; }

            public int IntValue { get; set; }
        }

        private class WithArray
        {
            public Guid StructureId { get; set; }

            public string[] StringValues { get; set; }

            public int[] IntValues { get; set; }
        }
    }
}