using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas
{
    public class StructurePropertyTestHelper
    {
        internal static StructureProperty GetProperty<T>(string name)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(name);

            var property = StructureProperty.CreateFrom(propertyInfo);

            return property;
        }

        internal static StructureProperty GetProperty<T>(string name, StructureProperty parent)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(name);

            var property = StructureProperty.CreateFrom(parent, propertyInfo);

            return property;
        }
    }
}