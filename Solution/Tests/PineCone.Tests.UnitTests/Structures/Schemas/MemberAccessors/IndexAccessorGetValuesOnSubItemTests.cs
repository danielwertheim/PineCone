using NUnit.Framework;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class IndexAccessorGetValuesOnSubItemTests : UnitTestBase
    {
        [Test]
        public void GetValues_WhenSubItemsArrayHasElementsWithValues_ReturnsTheValues()
        {
            var subItemsProp = StructurePropertyTestHelper.GetProperty<Item>("SubItems");
            var valueProp = StructurePropertyTestHelper.GetProperty<SubItem>("Value", subItemsProp);

            var subItems = new[] { new SubItem { Value = "A" }, new SubItem { Value = "B" } };
            var item = new Item { SubItems = subItems };

            var indexAccessor = new IndexAccessor(valueProp);
            var values = indexAccessor.GetValues(item);

            CollectionAssert.AreEquivalent(new[] { "A", "B" }, values);
        }

        [Test]
        public void GetValues_WhenSubItemsArrayHasElementsWithNullValues_ReturnsTheNullValues()
        {
            var subItemsProp = StructurePropertyTestHelper.GetProperty<Item>("SubItems");
            var valueProp = StructurePropertyTestHelper.GetProperty<SubItem>("Value", subItemsProp);

            var subItems = new[] { new SubItem { Value = null }, new SubItem { Value = null } };
            var item = new Item { SubItems = subItems };

            var indexAccessor = new IndexAccessor(valueProp);
            var values = indexAccessor.GetValues(item);

            CollectionAssert.AreEquivalent(new string[] { null, null }, values);
        }

        [Test]
        public void GetValues_WhenSubItemsArrayIsNull_ReturnsNull()
        {
            var subItemsProp = StructurePropertyTestHelper.GetProperty<Item>("SubItems");
            var valueProp = StructurePropertyTestHelper.GetProperty<SubItem>("Value", subItemsProp);
            var item = new Item { SubItems = null };

            var indexAccessor = new IndexAccessor(valueProp);
            var value = indexAccessor.GetValues(item);

            Assert.AreEqual(new string[] { null }, value);
        }

        [Test]
        public void GetValues_WhenSubItemsArrayHasBothNullAndNonNullItems_ReturnsNonNullAndNullItems()
        {
            var subItemsProp = StructurePropertyTestHelper.GetProperty<Item>("SubItems");
            var valueProp = StructurePropertyTestHelper.GetProperty<SubItem>("Value", subItemsProp);

            var subItems = new[] { null, new SubItem { Value = "A" } };
            var item = new Item { SubItems = subItems };

            var indexAccessor = new IndexAccessor(valueProp);
            var value = indexAccessor.GetValues(item);

            Assert.AreEqual(new object[] { null, "A" }, value);
        }

        [Test]
        public void GetValues_WhenStringOnSingleSubItem_ReturnsValue()
        {
            var subItemProp = StructurePropertyTestHelper.GetProperty<Item>("SingleSubItem");
            var valueProp = StructurePropertyTestHelper.GetProperty<SubItem>("Value", subItemProp);

            var subItem = new SubItem { Value = "The value" };
            var item = new Item { SingleSubItem = subItem };

            var indexAccessor = new IndexAccessor(valueProp);
            var value = indexAccessor.GetValues(item);

            Assert.AreEqual(new object[] { "The value" }, value);
        }

        private class Item
        {
            public SubItem SingleSubItem { get; set; }

            public SubItem[] SubItems { get; set; }
        }

        private class SubItem
        {
            public string Value { get; set; }
        }
    }
}