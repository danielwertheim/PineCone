using System;
using NUnit.Framework;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public abstract class StructureTypeReflecterTestsBase : UnitTestBase
    {
        protected IStructureTypeReflecter ReflecterFor<T>(Action<IStructureTypeConfig> configurator = null) where T : class
        {
            var cfg = new StructureTypeConfig(typeof(T));

            if (configurator != null)
                configurator(cfg);

            return new StructureTypeReflecter(cfg);
        }
    }
}