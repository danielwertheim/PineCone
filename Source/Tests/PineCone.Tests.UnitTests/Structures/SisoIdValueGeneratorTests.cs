using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureIdValueGeneratorTests : UnitTestBase
    {
        [Test]
        public void CreateGuidIds_WhenNumOfIdsIsPositiveInteger_ReturnsCorrectNumberOfSequentialGuids()
        {
            var numOfIds = 10;

            var ids = StructureIdGenerator.CreateIds(numOfIds).Select(id => id.ToString("D")).ToArray();
            
            var orderedIds = ids.OrderBy(id => id).ToArray();

            Assert.AreEqual(numOfIds, ids.Length);
            CollectionAssert.AreEqual(orderedIds, ids);
        }

        [Test]
        public void CreateGuidIds_WhenZeroIsPassedForNumOfIds_ReturnsZeroLenghtArray()
        {
            var ids = StructureIdGenerator.CreateIds(0);

            Assert.AreEqual(0, ids.Length);
        }

        [Test]
        public void CreateGuidIds_ReturnsGuids()
        {
            var ids = StructureIdGenerator.CreateIds(10);

            CollectionAssert.AllItemsAreInstancesOfType(ids, typeof(Guid));
        }
    }
}