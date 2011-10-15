using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.Builders;

namespace PineCone.Tests.UnitTests.Structures.Schemas
{
    [TestFixture]
    public class StructureSchemasTests : UnitTestBase
    {
        [Test]
        public void GetSchema_InvokedUsingGenerics_WillGenerateSchema()
        {
            var schemas = new StructureSchemas(new StructureTypeFactory(), new AutoSchemaBuilder());

            var schema = schemas.GetSchema<X>();

            Assert.IsNotNull(schema);
        }

        [Test]
        public void GetSchema_InvokedUsingType_WillGenerateSchema()
        {
            var schemas = new StructureSchemas(new StructureTypeFactory(), new AutoSchemaBuilder());

            var schema = schemas.GetSchema(typeof(X));

            Assert.IsNotNull(schema);
        }

        [Test]
        public void GetSchemas_WhenEmpty_YieldsEmptyStream()
        {
            var schemas = new StructureSchemas(new StructureTypeFactory(), new AutoSchemaBuilder());

            var numOfSchemas = schemas.GetSchemas().Count();

            Assert.AreEqual(0, numOfSchemas);
        }

        [Test]
        public void GetSchemas_WhenContainingASchema_YieldsOneSchema()
        {
            var schemas = new StructureSchemas(new StructureTypeFactory(), new AutoSchemaBuilder());
            var schema = schemas.GetSchema<X>();

            var fetchedSchema = schemas.GetSchemas().First();

            Assert.AreEqual(schema, fetchedSchema);
        }

        private class X
        {
            public Guid StructureId { get; set; }

            public int IntOfX { get; set; }
        }
    }
}