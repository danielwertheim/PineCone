﻿using System;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterSimpleIndexablePropertiesTests : StructureTypeReflecterTestsBase
    {
        [Test]
        public void GetIndexableProperties_WhenMultiplePublicSimplePropertiesExistsAndNoExclusions_ReturnsAllPublicSimpleProperties()
        {
            var properties = ReflecterFor<WithSimpleProperties>().GetIndexableProperties();

            var paths = properties.Select(p => p.Path).ToArray();
            Assert.AreEqual(9, properties.Count());
            CollectionAssert.Contains(paths, "GuidValue");
            CollectionAssert.Contains(paths, "ShortValue");
            CollectionAssert.Contains(paths, "IntValue");
            CollectionAssert.Contains(paths, "LongValue");
            CollectionAssert.Contains(paths, "StringValue");
            CollectionAssert.Contains(paths, "DecimalValue");
            CollectionAssert.Contains(paths, "DateTimeValue");
            CollectionAssert.Contains(paths, "BoolValue");
            CollectionAssert.Contains(paths, "ByteValue");
        }

        [Test]
        public void GetIndexableProperties_WhenMultiplePublicSimpleNullablePropertiesExistsAndNoExclusions_ReturnsAllPublicSimpleProperties()
        {
            var properties = ReflecterFor<WithSimpleNullableProperties>().GetIndexableProperties();

            var paths = properties.Select(p => p.Path).ToArray();
            Assert.AreEqual(8, properties.Count());
            CollectionAssert.Contains(paths, "GuidValue");
            CollectionAssert.Contains(paths, "ShortValue");
            CollectionAssert.Contains(paths, "IntValue");
            CollectionAssert.Contains(paths, "LongValue");
            CollectionAssert.Contains(paths, "DecimalValue");
            CollectionAssert.Contains(paths, "DateTimeValue");
            CollectionAssert.Contains(paths, "BoolValue");
            CollectionAssert.Contains(paths, "ByteValue");
        }

        [Test]
        public void GetIndexableProperties_WhenSimplePrivatePropertyExists_PrivatePropertyIsNotReturned()
        {
            var properties = ReflecterFor<WithPrivateProperty>().GetIndexableProperties();

            Assert.AreEqual(0, properties.Count());
        }

        private class WithSimpleProperties
        {
            public Guid GuidValue { get; set; }
            public short ShortValue { get; set; }
            public int IntValue { get; set; }
            public long LongValue { get; set; }
            public string StringValue { get; set; }
            public DateTime DateTimeValue { get; set; }
            public decimal DecimalValue { get; set; }
            public bool BoolValue { get; set; }
            public byte ByteValue { get; set; }
        }

        private class WithSimpleNullableProperties
        {
            public Guid? GuidValue { get; set; }
            public short? ShortValue { get; set; }
            public int? IntValue { get; set; }
            public long? LongValue { get; set; }
            public DateTime? DateTimeValue { get; set; }
            public decimal? DecimalValue { get; set; }
            public bool? BoolValue { get; set; }
            public byte? ByteValue { get; set; }
        }

        private class WithPrivateProperty
        {
            private int Int { get; set; }
        }
    }
}