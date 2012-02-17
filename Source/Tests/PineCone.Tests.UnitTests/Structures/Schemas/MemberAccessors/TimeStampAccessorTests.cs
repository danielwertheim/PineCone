using System;
using NCore;
using NUnit.Framework;
using PineCone.Resources;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class TimeStampAccessorTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenMemberIsNotOnRootLevel_ThrowsException()
        {
            var itemPropertyInfo = typeof(ModelWithMemberNotInRoot).GetProperty("NestedModelItem");
            var itemProperty = StructureProperty.CreateFrom(itemPropertyInfo);

            var timeStampPropertyInfo = typeof(ModelWithDateTime).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(itemProperty, timeStampPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new TimeStampAccessor(timeStampProperty));

            Assert.AreEqual(ExceptionMessages.TimeStampAccessor_InvalidLevel.Inject(timeStampProperty.Name), ex.Message);
        }

        [Test]
        public void Ctor_WhenMemberIsNotDateTime_ThrowsException()
        {
            var timeStampPropertyInfo = typeof(ModelWithStringMember).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(timeStampPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new TimeStampAccessor(timeStampProperty));

            Assert.AreEqual(ExceptionMessages.TimeStampAccessor_Invalid_Type.Inject(timeStampProperty.Name), ex.Message);
        }

        [Test]
        public void GetValue_WhenNoAssignedDateTimeExists_ReturnsMinValueDateTime()
        {
            var timeStampPropertyInfo = typeof(ModelWithDateTime).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(timeStampPropertyInfo);
            var accessor = new TimeStampAccessor(timeStampProperty);
            var initialValue = default(DateTime);
            var model = new ModelWithDateTime { TimeStamp = initialValue };

            var timeStamp = accessor.GetValue(model);

            Assert.AreEqual(initialValue, timeStamp);
        }

        [Test]
        public void GetValue_WhenAssignedDateTimeExists_ReturnsAssignedValue()
        {
            var timeStampPropertyInfo = typeof(ModelWithDateTime).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(timeStampPropertyInfo);
            var accessor = new TimeStampAccessor(timeStampProperty);
            var initialValue = new DateTime(1970, 12, 13, 01, 02, 03);
            var model = new ModelWithDateTime { TimeStamp = initialValue };

            var timeStamp = accessor.GetValue(model);

            Assert.AreEqual(initialValue, timeStamp);
        }

        [Test]
        public void GetValue_WhenNoAssignedNullableDateTimeExists_ReturnsNulledNullableDateTime()
        {
            var timeStampPropertyInfo = typeof(ModelWithNullableDateTime).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(timeStampPropertyInfo);
            var accessor = new TimeStampAccessor(timeStampProperty);
            var model = new ModelWithNullableDateTime { TimeStamp = null };

            var timeStamp = accessor.GetValue(model);

            Assert.IsNull(timeStamp);
        }

        [Test]
        public void GetValue_WhenAssignedNullableDateTimeExists_ReturnsAssignedValue()
        {
            var timeStampPropertyInfo = typeof(ModelWithNullableDateTime).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(timeStampPropertyInfo);
            var accessor = new TimeStampAccessor(timeStampProperty);
            var initialValue = new DateTime(1970, 12, 13, 01, 02, 03);
            var model = new ModelWithNullableDateTime { TimeStamp = initialValue };

            var timeStamp = accessor.GetValue(model);

            Assert.AreEqual(initialValue, timeStamp);
        }

        [Test]
        public void SetValue_WhenAssigningDateTime_UpdatesValue()
        {
            var timeStampPropertyInfo = typeof(ModelWithDateTime).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(timeStampPropertyInfo);
            var accessor = new TimeStampAccessor(timeStampProperty);
            var initialValue = new DateTime(1970, 12, 13, 01, 02, 03);
            var assignedValue = initialValue.AddDays(1);
            var model = new ModelWithDateTime { TimeStamp = initialValue };

            accessor.SetValue(model, assignedValue);

            Assert.AreEqual(assignedValue, model.TimeStamp);
        }

        [Test]
        public void SetValue_WhenAssigningValueToNullableDateTime_UpdatesValue()
        {
            var timeStampPropertyInfo = typeof(ModelWithNullableDateTime).GetProperty("TimeStamp");
            var timeStampProperty = StructureProperty.CreateFrom(timeStampPropertyInfo);
            var accessor = new TimeStampAccessor(timeStampProperty);
            var initialValue = new DateTime(1970, 12, 13, 01, 02, 03);
            var assignedValue = initialValue.AddDays(1);
            var model = new ModelWithNullableDateTime { TimeStamp = initialValue };

            accessor.SetValue(model, assignedValue);

            Assert.AreEqual(assignedValue, model.TimeStamp);
        }

        private class ModelWithDateTime
        {
            public DateTime TimeStamp { get; set; }
        }

        private class ModelWithNullableDateTime
        {
            public DateTime? TimeStamp { get; set; }
        }

        private class ModelWithStringMember
        {
            public string TimeStamp { get; set; }
        }

        private class ModelWithMemberNotInRoot
        {
            public ModelWithDateTime NestedModelItem { get; set; }
        }
    }
}