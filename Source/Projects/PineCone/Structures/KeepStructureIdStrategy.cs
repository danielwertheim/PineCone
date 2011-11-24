using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class KeepStructureIdStrategy : IStructureIdStrategy
    {
        public virtual IStructureId Apply<T>(IStructureSchema structureSchema, T item) where T : class
        {
            return structureSchema.IdAccessor.GetValue(item);
        }
    }
}