using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using PineCone.Serializers;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class StructureBuilder : IStructureBuilder
    {
        private readonly IStructureIndexesFactory _indexesFactory;
        private readonly IStructureIdGenerators _structureIdGenerators;
        private ISerializer _serializer;

        public IStructureIdGenerators StructureIdGenerators 
        {
            get { return _structureIdGenerators; }
        }

        public ISerializer Serializer
        {
            get { return _serializer; }
            set { _serializer = value ?? new EmptySerializer(); }
        }

        public StructureBuilder(IStructureIdGenerators structureIdGenerators, IStructureIndexesFactory indexesFactory)
        {
            Ensure.That(() => structureIdGenerators).IsNotNull();
            Ensure.That(() => indexesFactory).IsNotNull();

            _structureIdGenerators = structureIdGenerators;
            _indexesFactory = indexesFactory;

            Serializer = new EmptySerializer();
        }

        public IStructure CreateStructure<T>(T item, IStructureSchema structureSchema)
            where T : class
        {
            return CreateStructure(item, structureSchema, StructureIdGenerators.Get(structureSchema.IdAccessor.IdType).CreateId());
        }

        public IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema) where T : class
        {
            return CreateStructureBatches(items, structureSchema, items.Count).SelectMany(s => s);
        }

        public IEnumerable<IStructure[]> CreateStructureBatches<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize) where T : class
        {
            var structureIdGenerator = StructureIdGenerators.Get(structureSchema.IdAccessor.IdType);
            var batchSize = items.Count() > maxBatchSize ? maxBatchSize : items.Count();

            var batchNo = 0;
            while (true)
            {
                var sourceBatch = items.Skip(batchNo * batchSize).Take(batchSize).ToArray();
                if (sourceBatch.Length < 1)
                    yield break;

                var structures = new IStructure[sourceBatch.Length];
                var ids = structureIdGenerator.CreateIds(sourceBatch.Length).ToArray();

                Parallel.For(0, sourceBatch.Length,
                    i =>
                    {
                        structures[i] = CreateStructure(sourceBatch[i], structureSchema, ids[i]);
                    });

                yield return structures;

                batchNo++;
            }
        }

        private IStructure CreateStructure<T>(T item, IStructureSchema structureSchema, StructureId structureId)
            where T : class
        {
            structureSchema.IdAccessor.SetValue(item, structureId);

            return new Structure(
                structureSchema.Name,
                structureId,
                _indexesFactory.CreateIndexes(structureSchema, item, structureId),
                Serializer.Serialize(item));
        }
    }
}