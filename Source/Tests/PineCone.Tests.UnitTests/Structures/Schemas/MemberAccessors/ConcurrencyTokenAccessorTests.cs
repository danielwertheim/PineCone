using System;
using NCore;
using NUnit.Framework;
using PineCone.Resources;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class ConcurrencyTokenAccessorTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenMemberIsNotOnRootLevel_ThrowsException()
        {
            var itemPropertyInfo = typeof(ModelWithMemberNotInRoot).GetProperty("NestedModelItem");
            var itemProperty = StructureProperty.CreateFrom(itemPropertyInfo);

            var concTokenPropertyInfo = typeof(ModelWithGuidMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(itemProperty, concTokenPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new ConcurrencyTokenAccessor(concTokenProperty));

            Assert.AreEqual(ExceptionMessages.ConcurrencyTokenAccessor_InvalidLevel.Inject(concTokenProperty.Name), ex.Message);
        }

        [Test]
        public void Ctor_WhenMemberIsNotGuidIntOrLong_ThrowsException()
        {
            var concTokenPropertyInfo = typeof(ModelWithStringMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new ConcurrencyTokenAccessor(concTokenProperty));

            Assert.AreEqual(ExceptionMessages.ConcurrencyTokenAccessor_Invalid_Type.Inject(concTokenProperty.Name), ex.Message);
        }

        [Test]
        public void GetValue_WhenAssignedGuidExistsInModel_ReturnsGuid()
        {
            var concTokenPropertyInfo = typeof(ModelWithGuidMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = Guid.Parse("de7d7fcb-ccd0-46d2-b3e2-cd4a357c697f");
            var model = new ModelWithGuidMember {ConcurrencyToken = initialToken};

            var token = accessor.GetValue(model);

            Assert.AreEqual(initialToken, token);
        }

        [Test]
        public void GetValue_WhenAssignedIntExistsInModel_ReturnsGuid()
        {
            var concTokenPropertyInfo = typeof(ModelWithIntMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = 42;
            var model = new ModelWithIntMember { ConcurrencyToken = initialToken };

            var token = accessor.GetValue(model);

            Assert.AreEqual(initialToken, token);
        }

        [Test]
        public void GetValue_WhenAssignedLongExistsInModel_ReturnsGuid()
        {
            var concTokenPropertyInfo = typeof(ModelWithBigIntMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = (long)42;
            var model = new ModelWithBigIntMember { ConcurrencyToken = initialToken };

            var token = accessor.GetValue(model);

            Assert.AreEqual(initialToken, token);
        }

        [Test]
        public void SetValue_WhenAssigningNewGuidOnModel_UpdatesGuidOnModel()
        {
            var concTokenPropertyInfo = typeof(ModelWithGuidMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = Guid.Parse("de7d7fcb-ccd0-46d2-b3e2-cd4a357c697f");
            var assignedToken = Guid.Parse("f13185dd-1145-4e63-a53f-a0e22dda3e03");
            var model = new ModelWithGuidMember { ConcurrencyToken = initialToken };

            accessor.SetValue(model, assignedToken);

            Assert.AreEqual(assignedToken, model.ConcurrencyToken);
        }

        [Test]
        public void SetValue_WhenAssigningNewIntOnModel_UpdatesGuidOnModel()
        {
            var concTokenPropertyInfo = typeof(ModelWithIntMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = 42;
            var assignedToken = 43;
            var model = new ModelWithIntMember { ConcurrencyToken = initialToken };

            accessor.SetValue(model, assignedToken);

            Assert.AreEqual(assignedToken, model.ConcurrencyToken);
        }

        [Test]
        public void SetValue_WhenAssigningNewLongOnModel_UpdatesGuidOnModel()
        {
            var concTokenPropertyInfo = typeof(ModelWithBigIntMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = (long)42;
            var assignedToken = (long)43;
            var model = new ModelWithBigIntMember { ConcurrencyToken = initialToken };

            accessor.SetValue(model, assignedToken);

            Assert.AreEqual(assignedToken, model.ConcurrencyToken);
        }

        private class ModelWithGuidMember
        {
            public Guid ConcurrencyToken { get; set; }
        }

        private class ModelWithIntMember
        {
            public int ConcurrencyToken { get; set; }
        }

        private class ModelWithBigIntMember
        {
            public long ConcurrencyToken { get; set; }
        }

        private class ModelWithStringMember
        {
            public string ConcurrencyToken { get; set; }
        }

        private class ModelWithMemberNotInRoot
        {
            public ModelWithGuidMember NestedModelItem { get; set; }
        }
    }
}