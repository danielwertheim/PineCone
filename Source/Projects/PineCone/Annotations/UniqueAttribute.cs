using System;

namespace PineCone.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : Attribute
    {
        public UniqueMode Mode { get; private set; }

        public UniqueAttribute(UniqueMode mode)
        {
            Mode = mode;
        }
    }
}