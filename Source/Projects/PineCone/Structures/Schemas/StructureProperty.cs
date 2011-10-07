using System;
using System.Linq;
using System.Reflection;
using EnsureThat;
using NCore.Reflections;
using PineCone.Annotations;
using PineCone.Resources;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructureProperty : IStructureProperty
    {
        private static readonly Type UniqueAttributeType = typeof(UniqueAttribute);

        private readonly DynamicGetter _getter;
        private readonly DynamicSetter _setter;

        public string Name { get; private set; }

        public string Path { get; private set; }

        public Type PropertyType { get; private set; }

        public IStructureProperty Parent { get; private set; }

        public bool IsRootMember { get; private set; }

        public bool IsUnique 
        {
            get { return UniqueMode.HasValue; }
        }

        public UniqueModes? UniqueMode { get; private set; }

        public bool IsEnumerable { get; private set; }

        public bool IsElement { get; private set; }

        public Type ElementType { get; private set; }

        public static StructureProperty CreateFrom(PropertyInfo propertyInfo)
        {
            return CreateFrom(null, propertyInfo);
        }

        public static StructureProperty CreateFrom(IStructureProperty parent, PropertyInfo propertyInfo)
        {
            var uniqueAttribute = (UniqueAttribute)propertyInfo.GetCustomAttributes(UniqueAttributeType, true).FirstOrDefault();

            UniqueModes? uniqueMode = null;
            if (uniqueAttribute != null)
                uniqueMode = uniqueAttribute.Mode;

            return new StructureProperty(
                parent, 
                propertyInfo.Name, 
                propertyInfo.PropertyType,
                DynamicPropertyFactory.CreateGetter(propertyInfo),
                DynamicPropertyFactory.CreateSetter(propertyInfo),
                uniqueMode);
        }

        private StructureProperty(IStructureProperty parent, string name, Type propertyType, DynamicGetter getter, DynamicSetter setter, UniqueModes? uniqueMode = null)
        {
            Ensure.That(name, "name").IsNotNullOrWhiteSpace();
            Ensure.That(propertyType, "propertyType").IsNotNull();
            Ensure.That(getter, "getter").IsNotNull();
            Ensure.That(setter, "setter").IsNotNull();

            Parent = parent;
            IsRootMember = parent == null;
            Name = name;
            PropertyType = propertyType;
            _getter = getter;
            _setter = setter;
            UniqueMode = uniqueMode;

            var isSimpleType = PropertyType.IsSimpleType();
            IsEnumerable = !isSimpleType && PropertyType.IsEnumerableType();
            ElementType = IsEnumerable ? PropertyType.GetEnumerableElementType() : null;
            IsElement = Parent != null && (Parent.IsElement || Parent.IsEnumerable);

            if (IsUnique && !isSimpleType)
                throw new PineConeException(ExceptionMessages.StructureProperty_Ctor_UniqueOnNonSimpleType);

            Path = PropertyPathBuilder.BuildPath(this);
        }

        public object GetValue(object item)
        {
            return _getter(item);
        }

        public void SetValue(object target, object value)
        {
            _setter(target, value);
        }
    }
}