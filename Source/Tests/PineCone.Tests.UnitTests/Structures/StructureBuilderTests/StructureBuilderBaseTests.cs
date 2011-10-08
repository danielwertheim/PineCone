﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using PineCone.Structures;

namespace PineCone.Tests.UnitTests.Structures.StructureBuilderTests
{
    [TestFixture]
    public abstract class StructureBuilderBaseTests : UnitTestBase
    {
        protected StructureBuilder Builder;

        protected override void OnTestInitialize()
        {
            Builder = new StructureBuilder();
        }

        protected static ICollection<GuidItem> CreateGuidItems(int numOfItems)
        {
            var items = new List<GuidItem>(numOfItems);

            for (var c = 0; c < numOfItems; c++)
                items.Add(new GuidItem { Value = c + 1 });

            return items;
        }

        protected class GuidItem
        {
            public Guid StructureId { get; set; }

            public int Value { get; set; }
        }
    }
}