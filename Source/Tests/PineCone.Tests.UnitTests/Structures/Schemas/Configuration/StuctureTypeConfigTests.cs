using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone.Tests.UnitTests.Structures.Schemas.Configuration
{
    [TestFixture]
    public class StuctureTypeConfigTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenMissingType_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new StructureTypeConfig(null));

            Assert.AreEqual("structureType", ex.ParamName);
        }

        [Test]
        public void Ctor_WhenPassingType_TypePropertyIsAssigned()
        {
            var expectedType = typeof (Dummy);
            var config = new StructureTypeConfig(expectedType);

            Assert.AreEqual(expectedType, config.Type);
        }

        [Test]
        public void IsEmpty_WhenNothingIsRegistrered_ReturnsTrue()
        {
            var config = new StructureTypeConfig(typeof (Dummy));

            Assert.IsTrue(config.IsEmpty);
        }

        [Test]
        public void IsEmpty_WhenMembersAreExcluded_ReturnsFalse()
        {
            var config = new StructureTypeConfig(typeof(Dummy));
            config.MemberPathsNotBeingIndexed.Add("Temp");

            Assert.IsFalse(config.IsEmpty);
        }

        [Test]
        public void IsEmpty_WhenMembersAreIncluded_ReturnsFalse()
        {
            var config = new StructureTypeConfig(typeof(Dummy));
            config.MemberPathsBeingIndexed.Add("Temp");

            Assert.IsFalse(config.IsEmpty);
        }

        private class Dummy {}
    }
}