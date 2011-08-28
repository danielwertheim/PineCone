using System;
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

        private class X
        {
            public Guid StructureId { get; set; }

            public int IntOfX { get; set; }
        }
    }
}