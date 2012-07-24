using System;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterConcurrencyTokenPropertyTests : UnitTestBase
    {
        [Test]
        public void HasConcurrencyTokenProperty_WhenMemberExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<Model>().HasConcurrencyTokenProperty());
        }

        [Test]
        public void HasConcurrencyTokenProperty_WhenMemberDoesNotExists_ReturnsFalse()
        {
            Assert.IsFalse(ReflecterFor<ModelWithNoToken>().HasConcurrencyTokenProperty());
        }

        [Test]
        public void GetConcurrencyTokenProperty_WhenMemberExists_ReturnsProperty()
        {
            var property = ReflecterFor<Model>().GetConcurrencyTokenProperty();

            Assert.IsNotNull(property);
            Assert.AreEqual("ConcurrencyToken", property.Name);
        }

        [Test]
        public void GetConcurrencyTokenProperty_WhenMemberDoesNotExist_ReturnsNull()
        {
            var property = ReflecterFor<ModelWithNoToken>().GetConcurrencyTokenProperty();

            Assert.IsNull(property);
        }

        private static IStructureTypeReflecter ReflecterFor<T>() where T : class
        {
            return new StructureTypeReflecter(typeof(T));
        }

        private class Model
        {
            public Guid ConcurrencyToken { get; set; }
        }

        private class ModelWithNoToken
        {

        }
    }
}