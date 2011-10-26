using System;
using System.Linq;
using System.Reflection;
using EnsureThat;
using NCore;
using NCore.Reflections;
using PineCone.Annotations;
using PineCone.Resources;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructureProperty : IStructureProperty
    {
        private static readonly Type UniqueAttributeType = typeof(UniqueAttribute);

        private readonly DynamicProperty _property;

        public string Name { get { return _property.PropertyInfo.Name; } }

        public string Path { get; private set; }

        public Type PropertyType { get { return _property.PropertyInfo.PropertyType; } }

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

        public bool IsReadOnly { get; private set; }

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
                DynamicPropertyFactory.Create(propertyInfo),
                uniqueMode);
        }

        private StructureProperty(IStructureProperty parent, DynamicProperty property, UniqueModes? uniqueMode = null)
        {
            Ensure.That(property, "property").IsNotNull();

            _property = property;
            Parent = parent;
            IsRootMember = parent == null;
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
            return _property.Getter.Invoke(item);
        }

        public void SetValue(object target, object value)
        {
            if(IsReadOnly)
                throw new PineConeException(ExceptionMessages.StructureProperty_Setter_IsReadOnly.Inject(Path));

            _property.Setter(target, value);
        }
    }
}