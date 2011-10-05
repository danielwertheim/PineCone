using System;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureIdTests : UnitTestBase
    {
        [Test]
        public void Ctor_WhenGuid_ValueIsReflected()
        {
            var guid = Guid.Parse("46C72168-C637-416D-9736-751E2A17028A");

            var id = new StructureId(guid, guid.GetType());

            Assert.AreEqual(guid, id.Value);
        }

        [Test]
        public void Ctor_WhenNullGuid_HasValueIsFalse()
        {
            var id = new StructureId(null, typeof(Guid));

            Assert.AreEqual(null, id.Value);
            Assert.IsFalse(id.HasValue);
        }

        [Test]
        public void Ctor_WhenNullGuid_TypeIsCorrect()
        {
            var id = new StructureId(null, typeof(Guid));

            Assert.AreEqual(StructureIdTypes.Guid, id.IdType);
            Assert.AreEqual(typeof(Guid), id.DataType);
        }

        [Test]
        public void Ctor_WhenInt_ValueIsReflected()
        {
            var id = new StructureId(1, typeof(int));

            Assert.AreEqual(1, id.Value);
        }

        [Test]
        public void Ctor_WhenNullInt_HasValueIsFalse()
        {
            var id = new StructureId(null, typeof(int));

            Assert.AreEqual(null, id.Value);
            Assert.IsFalse(id.HasValue);
        }

        [Test]
        public void Ctor_WhenNullInt_TypeIsCorrect()
        {
            var id = new StructureId(null, typeof(int));

            Assert.AreEqual(StructureIdTypes.SmallIdentity, id.IdType);
            Assert.AreEqual(typeof(int), id.DataType);
        }

        [Test]
        public void Ctor_WhenLong_ValueIsReflected()
        {
            var id = new StructureId(long.MaxValue, typeof(long));

            Assert.AreEqual(long.MaxValue, id.Value);
        }

        [Test]
        public void Ctor_WhenNullLong_HasValueIsFalse()
        {
            var id = new StructureId(null, typeof(long));

            Assert.AreEqual(null, id.Value);
            Assert.IsFalse(id.HasValue);
        }

        [Test]
        public void Ctor_WhenNullLong_TypeIsCorrect()
        {
            var id = new StructureId(null, typeof(long));

            Assert.AreEqual(StructureIdTypes.BigIdentity, id.IdType);
            Assert.AreEqual(typeof(long), id.DataType);
        }
    }
}