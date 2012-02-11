using System;

namespace PineCone.Structures.Schemas
{
    public class DynamicGetter
    {
        private readonly Func<object, object> _accessor;

        public DynamicGetter(Func<object, object> accessor)
        {
            _accessor = accessor;
        }

        public object GetValue<T>(T item)
        {
            return _accessor(item);
        }
    }
}