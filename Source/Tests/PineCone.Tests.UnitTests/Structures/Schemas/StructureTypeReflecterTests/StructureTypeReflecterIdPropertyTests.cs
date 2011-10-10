using System;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterIdPropertyTests : UnitTestBase
    {
        private readonly IStructureTypeReflecter _reflecter = new StructureTypeReflecter();

        [Test]
        public void HasIdProperty_WhenGuidIdPropertyExists_ReturnsTrue()
        {
            var type = typeof(WithGuidId);
            var reflecter = new StructureTypeReflecter();

            Assert.IsTrue(reflecter.HasIdProperty(type));
        }

        [Test]
        public void HasIdProperty_WhenIdentityPropertyExists_ReturnsTrue()
        {
            var type = typeof(WithIntId);
            var reflecter = new StructureTypeReflecter();

            Assert.IsTrue(reflecter.HasIdProperty(type));
        }

        [Test]
        public void HasIdProperty_WhenNullableGuidIdPropertyExists_ReturnsTrue()
        {
            var type = typeof(WithNullableGuidId);
            var reflecter = new StructureTypeReflecter();

            Assert.IsTrue(reflecter.HasIdProperty(type));
        }

        [Test]
        public void HasIdProperty_WhenNullableIdentityPropertyExists_ReturnsTrue()
        {
            var type = typeof(WithNullableIntId);
            var reflecter = new StructureTypeReflecter();

            Assert.IsTrue(reflecter.HasIdProperty(type));
        }

        [Test]
        public void HasIdProperty_WhenIdPropertyDoesNotExist_ReturnsTrue()
        {
            var type = typeof(WithNoId);
            var reflecter = new StructureTypeReflecter();

            Assert.IsFalse(reflecter.HasIdProperty(type));
        }

        [Test]
        public void GetIdProperty_WhenPublicGuidIdProperty_ReturnsProperty()
        {
            var property = _reflecter.GetIdProperty(typeof (WithGuidId));

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPublicNullableGuidIdProperty_ReturnsProperty()
        {
            var property = _reflecter.GetIdProperty(typeof (WithNullableGuidId));

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPrivateGuidIdProperty_ReturnsNull()
        {
            var property = _reflecter.GetIdProperty(typeof (WithPrivateGuidId));

            Assert.IsNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPublicIntIdProperty_ReturnsProperty()
        {
            var property = _reflecter.GetIdProperty(typeof (WithIntId));

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPublicNullableIntIdProperty_ReturnsProperty()
        {
            var property = _reflecter.GetIdProperty(typeof (WithNullableIntId));

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPrivateIntIdProperty_ReturnsNull()
        {
            var property = _reflecter.GetIdProperty(typeof(WithPrivateIntId));

            Assert.IsNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPublicLongIdProperty_ReturnsProperty()
        {
            var property = _reflecter.GetIdProperty(typeof(WithLongId));

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPublicNullableLongIdProperty_ReturnsProperty()
        {
            var property = _reflecter.GetIdProperty(typeof(WithNullableLongId));

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIdProperty_WhenPrivateLongIdProperty_ReturnsNull()
        {
            var property = _reflecter.GetIdProperty(typeof(WithPrivateLongId));

            Assert.IsNull(property);
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
    }
}