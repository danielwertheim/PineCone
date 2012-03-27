using System.Threading.Tasks;
using NCore;
using PineCone.Serializers;
using PineCone.Structures.IdGenerators;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class StructureBuilderPreservingId : StructureBuilder
    {
        public StructureBuilderPreservingId()
        {
            IndexesFactory = new StructureIndexesFactory();
            StructureSerializer = new EmptyStructureSerializer();
            StructureIdGenerator = new EmptyStructureIdGenerator();
        }

        public override IStructure CreateStructure<T>(T item, IStructureSchema structureSchema)
        {
            var structureId = structureSchema.IdAccessor.GetValue(item);
            
            if (structureSchema.HasTimeStamp)
                structureSchema.TimeStampAccessor.SetValue(item, SysDateTime.Now);

            return new Structure(
                structureSchema.Name,
                structureId,
                IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                StructureSerializer.Serialize(item));
        }

		public override IStructure[] CreateStructures<T>(T[] items, IStructureSchema structureSchema)
		{
			return items.Length < 100
					? CreateStructuresInSerial(items, structureSchema)
					: CreateStructuresInParallel(items, structureSchema);
		}

		protected override IStructure[] CreateStructuresInParallel<T>(T[] items, IStructureSchema structureSchema)
		{
			var structures = new IStructure[items.Length];
            var timeStamp = SysDateTime.Now;

			Parallel.For(0, items.Length, i =>
			{
				var itm = items[i];
				var id = structureSchema.IdAccessor.GetValue(itm);

				structureSchema.IdAccessor.SetValue(itm, id);

                if (structureSchema.HasTimeStamp)
                    structureSchema.TimeStampAccessor.SetValue(itm, timeStamp);

				structures[i] = new Structure(
					structureSchema.Name,
					id,
					IndexesFactory.CreateIndexes(structureSchema, itm, id),
					StructureSerializer.Serialize(itm));
			});

			return structures;
		}

		protected override IStructure[] CreateStructuresInSerial<T>(T[] items, IStructureSchema structureSchema)
		{
			var structures = new IStructure[items.Length];
            var timeStamp = SysDateTime.Now;

			for (var i = 0; i < structures.Length; i++)
			{
				var itm = items[i];
				var id = structureSchema.IdAccessor.GetValue(itm);

				structureSchema.IdAccessor.SetValue(itm, id);

                if (structureSchema.HasTimeStamp)
                    structureSchema.TimeStampAccessor.SetValue(itm, timeStamp);

				structures[i] = new Structure(
					structureSchema.Name,
					id,
					IndexesFactory.CreateIndexes(structureSchema, itm, id),
					StructureSerializer.Serialize(itm));
			}

			return structures;
		}
    }
}