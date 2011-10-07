using System;

namespace PineCone.Structures
{
    public interface IStructureId : IEquatable<IStructureId>
    {
        StructureIdTypes IdType { get; }
        ValueType Value { get; }
        Type DataType { get; }
        bool HasValue { get; }
    }
}