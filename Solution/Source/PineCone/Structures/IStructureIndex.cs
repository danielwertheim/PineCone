using System;

namespace PineCone.Structures
{
    public interface IStructureIndex : IEquatable<IStructureIndex>
    {
        Guid StructureId { get;  }

        string Path { get;  }
        
        object Value { get; }

        bool IsUnique { get; }
    }
}