using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PineCone.Dynamic;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
    [TestFixture]
    public class DynamicStructureBuilderDynamicTests : UnitTestBase
    {
        private DynamicStructureBuilder _builder;

        protected override void OnTestInitialize()
        {
            _builder = new DynamicStructureBuilder(new GuidStructureIdGenerator());
        }

        [Test]
        public void CreateStructure_WhenDynamicStrutureWithNoIdAssigned_GetsIdAssigned()
        {
            dynamic item = new DynamicStructure("Test");
            item.StructureId = Guid.Empty;

            var structure = _builder.CreateStructure(item);

            Assert.AreNotEqual(Guid.Empty, structure.Id);
            Assert.AreEqual(item.StructureId, structure.Id);
        }

        [Test]
        public void CreateStructure_WhenDynamicStructureWithInt_IndexForIntGetsCreated()
        {
            dynamic item = new DynamicStructure("Test");
            item.IntValue = 42;

            var structure = _builder.CreateStructure(item);

            Assert.AreEqual(42, structure.Indexes[0].Value);
        }

        [Test]
        public void CreateStructure_WhenDynamicStructureWithEnumerableInts_IndexForIntGetsCreated()
        {
            dynamic item = new DynamicStructure("Test");
            item.IntValues = new List<int> { 1, 2, 3 };

            var structure = _builder.CreateStructure((DynamicStructure)item);
            
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, structure.Indexes.Select(i => i.Value));
        }
    }
}