using System.Collections.Generic;
using System.Threading.Tasks;
using EnsureThat;
using PineCone.Serializers;
using PineCone.Structures.IdGenerators;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class StructureBuilder : IStructureBuilder
    {
        private IStructureIndexesFactory _indexesFactory;
        private IStructureSerializer _structureSerializer;
        private IStructureIdGenerator _structureIdGenerator;

        public IStructureIndexesFactory IndexesFactory
        {
            get { return _indexesFactory; }
            set
            {
                Ensure.That(value, "IndexesFactory").IsNotNull();

                _indexesFactory = value;
            }
        }

        public IStructureSerializer StructureSerializer
        {
            get { return _structureSerializer; }
            set
            {
                Ensure.That(value, "StructureSerializer").IsNotNull();

                _structureSerializer = value;
            }
        }

        public IStructureIdGenerator StructureIdGenerator
        {
            get { return _structureIdGenerator; }
            set
            {
                Ensure.That(value, "StructureIdGenerator").IsNotNull();

                _structureIdGenerator = value;
            }
        }

        public StructureBuilder()
        {
            IndexesFactory = new StructureIndexesFactory();
            StructureSerializer = new EmptyStructureSerializer();
            StructureIdGenerator = new SequentialGuidStructureIdGenerator();
        }

        public virtual IStructure CreateStructure<T>(T item, IStructureSchema structureSchema) where T : class
        {
            var structureId = StructureIdGenerator.Generate(structureSchema);

            structureSchema.IdAccessor.SetValue(item, structureId);

            return new Structure(
                structureSchema.Name,
                structureId,
                IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                StructureSerializer.Serialize(item));
        }

		public virtual IStructure[] CreateStructures<T>(IList<T> items, IStructureSchema structureSchema) where T : class
		{
			return items.Count < 100
			       	? CreateStructuresInSerial(items, structureSchema)
			       	: CreateStructuresInParallel(items, structureSchema);
		}

    	protected virtual IStructure[] CreateStructuresInParallel<T>(IList<T> items, IStructureSchema structureSchema) where T : class
		{
			var structureIds = StructureIdGenerator.Generate(structureSchema, items.Count);
			var structures = new IStructure[items.Count];

			Parallel.For(0, items.Count, i =>
			{
				var id = structureIds[i];
				var itm = items[i];

				structureSchema.IdAccessor.SetValue(itm, id);

				structures[i] = new Structure(
					structureSchema.Name,
					id,
					IndexesFactory.CreateIndexes(structureSchema, itm, id),
					StructureSerializer.Serialize(itm));
			});

			return structures;
		}

		protected virtual IStructure[] CreateStructuresInSerial<T>(IList<T> items, IStructureSchema structureSchema) where T : class
		{
			var structures = new IStructure[items.Count];

			for(var i = 0; i < structures.Length; i++)
			{
				var id = StructureIdGenerator.Generate(structureSchema);
				var itm = items[i];

				structureSchema.IdAccessor.SetValue(itm, id);

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