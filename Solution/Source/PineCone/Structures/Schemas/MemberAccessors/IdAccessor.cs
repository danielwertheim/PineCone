using System;
using NCore;
using NCore.Reflections;
using PineCone.Resources;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public class IdAccessor : MemberAccessorBase, IIdAccessor
    {
        public IdAccessor(IStructureProperty property)
            : base(property)
        {
            if (!property.IsRootMember)
                throw new PineConeException(ExceptionMessages.IdAccessor_GetIdValue_InvalidLevel);

            var isGuidType = Property.PropertyType.IsGuidType() || Property.PropertyType.IsNullableGuidType();

            if (!isGuidType)
                throw new PineConeException(
                    ExceptionMessages.IdAccessor_UnsupportedPropertyType.Inject(Property.PropertyType.Name));
        }

        public Guid? GetValue<T>(T item)
            where T : class
        {
            return (Guid?)Property.GetValue(item);
        }
        
        public void SetValue<T>(T item, Guid value)
            where T : class
        {
            Property.SetValue(item, value);
        }
    }
}