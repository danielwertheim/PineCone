using System;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public interface IMemberAccessor
    {
        string Name { get; }

        string Path { get; }

        Type DataType { get; }
    }
}