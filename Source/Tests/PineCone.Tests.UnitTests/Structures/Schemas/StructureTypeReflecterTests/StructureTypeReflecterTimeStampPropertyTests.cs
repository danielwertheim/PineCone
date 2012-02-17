using System;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterTimeStampPropertyTests : UnitTestBase
    {
        private readonly IStructureTypeReflecter _reflecter = new StructureTypeReflecter();

        [Test]
        public void HasTimeStampProperty_WhenMemberExists_ReturnsTrue()
        {
            var type = typeof(Model);

            Assert.IsTrue(_reflecter.HasTimeStampProperty(type));
        }

        [Test]
        public void HasTimeStampProperty_WhenClassNamedMemberExists_ReturnsTrue()
        {
            var type = typeof(ModelWithModelTimeStamp);

            Assert.IsTrue(_reflecter.HasTimeStampProperty(type));
        }

        [Test]
        public void HasTimeStampProperty_WhenInterfaceNamedMemberExists_ReturnsTrue()
        {
            var type = typeof(IModel);

            Assert.IsTrue(_reflecter.HasTimeStampProperty(type));
        }

        [Test]
        public void HasTimeStampProperty_WhenStructureNamedMemberExists_ReturnsTrue()
        {
            var type = typeof(ModelWithStructureTimeStamp);

            Assert.IsTrue(_reflecter.HasTimeStampProperty(type));
        }

        [Test]
        public void HasTimeStampProperty_WhenModelWithNullableMemberExists_ReturnsTrue()
        {
            var type = typeof(ModelWithNullableTimeStamp);

            Assert.IsTrue(_reflecter.HasTimeStampProperty(type));
        }

        [Test]
        public void HasTimeStampProperty_WhenMemberDoesNotExists_ReturnsFalse()
        {
            var type = typeof(ModelWithNoTimeStamp);

            Assert.IsFalse(_reflecter.HasTimeStampProperty(type));
        }

        [Test]
        public void GetTimeStampProperty_WhenMemberExists_ReturnsProperty()
        {
            var type = typeof(Model);

            var property = _reflecter.GetTimeStampProperty(type);

            Assert.IsNotNull(property);
            Assert.AreEqual("TimeStamp", property.Name);
        }

        [Test]
        public void GetTimeStampProperty_WhenMemberDoesNotExist_ReturnsNull()
        {
            var type = typeof(ModelWithNoTimeStamp);

            var property = _reflecter.GetTimeStampProperty(type);

            Assert.IsNull(property);
        }

        private class Model
        {
            public DateTime TimeStamp { get; set; }
        }

        private class ModelWithNullableTimeStamp
        {
            public DateTime? TimeStamp { get; set; }
        }

        private class ModelWithStructureTimeStamp
        {
            public DateTime StructureTimeStamp { get; set; }
        }

        private class ModelWithModelTimeStamp
        {
            public DateTime ModelWithModelTimeStampTimeStamp { get; set; }
        }

        private interface IModel
        {
            DateTime ModelTimeStamp { get; set; }
        }

        private class ModelWithNoTimeStamp
        {

        }
    }
}