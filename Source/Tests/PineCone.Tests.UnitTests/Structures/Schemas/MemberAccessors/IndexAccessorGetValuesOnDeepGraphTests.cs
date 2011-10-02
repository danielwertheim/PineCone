using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas.MemberAccessors
{
    [TestFixture]
    public class IndexAccessorGetValuesOnDeepGraphTests : UnitTestBase
    {
        [Test]
        public void GetValues_WhenDeepGraphWithEnumerables_CanExtractValues()
        {
            var ordersPropertyInfo = typeof(TestCustomer).GetProperty("Orders");
            var ordersProperty = StructureProperty.CreateFrom(ordersPropertyInfo);

            var linesPropertyInfo = typeof(TestOrder).GetProperty("Lines");
            var linesProperty = StructureProperty.CreateFrom(ordersProperty, linesPropertyInfo);

            var prodNoPropertyInfo = typeof(TestOrderLine).GetProperty("ProductNo");
            var prodNoProperty = StructureProperty.CreateFrom(linesProperty, prodNoPropertyInfo);

            var pricesPropertyInfo = typeof(TestOrderLine).GetProperty("Prices");
            var pricesProperty = StructureProperty.CreateFrom(linesProperty, pricesPropertyInfo);

            var graph = new TestCustomer
            {
                Orders = 
                {
                    new TestOrder
                    {
                        Lines =
                        {
                            new TestOrderLine { ProductNo = "P1", Quantity = 1, Prices = new[] { 42, 4242 }}, 
                            new TestOrderLine { ProductNo = "P2", Quantity = 2, Prices = new[] { 43, 4343 }}
                        }
                    }
                }
            };

            var productNos = new IndexAccessor(prodNoProperty).GetValues(graph);
            var prices = new IndexAccessor(pricesProperty).GetValues(graph);

            CollectionAssert.AreEqual(new[] { "P1", "P2" }, productNos);
            CollectionAssert.AreEqual(new[] { 42, 4242, 43, 4343 }, prices);
        }

        private class TestCustomer
        {
            public string CustomerNo { get; set; }

            public List<TestOrder> Orders { get; set; }

            public int[] Points { get; set; }

            public string[] Addresses { get; set; }

            public TestCustomer()
            {
                Orders = new List<TestOrder>();
            }
        }

        private class TestOrder
        {
            public List<TestOrderLine> Lines { get; set; }

            public TestOrder()
            {
                Lines = new List<TestOrderLine>();
            }
        }

        private class TestOrderLine
        {
            public string ProductNo { get; set; }

            public int Quantity { get; set; }

            public int[] Prices { get; set; }
        }
    }
}