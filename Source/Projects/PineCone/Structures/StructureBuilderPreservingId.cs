using System.Collections.Generic;
using System.Threading.Tasks;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class StructureBuilderPreservingId : StructureBuilder
    {
        public override IStructure CreateStructure<T>(T item, IStructureSchema structureSchema)
        {
            var structureId = structureSchema.IdAccessor.GetValue(item);

            return new Structure(
                structureSchema.Name,
                structureId,
                IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                StructureSerializer.Serialize(item));
        }

        public override IStructure[] CreateStructures<T>(T[] items, IStructureSchema structureSchema)
        {
            var structures = new IStructure[items.Length];

            Parallel.For(0, items.Length, i =>
            {
                var itm = items[i];
                var id = structureSchema.IdAccessor.GetValue(itm);
                
                structureSchema.IdAccessor.SetValue(itm, id);

                structures[i] = new Structure(
                    structureSchema.Name,
                    id,
                    IndexesFactory.CreateIndexes(structureSchema, itm, id),
                    StructureSerializer.Serialize(itm));
            });

            return structures;
        }
    }
}