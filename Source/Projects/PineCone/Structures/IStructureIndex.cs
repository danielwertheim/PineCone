using System;

namespace PineCone.Structures
{
    public interface IStructureIndex : IEquatable<IStructureIndex>
    {
        IStructureId StructureId { get; }

        StructureIndexType IndexType { get; }

        string Path { get;  }
        
        object Value { get; }

        bool IsUnique { get; }
    }
}