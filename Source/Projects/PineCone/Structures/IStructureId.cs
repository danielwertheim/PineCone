using System;

namespace PineCone.Structures
{
    public interface IStructureId : IEquatable<IStructureId>
    {
        StructureIdTypes IdType { get; }
        object Value { get; }
        Type DataType { get; }
        bool HasValue { get; }
    }
}