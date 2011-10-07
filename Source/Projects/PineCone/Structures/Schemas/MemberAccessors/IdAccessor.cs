using System;
using NCore;
using PineCone.Resources;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public class IdAccessor : MemberAccessorBase, IIdAccessor
    {
        public StructureIdTypes IdType { get; private set; }

        public IdAccessor(IStructureProperty property)
            : base(property)
        {
            if (!property.IsRootMember)
                throw new PineConeException(ExceptionMessages.IdAccessor_GetIdValue_InvalidLevel);

            if (!StructureId.IsValidDataType(property.PropertyType))
                throw new PineConeException(ExceptionMessages.IdAccessor_UnsupportedPropertyType.Inject(Property.PropertyType.Name));

            IdType = StructureId.GetIdTypeFrom(property.PropertyType);
        }

        public IStructureId GetValue<T>(T item)
            where T : class
        {
            return StructureId.Create((ValueType)Property.GetValue(item), Property.PropertyType);
        }
        
        public void SetValue<T>(T item, IStructureId value)
            where T : class
        {
            Property.SetValue(item, value.Value);
        }
    }
}