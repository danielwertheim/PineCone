using System;
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
        private const string ConcurrencyTokenMemberName = "ConcurrencyToken";

        private static readonly string[] NonIndexableSystemMembers = new[] { ConcurrencyTokenMemberName };

        public const BindingFlags IdPropertyBindingFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;

        public const BindingFlags PropertyBindingFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;

        public bool HasIdProperty(Type type)
        {
            return GetIdProperty(type) != null;
        }

        public bool HasConcurrencyTokenProperty(Type type)
        {
            return GetConcurrencyTokenProperty(type) != null;
        }

        public bool HasTimeStampProperty(Type type)
        {
            return GetTimeStampProperty(type) != null;
        }

        public virtual IStructureProperty GetIdProperty(Type type)
        {
            var properties = type.GetProperties(IdPropertyBindingFlags).Where(p => p.Name.EndsWith(StructureIdPropertyNames.Indicator)).ToArray();

            var defaultProp = GetDefaultStructureIdProperty(properties);
            if (defaultProp != null)
                return StructureProperty.CreateFrom(defaultProp);

            var typeNamedIdProp = GetTypeNamedStructureIdProperty(type, properties);
            if (typeNamedIdProp != null)
                return StructureProperty.CreateFrom(typeNamedIdProp);

            var interfaceNamedIdProp = GetInterfaceNamedStructureIdProperty(type, properties);
            if (interfaceNamedIdProp != null)
                return StructureProperty.CreateFrom(interfaceNamedIdProp);

            var idProp = properties.SingleOrDefault(p => p.Name.Equals(StructureIdPropertyNames.Indicator));
            if (idProp != null)
                return StructureProperty.CreateFrom(idProp);

            return null;
        }

        protected virtual PropertyInfo GetDefaultStructureIdProperty(IEnumerable<PropertyInfo> properties)
        {
            return properties.SingleOrDefault(p => p.Name.Equals(StructureIdPropertyNames.Default));
        }

        protected virtual PropertyInfo GetTypeNamedStructureIdProperty(Type type, IEnumerable<PropertyInfo> properties)
        {
            var propertyName = StructureIdPropertyNames.GetTypeNamePropertyNameFor(type);

            return properties.SingleOrDefault(p => p.Name.Equals(propertyName));
        }

        protected virtual PropertyInfo GetInterfaceNamedStructureIdProperty(Type type, IEnumerable<PropertyInfo> properties)
        {
            if (!type.IsInterface)
                return null;

            var propertyName = StructureIdPropertyNames.GetInterfaceTypeNamePropertyNameFor(type);

            return properties.SingleOrDefault(p => p.Name.Equals(propertyName));
        }

        public virtual IStructureProperty GetTimeStampProperty(Type type)
        {
            var properties = type.GetProperties(PropertyBindingFlags).Where(p => p.Name.EndsWith(StructureTimeStampPropertyNames.Indicator)).ToArray();

            var defaultProp = GetDefaultStructureTimeStampProperty(properties);
            if (defaultProp != null)
                return StructureProperty.CreateFrom(defaultProp);

            var typeNamedProp = GetTypeNamedStructureTimeStampProperty(type, properties);
            if (typeNamedProp != null)
                return StructureProperty.CreateFrom(typeNamedProp);

            var interfaceNamedProp = GetInterfaceNamedStructureTimeStampProperty(type, properties);
            if (interfaceNamedProp != null)
                return StructureProperty.CreateFrom(interfaceNamedProp);

            var prop = properties.SingleOrDefault(p => p.Name.Equals(StructureTimeStampPropertyNames.Indicator));
            if (prop != null)
                return StructureProperty.CreateFrom(prop);

            return null;
        }

        protected virtual PropertyInfo GetDefaultStructureTimeStampProperty(IEnumerable<PropertyInfo> properties)
        {
            return properties.SingleOrDefault(p => p.Name.Equals(StructureTimeStampPropertyNames.Default));
        }

        protected virtual PropertyInfo GetTypeNamedStructureTimeStampProperty(Type type, IEnumerable<PropertyInfo> properties)
        {
            var propertyName = StructureTimeStampPropertyNames.GetTypeNamePropertyNameFor(type);

            return properties.SingleOrDefault(p => p.Name.Equals(propertyName));
        }

        protected virtual PropertyInfo GetInterfaceNamedStructureTimeStampProperty(Type type, IEnumerable<PropertyInfo> properties)
        {
            if (!type.IsInterface)
                return null;

            var propertyName = StructureTimeStampPropertyNames.GetInterfaceTypeNamePropertyNameFor(type);

            return properties.SingleOrDefault(p => p.Name.Equals(propertyName));
        }

        public virtual IStructureProperty GetConcurrencyTokenProperty(Type type)
        {
            var propertyInfo = type.GetProperty(ConcurrencyTokenMemberName, PropertyBindingFlags);

            return propertyInfo == null
                ? null
                : StructureProperty.CreateFrom(propertyInfo);
        }

        public virtual IStructureProperty[] GetIndexableProperties(Type type)
        {
            Ensure.That(type, "type").IsNotNull();

            return GetIndexableProperties(type, null, NonIndexableSystemMembers, null);
        }

        public virtual IStructureProperty[] GetIndexablePropertiesExcept(Type type, ICollection<string> nonIndexablePaths)
        {
            Ensure.That(type, "type").IsNotNull();
            Ensure.That(nonIndexablePaths, "nonIndexablePaths").HasItems();

            return GetIndexableProperties(type, null, NonIndexableSystemMembers.MergeWith(nonIndexablePaths).ToArray(), null);
        }

        public virtual IStructureProperty[] GetSpecificIndexableProperties(Type type, ICollection<string> indexablePaths)
        {
            Ensure.That(type, "type").IsNotNull();
            Ensure.That(indexablePaths, "indexablePaths").HasItems();

            return GetIndexableProperties(type, null, NonIndexableSystemMembers, indexablePaths);
        }

        protected virtual IStructureProperty[] GetIndexableProperties(
            IReflect type,
            IStructureProperty parent,
            ICollection<string> nonIndexablePaths,
            ICollection<string> indexablePaths)
        {
            var propertyInfos = type.GetProperties(PropertyBindingFlags);
            if (propertyInfos.Length == 0)
                return new IStructureProperty[] { };

            var properties = new List<IStructureProperty>();

            properties.AddRange(GetSimpleIndexablePropertyInfos(propertyInfos, parent, nonIndexablePaths, indexablePaths)
                .Select(spi => StructureProperty.CreateFrom(parent, spi)));

            foreach (var complexPropertyInfo in GetComplexIndexablePropertyInfos(propertyInfos, parent, nonIndexablePaths, indexablePaths))
            {
                var complexProperty = StructureProperty.CreateFrom(parent, complexPropertyInfo);
                var simpleComplexProps = GetIndexableProperties(
                    complexProperty.DataType, complexProperty, nonIndexablePaths, indexablePaths);

                var beforeCount = properties.Count;
                properties.AddRange(simpleComplexProps);

                if (properties.Count == beforeCount && complexProperty.DataType.IsValueType)
                    properties.Add(complexProperty);
            }

            foreach (var enumerablePropertyInfo in GetEnumerableIndexablePropertyInfos(propertyInfos, parent, nonIndexablePaths, indexablePaths))
            {
                var enumerableProperty = StructureProperty.CreateFrom(parent, enumerablePropertyInfo);
                if (enumerableProperty.ElementDataType.IsSimpleType())
                {
                    properties.Add(enumerableProperty);
                    continue;
                }

                var elementProperties = GetIndexableProperties(enumerableProperty.ElementDataType,
                                                               enumerableProperty,
                                                               nonIndexablePaths,
                                                               indexablePaths);
                properties.AddRange(elementProperties);
            }

            return properties.ToArray();
        }

        protected virtual IEnumerable<PropertyInfo> GetSimpleIndexablePropertyInfos(PropertyInfo[] properties, IStructureProperty parent = null, ICollection<string> nonIndexablePaths = null, ICollection<string> indexablePaths = null)
        {
            if (properties.Length == 0)
                return properties;

            var filteredProperties = properties.Where(p => p.PropertyType.IsSimpleType());

            if (nonIndexablePaths != null)
                filteredProperties = filteredProperties.Where(p => !nonIndexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            if (indexablePaths != null)
                filteredProperties = filteredProperties.Where(p => indexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            return filteredProperties.ToArray();
        }

        protected virtual IEnumerable<PropertyInfo> GetComplexIndexablePropertyInfos(PropertyInfo[] properties, IStructureProperty parent = null, ICollection<string> nonIndexablePaths = null, ICollection<string> indexablePaths = null)
        {
            if (properties.Length == 0)
                return properties;

            var filteredProperties = properties.Where(p =>
                !p.PropertyType.IsSimpleType() &&
                !p.PropertyType.IsEnumerableType() &&
                GetIdProperty(p.PropertyType) == null);

            if (nonIndexablePaths != null)
                filteredProperties = filteredProperties.Where(p => !nonIndexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            if (indexablePaths != null)
                filteredProperties = filteredProperties.Where(p => indexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            return filteredProperties.ToArray();
        }

        protected virtual IEnumerable<PropertyInfo> GetEnumerableIndexablePropertyInfos(PropertyInfo[] properties, IStructureProperty parent = null, ICollection<string> nonIndexablePaths = null, ICollection<string> indexablePaths = null)
        {
            if (properties.Length == 0)
                return properties;

            var filteredProperties = properties.Where(p =>
                !p.PropertyType.IsSimpleType() &&
                p.PropertyType.IsEnumerableType() &&
                !p.PropertyType.IsEnumerableBytesType());

            if (nonIndexablePaths != null)
                filteredProperties = filteredProperties.Where(p => !nonIndexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            if (indexablePaths != null)
                filteredProperties = filteredProperties.Where(p => indexablePaths.Contains(
                    PropertyPathBuilder.BuildPath(parent, p.Name)));

            return filteredProperties.ToArray();
        }
    }
}