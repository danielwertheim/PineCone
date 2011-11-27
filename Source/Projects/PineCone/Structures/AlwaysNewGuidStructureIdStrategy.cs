using System.Collections.Generic;
using System.Linq;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class AlwaysNewGuidStructureIdStrategy : IStructureIdStrategy
    {
        public virtual IStructureId Apply<T>(IStructureSchema structureSchema, T item) where T : class
        {
            var structureId = StructureId.Create(SequentialGuid.New());

            structureSchema.IdAccessor.SetValue(item, structureId);

            return structureId;
        }

        public IEnumerable<IStructureIdStrategyResult<T>> Apply<T>(IStructureSchema structureSchema, IEnumerable<T> items) where T : class
        {
            return items.Select(item => new StructureIdStrategyResult<T>(Apply(structureSchema, item), item));
        }
    }
}