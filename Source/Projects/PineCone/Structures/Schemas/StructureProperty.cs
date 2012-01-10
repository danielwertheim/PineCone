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

        private readonly PropertyInfo _property;
        private readonly DynamicGetter _getter;
        private readonly DynamicSetter _setter;

        public string Name { get { return _property.Name; } }

        public string Path { get; private set; }

        public Type PropertyType { get { return _property.PropertyType; } }

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
                propertyInfo,
                DynamicPropertyFactory.GetterFor(propertyInfo),
                DynamicPropertyFactory.SetterFor(propertyInfo),
                uniqueMode);
        }

        private StructureProperty(IStructureProperty parent, PropertyInfo property, DynamicGetter getter, DynamicSetter setter = null, UniqueModes? uniqueMode = null)
        {
            Ensure.That(property, "property").IsNotNull();
            Ensure.That(getter, "getter").IsNotNull();
            
            _property = property;
            _getter = getter;
            _setter = setter;

            Parent = parent;
            IsRootMember = parent == null;
            IsReadOnly = _setter == null;
            UniqueMode = uniqueMode;

            var isSimpleOrValueType = PropertyType.IsSimpleType() || PropertyType.IsValueType;
			IsEnumerable = !isSimpleOrValueType && PropertyType.IsEnumerableType();
            ElementType = IsEnumerable ? PropertyType.GetEnumerableElementType() : null;
            IsElement = Parent != null && (Parent.IsElement || Parent.IsEnumerable);

			if (IsUnique && !isSimpleOrValueType)
                throw new PineConeException(ExceptionMessages.StructureProperty_Ctor_UniqueOnNonSimpleType);

            Path = PropertyPathBuilder.BuildPath(this);
        }

        public object GetValue(object item)
        {
            return _getter.GetValue(item);
        }

        public void SetValue(object target, object value)
        {
            if(IsReadOnly)
                throw new PineConeException(ExceptionMessages.StructureProperty_Setter_IsReadOnly.Inject(Path));

            _setter.SetValue(target, value);
        }
    }
}