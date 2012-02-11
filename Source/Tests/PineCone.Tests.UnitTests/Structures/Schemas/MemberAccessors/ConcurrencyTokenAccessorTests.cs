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

            var concTokenPropertyInfo = typeof(Model).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(itemProperty, concTokenPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new ConcurrencyTokenAccessor(concTokenProperty));

            Assert.AreEqual(ExceptionMessages.ConcurrencyTokenAccessor_InvalidLevel.Inject(concTokenProperty.Name), ex.Message);
        }

        [Test]
        public void Ctor_WhenMemberIsNotGuid_ThrowsException()
        {
            var concTokenPropertyInfo = typeof(ModelWithNonGuidMember).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);

            var ex = Assert.Throws<PineConeException>(() => new ConcurrencyTokenAccessor(concTokenProperty));

            Assert.AreEqual(ExceptionMessages.ConcurrencyTokenAccessor_Invalid_Type.Inject(concTokenProperty.Name), ex.Message);
        }

        [Test]
        public void GetValue_WhenAssignedGuidExistsInModel_ReturnsGuid()
        {
            var concTokenPropertyInfo = typeof(Model).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = Guid.Parse("de7d7fcb-ccd0-46d2-b3e2-cd4a357c697f");
            var model = new Model {ConcurrencyToken = initialToken};

            var token = accessor.GetValue(model);

            Assert.AreEqual(initialToken, token);
        }

        [Test]
        public void SetValue_WhenAssigningNewGuidOnModel_UpdatesGuidOnModel()
        {
            var concTokenPropertyInfo = typeof(Model).GetProperty("ConcurrencyToken");
            var concTokenProperty = StructureProperty.CreateFrom(concTokenPropertyInfo);
            var accessor = new ConcurrencyTokenAccessor(concTokenProperty);
            var initialToken = Guid.Parse("de7d7fcb-ccd0-46d2-b3e2-cd4a357c697f");
            var assignedToken = Guid.Parse("f13185dd-1145-4e63-a53f-a0e22dda3e03");
            var model = new Model { ConcurrencyToken = initialToken };

            accessor.SetValue(model, assignedToken);

            Assert.AreEqual(assignedToken, model.ConcurrencyToken);
        }

        private class Model
        {
            public Guid ConcurrencyToken { get; set; }
        }

        private class ModelWithNonGuidMember
        {
            public int ConcurrencyToken { get; set; }
        }

        private class ModelWithMemberNotInRoot
        {
            public Model NestedModelItem { get; set; }
        }
    }
}