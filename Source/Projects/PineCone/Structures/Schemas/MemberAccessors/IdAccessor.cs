using NCore;
using PineCone.Resources;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public class IdAccessor : MemberAccessorBase, IIdAccessor
    {
        private readonly Getter.IGetter _getter;
        private readonly Setter.ISetter _setter;

        public StructureIdTypes IdType { get; private set; }

        public IdAccessor(IStructureProperty property)
            : base(property)
        {
            if (!property.IsRootMember)
                throw new PineConeException(ExceptionMessages.IdAccessor_GetIdValue_InvalidLevel);

            if (!StructureId.IsValidDataType(property.PropertyType))
                throw new PineConeException(ExceptionMessages.IdAccessor_UnsupportedPropertyType.Inject(Property.PropertyType.Name));

            IdType = StructureId.GetIdTypeFrom(property.PropertyType);

            _getter = Getter.For(IdType, Property.PropertyType);
            _setter = Setter.For(IdType, Property.PropertyType);
        }

        public IStructureId GetValue<T>(T item)
            where T : class
        {
            return _getter.GetValue(item, Property);
        }
        
        public void SetValue<T>(T item, IStructureId value)
            where T : class
        {
            _setter.SetValue(item, value, Property);
        }
    }
}