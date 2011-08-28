using NUnit.Framework;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class IndexAccessorSetValuesOnSubItemTests : UnitTestBase
    {
        [Test]
        public void SetValue_WhenComplexType_CompexTypeIsAssigned()
        {
            const string newValue = "Test";
            var item = new Item { SingleSubItem = null };

            var subItemProp = StructurePropertyTestHelper.GetProperty<Item>("SingleSubItem");

            var indexAccessor = new IndexAccessor(subItemProp);
            indexAccessor.SetValue(item, new SubItem { Value = newValue });

            Assert.AreEqual(newValue, item.SingleSubItem.Value);
        }

        [Test]
        public void SetValue_WhenStringValueToNestedNullProp_ValueIsAssigned()
        {
            const string newValue = "Test";
            var item = new Item
                       {
                           SingleSubItem = new SubItem { Value = null }
                       };

            var subItemProp = StructurePropertyTestHelper.GetProperty<Item>("SingleSubItem");
            var valueProp = StructurePropertyTestHelper.GetProperty<SubItem>("Value", subItemProp);

            var indexAccessor = new IndexAccessor(valueProp);
            indexAccessor.SetValue(item, newValue);

            Assert.AreEqual(newValue, item.SingleSubItem.Value);
        }

        [Test]
        public void SetValue_WhenStringValueToNestedNonNullProp_ValueIsAssigned()
        {
            const string newValue = "Test";
            var item = new Item
            {
                SingleSubItem = new SubItem { Value = "Not" + newValue }
            };

            var subItemProp = StructurePropertyTestHelper.GetProperty<Item>("SingleSubItem");
            var valueProp = StructurePropertyTestHelper.GetProperty<SubItem>("Value", subItemProp);

            var indexAccessor = new IndexAccessor(valueProp);
            indexAccessor.SetValue(item, newValue);

            Assert.AreEqual(newValue, item.SingleSubItem.Value);
        }

        private class Item
        {
            public SubItem SingleSubItem { get; set; }
        }

        private class SubItem
        {
            public string Value { get; set; }
        }
    }
}