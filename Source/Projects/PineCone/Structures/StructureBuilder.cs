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
        private readonly IStructureIdGenerator _structureIdGenerator;
        private readonly IStructureIndexesFactory _indexesFactory;
        private ISerializer _serializer;

        public ISerializer Serializer
        {
            get { return _serializer; }
            set { _serializer = value ?? new EmptySerializer(); }
        }

        public StructureBuilder(IStructureIdGenerator structureIdGenerator, IStructureIndexesFactory indexesFactory)
        {
            Ensure.That(() => structureIdGenerator).IsNotNull();
            Ensure.That(() => indexesFactory).IsNotNull();

            _structureIdGenerator = structureIdGenerator;
            _indexesFactory = indexesFactory;

            Serializer = new EmptySerializer();
        }

        public IStructure CreateStructure<T>(T item, IStructureSchema structureSchema)
            where T : class
        {
            return CreateStructure(item, structureSchema, _structureIdGenerator.CreateId());
        }

        public IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema) where T : class
        {
            return CreateStructureBatches(items, structureSchema, items.Count).SelectMany(s => s);
        }

        public IEnumerable<IStructure[]> CreateStructureBatches<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize) where T : class
        {
            var batchSize = items.Count() > maxBatchSize ? maxBatchSize : items.Count();

            var batchNo = 0;
            while (true)
            {
                var sourceBatch = items.Skip(batchNo * batchSize).Take(batchSize).ToArray();
                if (sourceBatch.Length < 1)
                    yield break;

                var structures = new IStructure[sourceBatch.Length];
                var ids = _structureIdGenerator.CreateIds(sourceBatch.Length).ToArray();

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