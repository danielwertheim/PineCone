using System.Linq;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas
{
    internal static class StructurePropertyTestFactory
    {
        private static readonly IStructureTypeReflecter Reflecter = new StructureTypeReflecter();

        internal static IStructureProperty GetIdProperty<T>() where T : class 
        {
            return Reflecter.GetIdProperty(typeof (T));
        }

        internal static IStructureProperty GetPropertyByPath<T>(string path) where T : class 
        {
            return Reflecter.GetIndexableProperties(typeof(T)).Single(i => i.Path == path);
        }

        internal static IStructureProperty GetPropertyByName<T>(string name) where T : class 
        {
            return Reflecter.GetIndexableProperties(typeof(T)).Single(i => i.Name == name);
        }

        internal static IStructureProperty GetRawProperty<T>(string name) where T : class 
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(name);

            var property = Reflecter.PropertyFactory.CreateRootPropertyFrom(propertyInfo);

            return property;
        }

        internal static IStructureProperty GetRawProperty<T>(string name, IStructureProperty parent) where T : class 
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(name);

            var property = Reflecter.PropertyFactory.CreateChildPropertyFrom(parent, propertyInfo);

            return property;
        }
    }
}