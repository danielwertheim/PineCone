using System;
using NCore;
using PineCone.Resources;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public class ConcurrencyTokenAccessor : MemberAccessorBase, IConcurrencyTokenAccessor
    {
        public ConcurrencyTokenAccessor(IStructureProperty property)
            : base(property)
        {
            if (!property.IsRootMember)
                throw new PineConeException(ExceptionMessages.ConcurrencyTokenAccessor_InvalidLevel.Inject(Property.Name));

            if (property.PropertyType != TypeFor<Guid>.Type)
                throw new PineConeException(ExceptionMessages.ConcurrencyTokenAccessor_Invalid_Type.Inject(Property.Name));
        }

        public Guid GetValue<T>(T item)
            where T : class
        {
            return (Guid)Property.GetValue(item);
        }
        
        public void SetValue<T>(T item, Guid value)
            where T : class
        {
            Property.SetValue(item, value);
        }
    }
}