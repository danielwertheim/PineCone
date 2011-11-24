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
    }
}