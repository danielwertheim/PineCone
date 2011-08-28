using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCore;
using PineCone.Resources;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class StructureIndexesFactory : IStructureIndexesFactory
    {
        public ICollection<IStructureIndex> CreateIndexes<T>(IStructureSchema structureSchema, T item, Guid structureId)
            where T : class
        {
            var indexes = new IEnumerable<IStructureIndex>[structureSchema.IndexAccessors.Count];

            Parallel.For(0, indexes.Length,
                c =>
                {
                    var indexAccessor = structureSchema.IndexAccessors[c];
                    var values = indexAccessor.GetValues(item);
                    if (values == null || values.Count < 1)
                    {
                        if (indexAccessor.IsUnique)
                            throw new PineConeException(ExceptionMessages.StructureIndexesFactory_UniqueIndex_IsNull.Inject(indexAccessor.Path, indexAccessor.Name));

                        indexes[c] = new[] {
                            new StructureIndex(structureId, indexAccessor.Name, null, indexAccessor.IsUnique)};
                    }
                    else
                    {
                        if (values.Count > 1 || indexAccessor.IsEnumerable || indexAccessor.IsElement)
                        {
                            var subIndexes = new IStructureIndex[values.Count];
                            Parallel.For(0, subIndexes.Length,
                                subC =>
                                {
                                    subIndexes[subC] = new StructureIndex(structureId, indexAccessor.Name, values[subC], indexAccessor.IsUnique);
                                });
                            indexes[c] = subIndexes;
                        }
                        else
                            indexes[c] = new[]{
                                new StructureIndex(structureId, indexAccessor.Name, values[0], indexAccessor.IsUnique)};
                    }
                });

            return indexes.SelectMany(i => i).ToArray();
        }
    }
}