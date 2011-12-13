using System;
using NUnit.Framework;
using PineCone.Resources;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures
{
    [TestFixture]
    public class StructureIdTests : UnitTestBase
    {
        [Test]
        public void ConvertFrom_WhenStringAsObject_ValuesAreReflected()
        {
            var value = "My string id.";

            var id = StructureId.ConvertFrom(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(string), id.DataType);
            Assert.AreEqual(StructureIdTypes.String, id.IdType);
        }

        [Test]
        public void ConvertFrom_WhenGuidAsObject_ValuesAreReflected()
        {
            var value = Guid.Parse("925DE70F-03F4-4FC6-B372-FAA344CA8C90");

            var id = StructureId.ConvertFrom(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(Guid), id.DataType);
            Assert.AreEqual(StructureIdTypes.Guid, id.IdType);
        }

        [Test]
        public void ConvertFrom_WhenIntAsObject_ValuesAreReflected()
        {
            int value = 42;

            var id = StructureId.ConvertFrom(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(int), id.DataType);
            Assert.AreEqual(StructureIdTypes.Identity, id.IdType);
        }

        [Test]
        public void ConvertFrom_WhenLongAsObject_ValuesAreReflected()
        {
            long value = 42;

            var id = StructureId.ConvertFrom(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(long), id.DataType);
            Assert.AreEqual(StructureIdTypes.BigIdentity, id.IdType);
        }

        [Test]
        public void Create_WhenString_ValuesAreReflected()
        {
            var value = "My string id.";

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(string), id.DataType);
            Assert.AreEqual(StructureIdTypes.String, id.IdType);
        }

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
            Assert.AreEqual(StructureIdTypes.BigIdentity, id.IdType);
        }

        [Test]
        public void Create_WhenNullableLongNotNull_ValuesAreReflected()
        {
            long? value = long.MaxValue;

            var id = StructureId.Create(value);

            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(long?), id.DataType);
            Assert.AreEqual(StructureIdTypes.BigIdentity, id.IdType);
        }

        [Test]
        public void Create_WhenNullableLongBeingNull_ValuesAreReflected()
        {
            long? value = null;

            var id = StructureId.Create(value);

            Assert.IsFalse(id.HasValue);
            Assert.AreEqual(value, id.Value);
            Assert.AreEqual(typeof(long?), id.DataType);
            Assert.AreEqual(StructureIdTypes.BigIdentity, id.IdType);
        }

        [Test]
        public void GetIdTypeFrom_WhenString_ReturnsIdTypeOfString()
        {
            Assert.AreEqual(StructureIdTypes.String, StructureId.GetIdTypeFrom(typeof(string)));
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
            Assert.AreEqual(StructureIdTypes.BigIdentity, StructureId.GetIdTypeFrom(typeof(long)));
        }

        [Test]
        public void GetIdTypeFrom_WhenNullableLong_ReturnsIdTypeOfIdentity()
        {
            Assert.AreEqual(StructureIdTypes.BigIdentity, StructureId.GetIdTypeFrom(typeof(long?)));
        }

        [Test]
        public void IsValidType_WhenString_ReturnsTrue()
        {
            Assert.IsTrue(StructureId.IsValidDataType(typeof(string)));
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
        public void IsValidType_WhenObjectType_ReturnsFalse()
        {
            Assert.IsFalse(StructureId.IsValidDataType(typeof(object)));
        }

		[Test]
		public void CompareTo_WhenDifferentIdTypes_ThrowsException()
		{
			var x = StructureId.Create((int?)42);
			var y = StructureId.Create((long?)42);

			var ex = Assert.Throws<PineConeException>(() => x.CompareTo(y));

			Assert.AreEqual(ExceptionMessages.StructureId_CompareTo_DifferentIdTypes, ex.Message);
		}

    	[Test]
    	public void CompareTo_WhenEqualInts_Returns0()
    	{
    		var x = StructureId.Create(42);
			var y = StructureId.Create(42);

			Assert.AreEqual(0, x.CompareTo(y));
    	}

		[Test]
		public void CompareTo_WhenEqualNullableInts_Returns0()
		{
			var x = StructureId.Create((int?)42);
			var y = StructureId.Create((int?)42);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualNonNullableAndNullableInts_Returns0()
		{
			var x = StructureId.Create((int?)42);
			var y = StructureId.Create(42);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualNullableIntsAreNull_Returns0()
		{
			var x = StructureId.Create((int?)null);
			var y = StructureId.Create((int?)null);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualLongs_Returns0()
		{
			var x = StructureId.Create((long)42);
			var y = StructureId.Create((long)42);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualNullableLongs_Returns0()
		{
			var x = StructureId.Create((long?)42);
			var y = StructureId.Create((long?)42);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualNonNullableAndNullableLongs_Returns0()
		{
			var x = StructureId.Create((long?)42);
			var y = StructureId.Create((long)42);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualNullableLongsAreNull_Returns0()
		{
			var x = StructureId.Create((long?)null);
			var y = StructureId.Create((long?)null);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualGuids_Returns0()
		{
			var v = Guid.Parse("E72A0240-4B05-4678-B1DD-8CCBA10F8CA2");
			var x = StructureId.Create(v);
			var y = StructureId.Create(v);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualNullableGuids_Returns0()
		{
			var v = Guid.Parse("E72A0240-4B05-4678-B1DD-8CCBA10F8CA2");
			var x = StructureId.Create((Guid?)v);
			var y = StructureId.Create((Guid?)v);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualNonNullableAndNullableGuids_Returns0()
		{
			var v = Guid.Parse("E72A0240-4B05-4678-B1DD-8CCBA10F8CA2");
			var x = StructureId.Create((Guid?)v);
			var y = StructureId.Create(v);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenStringsAreNull_Returns0()
		{
			var x = StructureId.Create((string)null);
			var y = StructureId.Create((string)null);

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenEqualStrings_Returns0()
		{
			var x = StructureId.Create("foo");
			var y = StructureId.Create("foo");

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenStringsDifferOnCasing_Returns0()
		{
			var x = StructureId.Create("foo");
			var y = StructureId.Create("FOO");

			Assert.AreEqual(0, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenIntXisLessThanY_ReturnsMinus1()
		{
			var x = StructureId.Create(10);
			var y = StructureId.Create(20);

			Assert.AreEqual(-1, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenIntXisGreaterThanY_Returns1()
		{
			var x = StructureId.Create(20);
			var y = StructureId.Create(10);

			Assert.AreEqual(1, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenLongXisLessThanY_ReturnsMinus1()
		{
			var x = StructureId.Create((long)10);
			var y = StructureId.Create((long)20);

			Assert.AreEqual(-1, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenLongXisGreaterThanY_Returns1()
		{
			var x = StructureId.Create((long)20);
			var y = StructureId.Create((long)10);

			Assert.AreEqual(1, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenStringXisLessThanY_ReturnsMinus1()
		{
			var x = StructureId.Create("a");
			var y = StructureId.Create("b");

			Assert.AreEqual(-1, x.CompareTo(y));
		}

		[Test]
		public void CompareTo_WhenStringXisGreaterThanY_Returns1()
		{
			var x = StructureId.Create("b");
			var y = StructureId.Create("a");

			Assert.AreEqual(1, x.CompareTo(y));
		}
    }
}