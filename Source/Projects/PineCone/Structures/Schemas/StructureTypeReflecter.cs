using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnsureThat;
using NCore.Collections;
using NCore.Reflections;

namespace PineCone.Structures.Schemas
{
    public class StructureTypeReflecter : IStructureTypeReflecter
    {
        private static readonly string[] NonIndexableSystemMembers = new string[] { };//StructureSchema.IdMemberName };

        public const BindingFlags IdPropertyBindingFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;

        public const BindingFlags PropertyBindingFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;

        public bool HasIdProperty(IReflect type)
        {
            return GetIdProperty(type) != null;
        }

        public IStructureProperty GetIdProperty(IReflect type)
        {
            var propertyInfo = type.GetProperties(IdPropertyBindingFlags)
                .Where(p => p.Name.Equals(StructureSchema.IdMemberName))
                .SingleOrDefault();

            if (propertyInfo == null)
                return null;

            return StructureProperty.CreateFrom(propertyInfo);
        }

        public IEnumerable<IStructureProperty> GetIndexableProperties(IReflect type)
        {
            Ensure.That(type, "type").IsNotNull();

            return GetIndexableProperties(type, null, NonIndexableSystemMembers, null);
        }

        public IEnumerable<IStructureProperty> GetIndexablePropertiesExcept(IReflect type, ICollection<string> nonIndexablePaths)
        {
            Ensure.That(type, "type").IsNotNull();
            Ensure.That((ICollection)nonIndexablePaths, "nonIndexablePaths").HasItems();

            return GetIndexableProperties(type, null, NonIndexableSystemMembers.MergeWith(nonIndexablePaths).ToArray(), null);
        }

        public IEnumerable<IStructureProperty> GetSpecificIndexableProperties(IReflect type, ICollection<string> indexablePaths)
        {
            Ensure.That(type, "type").IsNotNull();
            Ensure.That((ICollection)indexablePaths, "indexablePaths").HasItems();

            return GetIndexableProperties(type, null, NonIndexableSystemMembers, indexablePaths);
        }

        private IEnumerable<IStructureProperty> GetIndexableProperties(
            IReflect type, 
            IStructureProperty parent, 
            ICollection<string> nonIndexablePaths,
            ICollection<string> indexablePaths)
        {
            var propertyInfos = type.GetProperties(PropertyBindingFlags);
            var properties = new List<IStructureProperty>();

            properties.AddRange(
                GetSimpleIndexablePropertyInfos(propertyInfos, parent, nonIndexablePaths, indexablePaths)
                .Select(spi => StructureProperty.CreateFrom(parent, spi)));

            foreach (var complexPropertyInfo in GetComplexIndexablePropertyInfos(propertyInfos, parent, nonIndexablePaths, indexablePaths))
            {
                var complexProperty = StructureProperty.CreateFrom(parent, complexPropertyInfo);
                var simpleComplexProps = GetIndexableProperties(
                    complexProperty.PropertyType, complexProperty, nonIndexablePaths, indexablePaths);

                properties.AddRange(simpleComplexProps);
            }

            foreach (var enumerablePropertyInfo in GetEnumerableIndexablePropertyInfos(propertyInfos, parent, nonIndexablePaths, indexablePaths))
            {
                var enumerableProperty = StructureProperty.CreateFrom(parent, enumerablePropertyInfo);
                if (enumerableProperty.ElementType.IsSimpleType())
                {
                    properties.Add(enumerableProperty);
                    continue;
                }

                var elementProperties = GetIndexableProperties(enumerableProperty.ElementType,
                                                               enumerableProperty,
                                                               nonIndexablePaths,
                                                               indexablePaths);
                properties.AddRange(elementProperties);
            }

            return properties;
        }

        internal static IEnumerable<PropertyInfo> GetSimpleIndexablePropertyInfos(IEnumerable<PropertyInfo> properties, IStructureProperty parent = null, IEnumerable<string> nonIndexablePaths = null, IEnumerable<string> indexablePaths = null)
        {
            properties = properties.Where(p => p.PropertyType.IsSimpleType());

            if (nonIndexablePaths != null)
                properties = properties.Where(p => !nonIndexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            if (indexablePaths != null)
                properties = properties.Where(p => indexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            return properties.ToArray();
        }

        internal IEnumerable<PropertyInfo> GetComplexIndexablePropertyInfos(IEnumerable<PropertyInfo> properties, IStructureProperty parent = null, IEnumerable<string> nonIndexablePaths = null, IEnumerable<string> indexablePaths = null)
        {
            properties = properties.Where(p =>
                !p.PropertyType.IsSimpleType() &&
                !p.PropertyType.IsEnumerableType() &&
                GetIdProperty(p.PropertyType) == null);

            if (nonIndexablePaths != null)
                properties = properties.Where(p => !nonIndexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            if (indexablePaths != null)
                properties = properties.Where(p => indexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            return properties.ToArray();
        }

        internal IEnumerable<PropertyInfo> GetEnumerableIndexablePropertyInfos(IEnumerable<PropertyInfo> properties, IStructureProperty parent = null, IEnumerable<string> nonIndexablePaths = null, IEnumerable<string> indexablePaths = null)
        {
            properties = properties.Where(p =>
                !p.PropertyType.IsSimpleType() &&
                p.PropertyType.IsEnumerableType() &&
                !p.PropertyType.IsEnumerableBytesType());

            if (nonIndexablePaths != null)
                properties = properties.Where(p => !nonIndexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            if (indexablePaths != null)
                properties = properties.Where(p => indexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            return properties.ToArray();
        }
    }
}