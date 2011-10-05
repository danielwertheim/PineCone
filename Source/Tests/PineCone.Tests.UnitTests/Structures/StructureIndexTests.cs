using System;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureIndexTests : UnitTestBase
    {
        [Test]
        public void Equals_WhenSameStructureIdNameAndValue_ReturnsTrue()
        {
            var structureId = new StructureId(Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887"), typeof(Guid));

            var structure1 = new StructureIndex(structureId, "TheName", "TheValue");
            var structure2 = new StructureIndex(structureId, "TheName", "TheValue");

            Assert.AreEqual(structure1, structure2);
        }

        [Test]
        public void Equals_WhenDifferentGuidStructureId_ReturnsFalse()
        {
            var structureId1 = new StructureId(Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887"), typeof(Guid));
            var structureId2 = new StructureId(Guid.Parse("14D4D3EC-6E1E-4839-ACC7-EA3B4653CF96"), typeof(Guid));

            var structure1 = new StructureIndex(structureId1, "TheName", "TheValue");
            var structure2 = new StructureIndex(structureId2, "TheName", "TheValue");

            Assert.AreNotEqual(structure1, structure2);
        }

        [Test]
        public void Equals_WhenDifferentName_ReturnsFalse()
        {
            var structureId = new StructureId(Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887"), typeof(Guid));

            var structure1 = new StructureIndex(structureId, "TheName", "TheValue");
            var structure2 = new StructureIndex(structureId, "OtherName", "TheValue");

            Assert.AreNotEqual(structure1, structure2);
        }

        [Test]
        public void Equals_WhenDifferentValue_ReturnsFalse()
        {
            var structureId = new StructureId(Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887"), typeof(Guid));

            var structure1 = new StructureIndex(structureId, "TheName", "TheValue");
            var structure2 = new StructureIndex(structureId, "TheName", "OtherValue");

            Assert.AreNotEqual(structure1, structure2);
        }
    }
}