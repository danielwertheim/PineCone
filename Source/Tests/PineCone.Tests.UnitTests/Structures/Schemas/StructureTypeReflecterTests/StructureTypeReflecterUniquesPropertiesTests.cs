using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PineCone.Annotations;
using PineCone.Structures.Schemas;
using PineCone.TestUtils;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterUniquesPropertiesTests : UnitTestBase
    {
        private readonly IStructureTypeReflecter _reflecter = new StructureTypeReflecter();

        [Test]
        public void GetIndexableProperties_WhenSimpleUniquesExistsOnRoot_ReturnsSimpleUniques()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithSimpleUniques));

            Assert.AreEqual(2, properties.Count());

            CustomAssert.Exists(properties, p => p.Name == "UqIntOnLevel1");
            CustomAssert.Exists(properties, p => p.Name == "UqStringOnLevel1");
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithExplicitUniqueOnChildWithNoUniques_ThrowsPineConeException()
        {
            Assert.Throws<PineConeException>(
                () => _reflecter.GetIndexableProperties(typeof(WithExplicitUniqueOnChildWithoutUniques)));
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithExplicitUniqueOnChildWithUniques_ThrowsPineConeException()
        {
            Assert.Throws<PineConeException>(
                () => _reflecter.GetIndexableProperties(typeof(WithExplicitUniqueOnChildWithUniques)));
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithImplicitUniqueOnChildWithUniques_ChildUniquesAreExtracted()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithImplicitUniqueOnChildWithUniques));

            var uniques = properties.Where(p => p.IsUnique).ToList();
            Assert.AreEqual(1, uniques.Count);
            Assert.AreEqual("Child.Code", uniques[0].Path);
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithUniqueEnumerableOfSimple_ThrowsPineConeException()
        {
            Assert.Throws<PineConeException>(
                () => _reflecter.GetIndexableProperties(typeof(WithUniqueEnumerableOfSimple)));
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithUniqueEnumerableOfComplexWithUnique_ThrowsPineConeException()
        {
            Assert.Throws<PineConeException>(
                () => _reflecter.GetIndexableProperties(typeof(WithUniqueEnumerableOfComplexWithUnique)));
        }

        [Test]
        public void GetIndexableProperties_WhenRootWithEnumerableOfComplexWithUnique_UniqueIsExtracted()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithEnumerableOfComplexWithUnique));

            var uniques = properties.Where(p => p.IsUnique).ToList();
            Assert.AreEqual(1, uniques.Count);
            Assert.AreEqual("Items.Code", uniques[0].Path);
        }

        private class WithSimpleUniques
        {
            [Unique(UniqueMode.PerType)]
            public int UqIntOnLevel1 { get; set; }

            [Unique(UniqueMode.PerInstance)]
            public string UqStringOnLevel1 { get; set; }
        }

        private class WithExplicitUniqueOnChildWithoutUniques
        {
            [Unique(UniqueMode.PerInstance)]
            public ChildWithoutUnique Child { get; set; }
        }

        private class WithExplicitUniqueOnChildWithUniques
        {
            [Unique(UniqueMode.PerInstance)]
            public ChildWithUnique Child { get; set; }
        }

        private class WithImplicitUniqueOnChildWithUniques
        {
            public ChildWithUnique Child { get; set; }
        }

        private class WithUniqueEnumerableOfSimple
        {
            [Unique(UniqueMode.PerInstance)]
            public IEnumerable<string> Items { get; set; }
        }

        private class WithUniqueEnumerableOfComplexWithUnique
        {
            [Unique(UniqueMode.PerInstance)]
            public IEnumerable<ChildWithUnique> Items { get; set; }
        }

        private class WithEnumerableOfComplexWithUnique
        {
            public IEnumerable<ChildWithUnique> Items { get; set; }
        }

        private class ChildWithoutUnique
        {
            public string Name { get; set; }
        }

        private class ChildWithUnique
        {
            [Unique(UniqueMode.PerInstance)]
            public int Code { get; set; }
        }
    }
}