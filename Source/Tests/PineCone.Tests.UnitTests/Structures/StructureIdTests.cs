using System;
using NCore;
using NUnit.Framework;
using PineCone.Resources;
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

        [Test]
        public void Ctor_WhenRefType_ThrowsException()
        {
            var ex = Assert.Throws<PineConeException>(() => new StructureId(null, typeof(string)));

            Assert.AreEqual(ExceptionMessages.StructureId_InvalidType.Inject(typeof(string).Name), ex.Message);
        }

        [Test]
        public void GetIdTypeFrom_WhenGuid_ReturnsIdTypeOfGuid()
        {
            Assert.AreEqual(StructureIdTypes.Guid, StructureId.GetIdTypeFrom(typeof(Guid)));
        }

        [Test]
        public void GetIdTypeFrom_WhenNullableGuid_ReturnsIdTypeOfGuid()
        {
            Assert.AreEqual(StructureIdTypes.Guid, StructureId.GetIdTypeFrom(typeof(Guid?)));
        }

        [Test]
        public void GetIdTypeFrom_WhenInt_ReturnsIdTypeOfSmallIdentity()
        {
            Assert.AreEqual(StructureIdTypes.SmallIdentity, StructureId.GetIdTypeFrom(typeof(int)));
        }

        [Test]
        public void GetIdTypeFrom_WhenNullableInt_ReturnsIdTypeOfSmallIdentity()
        {
            Assert.AreEqual(StructureIdTypes.SmallIdentity, StructureId.GetIdTypeFrom(typeof(int?)));
        }

        [Test]
        public void GetIdTypeFrom_WhenLong_ReturnsIdTypeOfBigIdentity()
        {
            Assert.AreEqual(StructureIdTypes.BigIdentity, StructureId.GetIdTypeFrom(typeof(long)));
        }

        [Test]
        public void GetIdTypeFrom_WhenNullableLong_ReturnsIdTypeOfBigIdentity()
        {
            Assert.AreEqual(StructureIdTypes.BigIdentity, StructureId.GetIdTypeFrom(typeof(long?)));
        }

        [Test]
        public void IsValidType_WhenGuid_ReturnsTrue()
        {
            Assert.IsTrue(StructureId.IsValidDataType(typeof(Guid)));
        }

        [Test]
        public void IsValidType_WhenNullableGuid_ReturnsTrue()
        {
            Assert.IsTrue(StructureId.IsValidDataType(typeof(Guid?)));
        }

        [Test]
        public void IsValidType_WhenInt_ReturnsTrue()
        {
            Assert.IsTrue(StructureId.IsValidDataType(typeof(int)));
        }

        [Test]
        public void IsValidType_WhenNullableInt_ReturnsTrue()
        {
            Assert.IsTrue(StructureId.IsValidDataType(typeof(int?)));
        }

        [Test]
        public void IsValidType_WhenLong_ReturnsTrue()
        {
            Assert.IsTrue(StructureId.IsValidDataType(typeof(long)));
        }

        [Test]
        public void IsValidType_WhenNullableLong_ReturnsTrue()
        {
            Assert.IsTrue(StructureId.IsValidDataType(typeof(long?)));
        }

        [Test]
        public void IsValidType_WhenRefType_ReturnsFalse()
        {
            Assert.IsFalse(StructureId.IsValidDataType(typeof(string)));
        }
    }
}