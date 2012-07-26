using System;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterTimeStampPropertyTests : StructureTypeReflecterTestsBase
    {
        [Test]
        public void HasTimeStampProperty_WhenMemberExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<Model>().HasTimeStampProperty());
        }

        [Test]
        public void HasTimeStampProperty_WhenClassNamedMemberExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<ModelWithModelTimeStamp>().HasTimeStampProperty());
        }

        [Test]
        public void HasTimeStampProperty_WhenInterfaceNamedMemberExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<IModel>().HasTimeStampProperty());
        }

        [Test]
        public void HasTimeStampProperty_WhenStructureNamedMemberExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<ModelWithStructureTimeStamp>().HasTimeStampProperty());
        }

        [Test]
        public void HasTimeStampProperty_WhenModelWithNullableMemberExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<ModelWithNullableTimeStamp>().HasTimeStampProperty());
        }

        [Test]
        public void HasTimeStampProperty_WhenMemberDoesNotExists_ReturnsFalse()
        {
            Assert.IsFalse(ReflecterFor<ModelWithNoTimeStamp>().HasTimeStampProperty());
        }

        [Test]
        public void GetTimeStampProperty_WhenMemberExists_ReturnsProperty()
        {
            var property = ReflecterFor<Model>().GetTimeStampProperty();

            Assert.IsNotNull(property);
            Assert.AreEqual("TimeStamp", property.Name);
        }

        [Test]
        public void GetTimeStampProperty_WhenMemberDoesNotExist_ReturnsNull()
        {
            var property = ReflecterFor<ModelWithNoTimeStamp>().GetTimeStampProperty();

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