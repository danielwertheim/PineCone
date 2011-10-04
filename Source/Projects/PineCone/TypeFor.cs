using System;

namespace PineCone
{
    internal static class TypeFor<T>
    {
        internal static readonly Type Type = typeof (T);
    }
}