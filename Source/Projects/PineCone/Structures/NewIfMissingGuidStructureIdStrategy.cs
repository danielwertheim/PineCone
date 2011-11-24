using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class NewIfMissingGuidStructureIdStrategy : IStructureIdStrategy
    {
        public virtual IStructureId Apply<T>(IStructureSchema structureSchema, T item) where T : class
        {
            var structureId = structureSchema.IdAccessor.GetValue(item);

            if (structureId.HasValue)
                return structureId;

            structureId = StructureId.Create(SequentialGuid.New());
            structureSchema.IdAccessor.SetValue(item, structureId);

            return structureId;
        }
    }
}