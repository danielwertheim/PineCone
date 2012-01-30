﻿using System.Collections.Generic;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Structures.Schemas
{
    public interface IStructureSchema
    {
		IStructureType Type { get; }

    	string Name { get; }

        string Hash { get; }

    	bool HasId { get; }

    	IIdAccessor IdAccessor { get; }

        IList<IIndexAccessor> IndexAccessors { get; }

        IList<IIndexAccessor> UniqueIndexAccessors { get; }
    }
}