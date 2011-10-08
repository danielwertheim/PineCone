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
        private IStructureIndexesFactory _indexesFactory;
        private IStructureIdGenerator _idGenerator;
        private ISerializer _serializer;

        public IStructureIndexesFactory IndexesFactory
        {
            get { return _indexesFactory; }
            set
            {
                Ensure.That(value, "IndexesFactory").IsNotNull();

                _indexesFactory = value;
            }
        }

        public IStructureIdGenerator IdGenerator
        {
            get { return _idGenerator; }
            set
            {
                Ensure.That(value, "IdGenerator").IsNotNull();

                _idGenerator = value;
            }
        }

        public ISerializer Serializer
        {
            get { return _serializer; }
            set { _serializer = value ?? new EmptySerializer(); }
        }

        public StructureBuilder()
        {
            _indexesFactory = new StructureIndexesFactory();
            _idGenerator = new GuidStructureIdGenerator();

            Serializer = new EmptySerializer();
        }

        public IStructure CreateStructure<T>(T item, IStructureSchema structureSchema, IStructureIdGenerator structureIdGenerator = null)
            where T : class
        {
            var id = (structureIdGenerator ?? IdGenerator).CreateId(structureSchema);

            return CreateStructure(item, structureSchema, id);
        }

        public IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema, IStructureIdGenerator structureIdGenerator = null) where T : class
        {
            return CreateStructureBatches(items, structureSchema, items.Count, structureIdGenerator).SelectMany(s => s);
        }

        public IEnumerable<IStructure[]> CreateStructureBatches<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize, IStructureIdGenerator structureIdGenerator = null) where T : class
        {
            var idGenerator = structureIdGenerator ?? IdGenerator;
            var batchSize = items.Count() > maxBatchSize ? maxBatchSize : items.Count();

            var batchNo = 0;
            while (true)
            {
                var sourceBatch = items.Skip(batchNo * batchSize).Take(batchSize).ToArray();
                if (sourceBatch.Length < 1)
                    yield break;

                var structures = new IStructure[sourceBatch.Length];
                var ids = idGenerator.CreateIds(sourceBatch.Length, structureSchema).ToArray();

                Parallel.For(0, sourceBatch.Length,
                    i =>
                    {
                        structures[i] = CreateStructure(sourceBatch[i], structureSchema, ids[i]);
                    });

                yield return structures;

                batchNo++;
            }
        }

        private IStructure CreateStructure<T>(T item, IStructureSchema structureSchema, IStructureId structureId)
            where T : class
        {
            structureSchema.IdAccessor.SetValue(item, structureId);

            return new Structure(
                structureSchema.Name,
                structureId,
                IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                Serializer.Serialize(item));
        }
    }
}