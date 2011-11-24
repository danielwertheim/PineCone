using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class GuidStructureIdGeneratorTests : UnitTestBase
    {
        private Func<int, IEnumerable<IStructureId>> _structureIdGenerator;

        protected override void OnFixtureInitialize()
        {
            _structureIdGenerator = GenerateIds;
        }

        private IEnumerable<IStructureId> GenerateIds(int numOfIds)
        {
            for (var c = 0; c < numOfIds; c++)
                yield return StructureId.Create(SequentialGuid.New());
        }

        [Test]
        public void CreateIds_WhenNumOfIdsIsPositiveInteger_ReturnsCorrectNumberOfSequentialGuids()
        {
            var numOfIds = 10;

            var ids = _structureIdGenerator.Invoke(numOfIds)
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
            var ids = _structureIdGenerator.Invoke(0).ToArray();

            Assert.AreEqual(0, ids.Length);
        }

        [Test]
        public void CreateIds_ReturnsGuids()
        {
            var ids = _structureIdGenerator.Invoke(10).Select(id => id.Value);

            CollectionAssert.AllItemsAreInstancesOfType(ids, typeof(Guid));
        }
    }
}