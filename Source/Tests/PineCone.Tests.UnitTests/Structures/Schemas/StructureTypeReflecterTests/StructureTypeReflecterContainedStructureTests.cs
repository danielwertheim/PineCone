using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterContainedStructureTests : UnitTestBase
    {
        [Test]
        public void GetIndexableProperties_When_contained_structure_Contained_members_are_not_extracted()
        {
            var properties = ReflecterFor<WithContainedStructure>().GetIndexableProperties();

            Assert.IsFalse(properties.Any());
        }

        [Test]
        public void GetIndexableProperties_When_contained_structure_and_contained_structures_are_allowed_Contained_members_are_extracted()
        {
            var properties = ReflecterFor<WithContainedStructure>().GetIndexableProperties();

            Assert.AreEqual(1, properties.Count());
            Assert.IsNotNull(properties.SingleOrDefault(p => p.Path == "Contained.NestedValue"));
        }

        private static IStructureTypeReflecter ReflecterFor<T>() where T : class
        {
            return new StructureTypeReflecter(typeof(T));
        }

        private class WithContainedStructure
        {
            public Structure Contained { get; set; }
        }

        private class Structure
        {
            public int StructureId { get; set; }
            public int NestedValue { get; set; }
        }
    }
}