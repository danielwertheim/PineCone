using System.Reflection;

namespace PineCone.Structures.Schemas
{
    public interface IStructurePropertyFactory
    {
        StructurePropertyFactoryRules Rules { get; }
        IStructureProperty CreateRootPropertyFrom(PropertyInfo propertyInfo);
        IStructureProperty CreateChildPropertyFrom(IStructureProperty parent, PropertyInfo propertyInfo);
    }
}