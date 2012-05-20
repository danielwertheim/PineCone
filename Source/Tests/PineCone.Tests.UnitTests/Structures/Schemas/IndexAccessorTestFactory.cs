using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.Builders;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Tests.UnitTests.Structures.Schemas
{
    internal static class IndexAccessorTestFactory
    {
        private static readonly IDataTypeConverter DataTypeConverter = new DataTypeConverter();

        internal static IIndexAccessor CreateFor(IStructureProperty property)
        {
            return new IndexAccessor(property, DataTypeConverter.Convert(property));
        }
    }
}