using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PineCone.Structures;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureIndexesFactoryTests : UnitTestBase
    {
        private IStructureIdGenerator _structureIdGenerator;
        private IStructureSchema _structureSchema;

        protected override void OnFixtureInitialize()
        {
            _structureIdGenerator = new GuidStructureIdGenerator();
            _structureSchema = new Mock<IStructureSchema>().Object;
        }

        [Test]
        public void GetIndexes_WhenItemWithAssignedString_ReturnsIndexWithStringValue()
        {
            var item = new WithNoArray { StringValue = "A" };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithNoArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual("A", indexes[0].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithAssignedInt_ReturnsIndexWithIntValue()
        {
            var item = new WithNoArray { IntValue = 42 };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithNoArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual(42, indexes[0].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithOneString_ReturnsIndexWithString()
        {
            var item = new WithArray { StringValues = new[] { "A" } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual("A", indexes[0].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithOneString_ReturnsIndexWithDataTypeOfStringElement()
        {
            var item = new WithArray { StringValues = new[] { "A" } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual(typeof(string), indexes[0].DataType);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithOneInt_ReturnsIndexWithInt()
        {
            var item = new WithArray { IntValues = new[] { 42 } };
            var schema = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schema, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual(42, indexes[0].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoDifferentStrings_ReturnsTwoStringIndexes()
        {
            var item = new WithArray { StringValues = new[] { "A", "B" } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual("A", indexes[0].Value);
            Assert.AreEqual("B", indexes[1].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoDifferentInts_ReturnsTwoIntIndexes()
        {
            var item = new WithArray { IntValues = new[] { 42, 43 } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual(42, indexes[0].Value);
            Assert.AreEqual(43, indexes[1].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoEqualElements_ReturnsTwoStringIndexes()
        {
            var item = new WithArray { StringValues = new[] { "A", "A" } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

            Assert.AreEqual("A", indexes[0].Value);
            Assert.AreEqual("A", indexes[1].Value);
        }

        [Test]
        public void GetIndexes_WhenItemWithEnumerableWithTwoEqualElements_ReturnsTwoIntIndexes()
        {
            var item = new WithArray { IntValues = new[] { 42, 42 } };
            var schemaStub = StructureSchemaTestFactory.CreateRealFrom<WithArray>();

            var factory = new StructureIndexesFactory();
            var indexes = factory.CreateIndexes(schemaStub, item, _structureIdGenerator.CreateId(_structureSchema)).ToList();

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