using System;

namespace PineCone
{
    internal static class TypeFor<T> where T : class
    {
        internal static readonly Type Type = typeof (T);
    }
}