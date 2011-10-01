using System.Collections.Generic;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Structures.Schemas
{
    public interface IStructureSchema
    {
        string Name { get; }

        IIdAccessor IdAccessor { get; }

        IList<IIndexAccessor> IndexAccessors { get; }

        IList<IIndexAccessor> UniqueIndexAccessors { get; }
    }
}