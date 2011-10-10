using System;
using NUnit.Framework;
using PineCone.Resources;
using PineCone.Structures;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class IdAccessorTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenIntNotOnFirstLevel_ThrowsPineConeException()
        {
            var itemPropertyInfo = typeof(Container).GetProperty("NestedWithIdentity");
            var itemProperty = StructureProperty.CreateFrom(itemPropertyInfo);

            var intPropertyInfo = typeof(IdentityDummy).GetProperty("StructureId");
            var intProperty = StructureProperty.CreateFrom(itemProperty, intPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new IdAccessor(intProperty));

            Assert.AreEqual(ExceptionMessages.IdAccessor_GetIdValue_InvalidLevel, ex.Message);
        }

        [Test]
        public void Ctor_WhenGuidNotOnFirstLevel_ThrowsPineConeException()
        {
            var itemPropertyInfo = typeof(Container).GetProperty("NestedWithGuid");
            var itemProperty = StructureProperty.CreateFrom(itemPropertyInfo);

            var guidPropertyInfo = typeof(GuidDummy).GetProperty("StructureId");
            var guidProperty = StructureProperty.CreateFrom(itemProperty, guidPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new IdAccessor(guidProperty));

            Assert.AreEqual(ExceptionMessages.IdAccessor_GetIdValue_InvalidLevel, ex.Message);
        }

        [Test]
        public void GetValue_FromAssignedGuidProperty_ReturnsAssignedGuid()
        {
            var id = Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf");
            var item = new GuidDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<GuidDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue<GuidDummy>(item);

            Assert.AreEqual(id, idViaAccessor.Value);
        }

        [Test]
        public void GetValue_FromAssignedNullableGuidProperty_ReturnsAssignedGuid()
        {
            var id = Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf");
            var item = new NullableGuidDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<NullableGuidDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.AreEqual(id, idViaAccessor.Value);
        }

        [Test]
        public void GetValue_FromUnAssignedNullableGuidProperty_ReturnsNull()
        {
            var item = new NullableGuidDummy();
            var property = StructurePropertyTestFactory.GetIdProperty<NullableGuidDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.IsFalse(idViaAccessor.HasValue);
        }

        [Test]
        public void SetValue_ToGuidProperty_ValueIsAssigned()
        {
            var id = StructureId.Create(Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf"));
            var item = new GuidDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<GuidDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id.Value, item.StructureId);
        }

        [Test]
        public void SetValue_ToNullableGuidProperty_ValueIsAssigned()
        {
            var id = StructureId.Create(Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf"));
            var item = new NullableGuidDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<NullableGuidDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id.Value, item.StructureId);
        }

        [Test]
        public void GetValue_FromAssignedIntIdentityProperty_ReturnsAssignedValue()
        {
            var id = 42;
            var item = new IdentityDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<IdentityDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue<IdentityDummy>(item);

            Assert.AreEqual(id, idViaAccessor.Value);
        }

        [Test]
        public void GetValue_FromAssignedNullableIntIdentityProperty_ReturnsAssignedValue()
        {
            var id = 42;
            var item = new NullableIdentityDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<NullableIdentityDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.AreEqual(id, idViaAccessor.Value);
        }

        [Test]
        public void GetValue_FromUnAssignedNullableIntIdentityProperty_ReturnsNull()
        {
            var item = new NullableIdentityDummy();
            var property = StructurePropertyTestFactory.GetIdProperty<NullableIdentityDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.IsFalse(idViaAccessor.HasValue);
        }

        [Test]
        public void SetValue_ToIntIdentityProperty_ValueIsAssigned()
        {
            var id = StructureId.Create(42);
            var item = new IdentityDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<IdentityDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id.Value, item.StructureId);
        }

        [Test]
        public void SetValue_ToNullableIntIdentityProperty_ValueIsAssigned()
        {
            var id = StructureId.Create(42);
            var item = new NullableIdentityDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<NullableIdentityDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id.Value, item.StructureId);
        }

        [Test]
        public void GetValue_FromAssignedLongIdentityProperty_ReturnsAssignedValue()
        {
            var id = 42;
            var item = new BigIdentityDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<BigIdentityDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue<BigIdentityDummy>(item);

            Assert.AreEqual(id, idViaAccessor.Value);
        }

        [Test]
        public void GetValue_FromAssignedNullableLongIdentityProperty_ReturnsAssignedValue()
        {
            var id = 42;
            var item = new NullableBigIdentityDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<NullableBigIdentityDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.AreEqual(id, idViaAccessor.Value);
        }

        [Test]
        public void GetValue_FromUnAssignedNullableLongIdentityProperty_ReturnsNull()
        {
            var item = new NullableBigIdentityDummy();
            var property = StructurePropertyTestFactory.GetIdProperty<NullableBigIdentityDummy>();

            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.IsFalse(idViaAccessor.HasValue);
        }

        [Test]
        public void SetValue_of_long_ToLongIdentityProperty_ValueIsAssigned()
        {
            var id = StructureId.Create((long)42);
            var item = new BigIdentityDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<BigIdentityDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id.Value, item.StructureId);
        }

        [Test]
        public void SetValue_of_long_ToNullableLongIdentityProperty_ValueIsAssigned()
        {
            var id = StructureId.Create((long)42);
            var item = new NullableBigIdentityDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<NullableBigIdentityDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id.Value, item.StructureId);
        }
        
        private class Container
        {
            public IdentityDummy NestedWithIdentity { get; set; }

            public GuidDummy NestedWithGuid { get; set; }
        }

        private class GuidDummy
        {
            public Guid StructureId { get; set; }
        }

        private class NullableGuidDummy
        {
            public Guid? StructureId { get; set; }
        }

        private class IdentityDummy
        {
            public int StructureId { get; set; }
        }

        private class NullableIdentityDummy
        {
            public int? StructureId { get; set; }
        }

        private class BigIdentityDummy
        {
            public long StructureId { get; set; }
        }

        private class NullableBigIdentityDummy
        {
            public long? StructureId { get; set; }
        }
    }
}