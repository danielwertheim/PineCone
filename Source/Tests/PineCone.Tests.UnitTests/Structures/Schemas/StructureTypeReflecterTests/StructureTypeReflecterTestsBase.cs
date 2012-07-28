using NUnit.Framework;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public abstract class StructureTypeReflecterTestsBase : UnitTestBase
    {
        protected IStructureTypeReflecter ReflecterFor()
        {
            return new StructureTypeReflecter();
        }
    }
}