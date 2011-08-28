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
            var guid = Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887");

            var structure1 = new StructureIndex(guid, "TheName", "TheValue");
            var structure2 = new StructureIndex(guid, "TheName", "TheValue");

            Assert.AreEqual(structure1, structure2);
        }

        [Test]
        public void Equals_WhenDifferentGuidStructureId_ReturnsFalse()
        {
            var guid1 = Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887");
            var guid2 = Guid.Parse("14D4D3EC-6E1E-4839-ACC7-EA3B4653CF96");

            var structure1 = new StructureIndex(guid1, "TheName", "TheValue");
            var structure2 = new StructureIndex(guid2, "TheName", "TheValue");

            Assert.AreNotEqual(structure1, structure2);
        }

        [Test]
        public void Equals_WhenDifferentName_ReturnsFalse()
        {
            var guid1 = Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887");

            var structure1 = new StructureIndex(guid1, "TheName", "TheValue");
            var structure2 = new StructureIndex(guid1, "OtherName", "TheValue");

            Assert.AreNotEqual(structure1, structure2);
        }

        [Test]
        public void Equals_WhenDifferentValue_ReturnsFalse()
        {
            var guid1 = Guid.Parse("06E2FC67-AB9F-4E65-A2C8-5FC897597887");

            var structure1 = new StructureIndex(guid1, "TheName", "TheValue");
            var structure2 = new StructureIndex(guid1, "TheName", "OtherValue");

            Assert.AreNotEqual(structure1, structure2);
        }
    }
}