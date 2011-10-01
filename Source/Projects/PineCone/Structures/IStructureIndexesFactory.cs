using System;
using System.Collections.Generic;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public interface IStructureIndexesFactory
    {
        ICollection<IStructureIndex> CreateIndexes<T>(IStructureSchema structureSchema, T item, Guid structureId)
            where T : class;
    }
}