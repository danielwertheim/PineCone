using System;
using NUnit.Framework;
using PineCone.Resources;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class IdAccessorTests : UnitTestBase
    {
        [Test]
        public void GetValue_FromAssignedGuidProperty_ReturnsAssignedGuid()
        {
            var id = Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf");
            var item = new GuidDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<GuidDummy>();
            
            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue<GuidDummy>(item);

            Assert.AreEqual(id, idViaAccessor);
        }

        [Test]
        public void GetValue_FromAssignedNullableGuidProperty_ReturnsAssignedGuid()
        {
            var id = Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf");
            var item = new NullableGuidDummy { StructureId = id };
            var property = StructurePropertyTestFactory.GetIdProperty<NullableGuidDummy>();
            
            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.AreEqual(id, idViaAccessor);
        }

        [Test]
        public void GetValue_FromUnAssignedNullableGuidProperty_ReturnsNull()
        {
            var item = new NullableGuidDummy();
            var property = StructurePropertyTestFactory.GetIdProperty<NullableGuidDummy>();
            
            var idAccessor = new IdAccessor(property);
            var idViaAccessor = idAccessor.GetValue(item);

            Assert.IsNull(idViaAccessor);
        }

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
        public void SetValue_ToGuidProperty_ValueIsAssigned()
        {
            var id = Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf");
            var item = new GuidDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<GuidDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id, item.StructureId);
        }

       [Test]
        public void SetValue_ToNullableGuidProperty_ValueIsAssigned()
        {
            var id = Guid.Parse("fc47a673-5a5b-419b-9a40-a756591aa7bf");
            var item = new NullableGuidDummy();

            var property = StructurePropertyTestFactory.GetIdProperty<NullableGuidDummy>();
            var idAccessor = new IdAccessor(property);
            idAccessor.SetValue(item, id);

            Assert.AreEqual(id, item.StructureId);
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
    }
}