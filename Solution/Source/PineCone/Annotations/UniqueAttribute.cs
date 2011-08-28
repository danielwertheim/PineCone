using System;

namespace PineCone.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : Attribute
    {
        public UniqueModes Mode { get; private set; }

        public UniqueAttribute(UniqueModes mode)
        {
            Mode = mode;
        }
    }
}