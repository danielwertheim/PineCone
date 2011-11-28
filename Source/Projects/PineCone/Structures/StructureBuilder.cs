using System.Threading.Tasks;
using EnsureThat;
using PineCone.Serializers;
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
            StructureIdGenerator = new GuidStructureIdGenerator();
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

        public virtual IStructure[] CreateStructures<T>(T[] items, IStructureSchema structureSchema) where T : class
        {
            var structureIds = StructureIdGenerator.Generate(structureSchema, items.Length);
            var structures = new IStructure[items.Length];

            Parallel.For(0, items.Length, i =>
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
    }
}