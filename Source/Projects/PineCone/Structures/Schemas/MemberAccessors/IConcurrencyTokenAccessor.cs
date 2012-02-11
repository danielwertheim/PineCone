using System;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public interface IConcurrencyTokenAccessor : IMemberAccessor
    {
        Guid GetValue<T>(T item) where T : class;

        void SetValue<T>(T item, Guid value) where T : class;
    }
}