using NUnit.Framework;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class IndexAccessorSetValuesTests : UnitTestBase
    {
        [Test]
        public void SetValue_WhenAssigningStringValueToNullProp_ValueIsAssigned()
        {
            const string newValue = "Test";
            var item = new Item { StringProp = null };

            var property = StructurePropertyTestHelper.GetProperty<Item>("StringProp");
            var indexAccessor = new IndexAccessor(property);

            indexAccessor.SetValue(item, newValue);

            Assert.AreEqual(newValue, item.StringProp);
        }

        [Test]
        public void SetValue_WhenAssigningStringValueToNonNullProp_ValueIsAssigned()
        {
            const string newValue = "Override with this";
            var item = new Item { StringProp = "Test" };

            var property = StructurePropertyTestHelper.GetProperty<Item>("StringProp");
            var indexAccessor = new IndexAccessor(property);

            indexAccessor.SetValue(item, newValue);

            Assert.AreEqual(newValue, item.StringProp);
        }

        [Test]
        public void SetValue_WhenAssigningPrimitiveValueToNonNullProp_ValueIsAssigned()
        {
            const int newValue = 42;
            var item = new Item { IntProp = 0 };

            var property = StructurePropertyTestHelper.GetProperty<Item>("IntProp");
            var indexAccessor = new IndexAccessor(property);

            indexAccessor.SetValue(item, newValue);

            Assert.AreEqual(newValue, item.IntProp);
        }

        [Test]
        public void SetValue_WhenAssigningPrimitiveValueToNullNullableProp_ValueIsAssigned()
        {
            const int newValue = 42;
            var item = new Item { NullableIntProp = null };

            var property = StructurePropertyTestHelper.GetProperty<Item>("NullableIntProp");
            var indexAccessor = new IndexAccessor(property);

            indexAccessor.SetValue(item, newValue);

            Assert.AreEqual(newValue, item.NullableIntProp);
        }

        [Test]
        public void SetValue_WhenAssigningPrimitiveValueToNonNullNullableProp_ValueIsAssigned()
        {
            const int newValue = 42;
            var item = new Item { NullableIntProp = 1 };

            var property = StructurePropertyTestHelper.GetProperty<Item>("NullableIntProp");
            var indexAccessor = new IndexAccessor(property);

            indexAccessor.SetValue(item, newValue);

            Assert.AreEqual(newValue, item.NullableIntProp);
        }

        private class Item
        {
            public string StringProp { get; set; }

            public int IntProp { get; set; }

            public int? NullableIntProp { get; set; }
        }
    }
}