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
        public void Create_WhenGuid_ValuesAreReflected()
        {
            var value = Guid.Parse("46C72168-C637-416D-9736-751E2A17028A");

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(Guid), id.DataType);
            Assert.AreEqual(StructureIdTypes.Guid, id.IdType);
        }

        [Test]
        public void Create_WhenNullableGuidNotNull_ValuesAreReflected()
        {
            Guid? value = Guid.Parse("46C72168-C637-416D-9736-751E2A17028A");

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(Guid?), id.DataType);
            Assert.AreEqual(StructureIdTypes.Guid, id.IdType);
        }

        [Test]
        public void Create_WhenNullableGuidBeingNull_ValuesAreReflected()
        {
            Guid? value = null;

            var id = StructureId.Create(value);

            Assert.IsFalse(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(Guid?), id.DataType);
            Assert.AreEqual(StructureIdTypes.Guid, id.IdType);
        }

        [Test]
        public void Create_WhenInt_ValuesAreReflected()
        {
            var value = int.MaxValue;

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(int), id.DataType);
            Assert.AreEqual(StructureIdTypes.Identity, id.IdType);
        }

        [Test]
        public void Create_WhenNullableIntNotNull_ValuesAreReflected()
        {
            int? value = int.MaxValue;

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(int?), id.DataType);
            Assert.AreEqual(StructureIdTypes.Identity, id.IdType);
        }

        [Test]
        public void Create_WhenNullableIntBeingNull_ValuesAreReflected()
        {
            int? value = null;

            var id = StructureId.Create(value);

            Assert.IsFalse(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(int?), id.DataType);
            Assert.AreEqual(StructureIdTypes.Identity, id.IdType);
        }

        [Test]
        public void Create_WhenLong_ValuesAreReflected()
        {
            var value = long.MaxValue;

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(long), id.DataType);
            Assert.AreEqual(StructureIdTypes.Identity, id.IdType);
        }

        [Test]
        public void Create_WhenNullableLongNotNull_ValuesAreReflected()
        {
            long? value = long.MaxValue;

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(long?), id.DataType);
            Assert.AreEqual(StructureIdTypes.Identity, id.IdType);
        }

        [Test]
        public void Create_WhenNullableLongBeingNull_ValuesAreReflected()
        {
            long? value = null;

            var id = StructureId.Create(value);

            Assert.IsFalse(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(long?), id.DataType);
            Assert.AreEqual(StructureIdTypes.Identity, id.IdType);
        }

        [Test]
        public void Ctor_WhenRefType_ThrowsException()
        {
            var ex = Assert.Throws<PineConeException>(() => StructureId.Create(null, typeof(string)));

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
        public void GetIdTypeFrom_WhenInt_ReturnsIdTypeOfIdentity()
        {
            Assert.AreEqual(StructureIdTypes.Identity, StructureId.GetIdTypeFrom(typeof(int)));
        }

        [Test]
        public void GetIdTypeFrom_WhenNullableInt_ReturnsIdTypeOfIdentity()
        {
            Assert.AreEqual(StructureIdTypes.Identity, StructureId.GetIdTypeFrom(typeof(int?)));
        }

        [Test]
        public void GetIdTypeFrom_WhenLong_ReturnsIdTypeOfIdentity()
        {
            Assert.AreEqual(StructureIdTypes.Identity, StructureId.GetIdTypeFrom(typeof(long)));
        }

        [Test]
        public void GetIdTypeFrom_WhenNullableLong_ReturnsIdTypeOfIdentity()
        {
            Assert.AreEqual(StructureIdTypes.Identity, StructureId.GetIdTypeFrom(typeof(long?)));
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