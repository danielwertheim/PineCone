using System.Collections.Generic;
using System.Linq;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class KeepStructureIdStrategy : IStructureIdStrategy
    {
        public virtual IStructureId Apply<T>(IStructureSchema structureSchema, T item) where T : class
        {
            return structureSchema.IdAccessor.GetValue(item);
        }

        public IEnumerable<IStructureIdStrategyResult<T>> Apply<T>(IStructureSchema structureSchema, IEnumerable<T> items) where T : class
        {
            return items.Select(item => new StructureIdStrategyResult<T>(structureSchema.IdAccessor.GetValue(item), item));
        }
    }
}