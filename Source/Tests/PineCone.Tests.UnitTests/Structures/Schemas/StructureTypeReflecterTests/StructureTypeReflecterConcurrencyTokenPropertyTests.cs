using System;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterConcurrencyTokenPropertyTests : UnitTestBase
    {
        private readonly IStructureTypeReflecter _reflecter = new StructureTypeReflecter();

        [Test]
        public void HasConcurrencyTokenProperty_WhenMemberExists_ReturnsTrue()
        {
            var type = typeof (Model);

            Assert.IsTrue(_reflecter.HasConcurrencyTokenProperty(type));
        }

        [Test]
        public void HasConcurrencyTokenProperty_WhenMemberDoesNotExists_ReturnsFalse()
        {
            var type = typeof(ModelWithNoToken);

            Assert.IsFalse(_reflecter.HasConcurrencyTokenProperty(type));
        }

        [Test]
        public void GetConcurrencyTokenProperty_WhenMemberExists_ReturnsProperty()
        {
            var type = typeof(Model);

            var property = _reflecter.GetConcurrencyTokenProperty(type);
            
            Assert.IsNotNull(property);
            Assert.AreEqual("ConcurrencyToken", property.Name);
        }

        [Test]
        public void GetConcurrencyTokenProperty_WhenMemberDoesNotExist_ReturnsNull()
        {
            var type = typeof(ModelWithNoToken);

            var property = _reflecter.GetConcurrencyTokenProperty(type);

            Assert.IsNull(property);
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