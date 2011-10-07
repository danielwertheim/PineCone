using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PineCone.Structures;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class GuidStructureIdGeneratorTests : UnitTestBase
    {
        private IStructureIdGenerator _structureIdGenerator;
        private IStructureSchema _structureSchema;

        protected override void OnFixtureInitialize()
        {
            _structureIdGenerator = new GuidStructureIdGenerator();
            _structureSchema = new Mock<IStructureSchema>().Object;
        }

        [Test]
        public void CreateIds_WhenNumOfIdsIsPositiveInteger_ReturnsCorrectNumberOfSequentialGuids()
        {
            var numOfIds = 10;

            var ids = _structureIdGenerator.CreateIds(numOfIds, _structureSchema)
                .Select(id => id.Value)
                .Cast<Guid>()
                .Select(id => id.ToString("D"))
                .ToArray();
            
            var orderedIds = ids.OrderBy(id => id).ToArray();

            Assert.AreEqual(numOfIds, ids.Length);
            CollectionAssert.AreEqual(orderedIds, ids);
        }

        [Test]
        public void CreateIds_WhenZeroIsPassedForNumOfIds_ReturnsZeroLenghtArray()
        {
            var ids = _structureIdGenerator.CreateIds(0, _structureSchema).ToArray();

            Assert.AreEqual(0, ids.Length);
        }

        [Test]
        public void CreateIds_ReturnsGuids()
        {
            var ids = _structureIdGenerator.CreateIds(10, _structureSchema).Select(id => id.Value);

            CollectionAssert.AllItemsAreInstancesOfType(ids, typeof(Guid));
        }
    }
}