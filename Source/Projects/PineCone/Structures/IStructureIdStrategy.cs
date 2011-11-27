using System.Collections.Generic;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public interface IStructureIdStrategy
    {
        IStructureId Apply<T>(IStructureSchema structureSchema, T item) where T : class ;

        IEnumerable<IStructureIdStrategyResult<T>> Apply<T>(IStructureSchema structureSchema, IEnumerable<T> items) where T : class;
    }
}