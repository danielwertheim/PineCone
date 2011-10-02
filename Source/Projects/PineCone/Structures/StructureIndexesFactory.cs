using System;
using System.Collections;
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

            Parallel.For(0, indexes.Length, c =>
            {
                var indexAccessor = structureSchema.IndexAccessors[c];
                var values = indexAccessor.GetValues(item);
                var valuesExists = values != null && values.Count > 0;
                var isCollectionOfValues = indexAccessor.IsEnumerable || indexAccessor.IsElement || (values != null && values.Count > 1);

                if (!valuesExists)
                {
                    if (indexAccessor.IsUnique)
                        throw new PineConeException(ExceptionMessages.StructureIndexesFactory_UniqueIndex_IsNull.Inject(indexAccessor.Path, indexAccessor.Name));

                    if(!isCollectionOfValues)
                        indexes[c] = new[] { new StructureIndex(structureId, indexAccessor.Path, null, indexAccessor.IsUnique) };
                    
                    return;
                }

                if (!isCollectionOfValues)
                    indexes[c] = new[] { new StructureIndex(structureId, indexAccessor.Path, values[0], indexAccessor.IsUnique) };
                else
                {
                    var pathFormat = indexAccessor.Name + "[{0}]";
                    var subIndexes = new IStructureIndex[values.Count];
                    Parallel.For(0, subIndexes.Length, subC =>
                    {
                        subIndexes[subC] = new StructureIndex(structureId, pathFormat.Inject(subC), values[subC], indexAccessor.IsUnique);
                    });
                    indexes[c] = subIndexes;
                }
            });

            return indexes.Where(i => i != null).SelectMany(i => i).ToArray();
        }
    }
}