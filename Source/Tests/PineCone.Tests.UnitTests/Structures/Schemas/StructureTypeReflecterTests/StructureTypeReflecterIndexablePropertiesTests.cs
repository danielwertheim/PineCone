﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterIndexablePropertiesTests : UnitTestBase
    {
        private readonly IStructureTypeReflecter _reflecter = new StructureTypeReflecter();

        [Test]
        public void GetIndexableProperties_WhenGuidIdExists_IdMemberIsReturned()
        {
            var property = _reflecter.GetIndexableProperties(typeof (WithGuidId))
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenIdentityExists_IdMemberIsReturned()
        {
            var property = _reflecter.GetIndexableProperties(typeof(WithIdentityId))
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenNulledNullableGuidIdExists_IdMemberIsReturned()
        {
            var property = _reflecter.GetIndexableProperties(typeof(WithNullableGuidId))
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenNullableIdentityExists_IdMemberIsReturned()
        {
            var property = _reflecter.GetIndexableProperties(typeof(WithNullableIdentityId))
                .SingleOrDefault(p => p.Path == "StructureId");

            Assert.IsNotNull(property);
        }

        [Test]
        public void GetIndexableProperties_WhenByteArray_DoesNotReturnTheArrayMember()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithArrayOfBytes))
                .SingleOrDefault(p => p.Path == "Values");

            Assert.IsNull(properties);
        }

        [Test]
        public void GetIndexableProperties_WhenIEnumerableOfTIndexes_DoesNotReturnTheEnumerableMember()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithCollectionIndexes))
                .SingleOrDefault(p => p.Path == "IEnumerable1");

            Assert.IsNull(properties);
        }

        [Test]
        public void GetIndexableProperties_WhenIEnumerableOfTIndexes_ReturnsTheElementMembers()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithCollectionIndexes))
                .SingleOrDefault(p => p.Path == "IEnumerable1.Int2");

            Assert.IsNotNull(properties);
        }

        [Test]
        public void GetIndexableProperties_WhenIListOfTIndexes_ReturnsTheElementMembers()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithCollectionIndexes))
                .SingleOrDefault(p => p.Path == "IList1.Int2");

            Assert.IsNotNull(properties);
        }

        [Test]
        public void GetIndexableProperties_WhenArrayOfStrings_OnlyReturnsPropertyForAccessingTheStringArray()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithArrayOfStrings));
            var arrayIndex = properties.SingleOrDefault(p => p.Path == "Values");

            Assert.AreEqual(1, properties.Count());
            Assert.IsNotNull(arrayIndex);
        }

        [Test]
        public void GetIndexableProperties_WhenArrayOfIntegers_OnlyReturnsPropertyForAccessingTheStringArray()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithArrayOfIntegers));
            var arrayIndex = properties.SingleOrDefault(p => p.Path == "Values");

            Assert.AreEqual(1, properties.Count());
            Assert.IsNotNull(arrayIndex);
        }

        [Test]
        public void GetIndexableProperties_WhenWithNestedArrayOfStrings_OnlyReturnsPropertyForAccessingTheStringArray()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithNestedArrayOfStrings));
            var arrayIndex = properties.SingleOrDefault(p => p.Path == "Item.Values");

            Assert.AreEqual(1, properties.Count());
            Assert.IsNotNull(arrayIndex);
        }

        [Test]
        public void GetIndexableProperties_WhenWithArrayOfNestedArrayOfStrings_OnlyReturnsPropertyForAccessingTheStringArray()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithArrayOfNestedArrayOfStrings));
            var arrayIndex = properties.SingleOrDefault(p => p.Path == "Items.Values");

            Assert.AreEqual(1, properties.Count());
            Assert.IsNotNull(arrayIndex);
        }

        [Test]
        public void GetIndexableProperties_WhenEnumerableByteMembers_NoPropertiesAreReturned()
        {
            var properties = _reflecter.GetIndexableProperties(typeof(WithEnumarbleBytes));

            Assert.AreEqual(0, properties.Count());
        }

        private class WithIdentityId
        {
            public int StructureId { get; set; }
        }

        private class WithGuidId
        {
            public Guid StructureId { get; set; }
        }

        private class WithNullableIdentityId
        {
            public int? StructureId { get; set; }
        }

        private class WithNullableGuidId
        {
            public Guid? StructureId { get; set; }
        }

        private class WithEnumarbleBytes
        {
            public byte[] Bytes1 { get; set; }

            public IEnumerable<byte> Bytes2 { get; set; }

            public IList<byte> Bytes3 { get; set; }

            public List<byte> Bytes4 { get; set; }

            public ICollection<byte> Bytes5 { get; set; }

            public Collection<byte> Bytes6 { get; set; }
        }

        private class WithArrayOfStrings
        {
            public string[] Values { get; set; }
        }

        private class WithArrayOfIntegers
        {
            public int[] Values { get; set; }
        }

        private class WithArrayOfBytes
        {
            public byte[] Values { get; set; }
        }

        private class WithNestedArrayOfStrings
        {
            public WithArrayOfStrings Item { get; set; }
        }

        private class WithArrayOfNestedArrayOfStrings
        {
            public WithArrayOfStrings[] Items { get; set; }
        }

        private class Element
        {
            public int Int2 { get; set; }
        }

        private class WithCollectionIndexes
        {
            public int Int1 { get; set; }

            public IEnumerable<Element> IEnumerable1 { get; set; }

            public IList<Element> IList1 { get; set; }
        }
    }
}