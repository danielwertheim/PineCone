using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterIdPropertyTests : StructureTypeReflecterTestsBase
    {
        [Test]
        public void HasIdProperty_WhenGuidIdPropertyExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<WithGuidId>().HasIdProperty());
        }

        [Test]
        public void HasIdProperty_WhenIdentityPropertyExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<WithIntId>().HasIdProperty());
        }

        [Test]
        public void HasIdProperty_WhenNullableGuidIdPropertyExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<WithNullableGuidId>().HasIdProperty());
        }

        [Test]
        public void HasIdProperty_WhenNullableIdentityPropertyExists_ReturnsTrue()
        {
            Assert.IsTrue(ReflecterFor<WithNullableIntId>().HasIdProperty());
        }

        [Test]
        public void HasIdProperty_WhenIdPropertyDoesNotExist_ReturnsFalse()
        {
            Assert.IsFalse(ReflecterFor<WithNoId>().HasIdProperty());
        }

		[Test]
		public void HasIdProperty_WhenIdPropertyNameIsTypeNamedId_ReturnsTrue()
		{
            Assert.IsTrue(ReflecterFor<WithCustomIdOfTypeName>().HasIdProperty());
		}

		[Test]
		public void HasIdProperty_WhenIdPropertyNameIsInterfaceNamedId_ReturnsTrue()
		{
            Assert.IsTrue(ReflecterFor<IMyType>().HasIdProperty());
		}

		[Test]
		public void HasIdProperty_WhenIdPropertyNameIsId_ReturnsTrue()
		{
            Assert.IsTrue(ReflecterFor<WithId>().HasIdProperty());
		}

        [Test]
        public void GetIdProperty_WhenPublicGuidIdProperty_ReturnsProperty()
        {
            Assert.IsNotNull(ReflecterFor<WithGuidId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPublicNullableGuidIdProperty_ReturnsProperty()
        {
            Assert.IsNotNull(ReflecterFor<WithNullableGuidId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPrivateGuidIdProperty_ReturnsNull()
        {
            Assert.IsNull(ReflecterFor<WithPrivateGuidId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPublicIntIdProperty_ReturnsProperty()
        {
            Assert.IsNotNull(ReflecterFor<WithIntId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPublicNullableIntIdProperty_ReturnsProperty()
        {
            Assert.IsNotNull(ReflecterFor<WithNullableIntId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPrivateIntIdProperty_ReturnsNull()
        {
            Assert.IsNull(ReflecterFor<WithPrivateIntId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPublicLongIdProperty_ReturnsProperty()
        {
            Assert.IsNotNull(ReflecterFor<WithLongId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPublicNullableLongIdProperty_ReturnsProperty()
        {
            Assert.IsNotNull(ReflecterFor<WithNullableLongId>().GetIdProperty());
        }

        [Test]
        public void GetIdProperty_WhenPrivateLongIdProperty_ReturnsNull()
        {
            Assert.IsNull(ReflecterFor<WithPrivateLongId>().GetIdProperty());
        }

		[Test]
		public void GetIdProperty_WhenIdPropertyNameIsTypeNamedId_ReturnsProperty()
		{
			var property = ReflecterFor<WithCustomIdOfTypeName>().GetIdProperty();

			Assert.IsNotNull(property);
			Assert.AreEqual("WithCustomIdOfTypeNameId", property.Name);
		}

		[Test]
		public void GetIdProperty_WhenIdPropertyNameIsInterfaceNamedId_ReturnsProperty()
		{
            var property = ReflecterFor<IMyType>().GetIdProperty();

			Assert.IsNotNull(property);
			Assert.AreEqual("MyTypeId", property.Name);
		}

		[Test]
		public void GetIdProperty_WhenIdPropertyNameIsId_ReturnsProperty()
		{
            var property = ReflecterFor<WithId>().GetIdProperty();

			Assert.IsNotNull(property);
			Assert.AreEqual("Id", property.Name);
		}

        [Test]
        public void GetIndexableProperties_WhenGuidIdExists_IdMemberIsReturned()
        {
            var property = ReflecterFor<WithGuidId>().GetIndexableProperties()
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenIntIdExists_IdMemberIsReturned()
        {
            var property = ReflecterFor<WithIntId>().GetIndexableProperties()
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenLongIdExists_IdMemberIsReturned()
        {
            var property = ReflecterFor<WithLongId>().GetIndexableProperties()
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenNulledNullableGuidIdExists_IdMemberIsReturned()
        {
            var property = ReflecterFor<WithNullableGuidId>().GetIndexableProperties()
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenNullableIntExists_IdMemberIsReturned()
        {
            var property = ReflecterFor<WithNullableIntId>().GetIndexableProperties()
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenNullableLongExists_IdMemberIsReturned()
        {
            var property = ReflecterFor<WithNullableLongId>().GetIndexableProperties()
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        private class WithNoId
        {}

        private class WithGuidId
        {
            public Guid StructureId { get; set; }
        }

        private class WithNullableGuidId
        {
            public Guid? StructureId { get; set; }
        }

        private class WithPrivateGuidId
        {
            private Guid StructureId { get; set; }
        }

        private class WithIntId
        {
            public int StructureId { get; set; }
        }

        private class WithNullableIntId
        {
            public int? StructureId { get; set; }
        }

        private class WithPrivateIntId
        {
            private int StructureId { get; set; }
        }

        private class WithLongId
        {
            public long StructureId { get; set; }
        }

        private class WithNullableLongId
        {
            public long? StructureId { get; set; }
        }

        private class WithPrivateLongId
        {
            private long StructureId { get; set; }
        }

		private class WithCustomIdOfTypeName
		{
			public Guid WithCustomIdOfTypeNameId { get; set; }
		}

		private interface IMyType
		{
			Guid MyTypeId { get; set; } 
		}

		private class WithId
		{
			public Guid Id { get; set; }
		}
    }
}