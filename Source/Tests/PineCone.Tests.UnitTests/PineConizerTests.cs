using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PineCone.Structures;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests
{
    [TestFixture]
    public class PineConizerTests : UnitTestBase
    {
        [Test]
        public void CreateStructureFor_WillForwardCallToStructureSchemasAndBuilder()
        {
            var schemasMock = new Mock<IStructureSchemas>();
            var builderMock = new Mock<IStructureBuilder>();
            var pineConizer = new PineConizer { Schemas = schemasMock.Object, Builder = builderMock.Object };
            var item = new X { IntOfX = 1 };

            pineConizer.CreateStructureFor(item);

            schemasMock.Verify(m => m.GetSchema<X>(), Times.Once());
            builderMock.Verify(m => m.CreateStructure(item, It.IsAny<IStructureSchema>()), Times.Once());
        }
        
        [Test]
        public void CreateStructuresFor_WillForwardCallToStructureSchemas()
        {
            var schemasMock = new Mock<IStructureSchemas>();
            var builderMock = new Mock<IStructureBuilder>();
            var pineConizer = new PineConizer { Schemas = schemasMock.Object, Builder = builderMock.Object };
            var items = new[] { new X { IntOfX = 1 }, new X { IntOfX = 2 } };

            pineConizer.CreateStructuresFor(items);

            schemasMock.Verify(m => m.GetSchema<X>(), Times.Once());
            builderMock.Verify(m => m.CreateStructures(items, It.IsAny<IStructureSchema>()), Times.Once());
        }

        private class X
        {
            public Guid StructureId { get; set; }

            public int IntOfX { get; set; }
        }
    }
}