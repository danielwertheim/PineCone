using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class GuidStructureIdGeneratorTests : UnitTestBase
    {
        private IStructureIdGenerator _structureIdGenerator;

        protected override void OnFixtureInitialize()
        {
            _structureIdGenerator = new GuidStructureIdGenerator();
        }

        [Test]
        public void CreateIds_WhenNumOfIdsIsPositiveInteger_ReturnsCorrectNumberOfSequentialGuids()
        {
            var numOfIds = 10;

            var ids = _structureIdGenerator.CreateIds(numOfIds)
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
            var ids = _structureIdGenerator.CreateIds(0).ToArray();

            Assert.AreEqual(0, ids.Length);
        }

        [Test]
        public void CreateIds_ReturnsGuids()
        {
            var ids = _structureIdGenerator.CreateIds(10).Select(id => id.Value);

            CollectionAssert.AllItemsAreInstancesOfType(ids, typeof(Guid));
        }
    }
}