using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCore.Validation;
using PineCone.Serializers;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class StructureBuilder : IStructureBuilder
    {
        private ISerializer _serializer;

        public ISerializer Serializer
        {
            get { return _serializer; }
            set { _serializer = value ?? new EmptySerializer(); }
        }

        public IStructureIndexesFactory IndexesFactory { get; private set; }

        public StructureBuilder(IStructureIndexesFactory structureIndexesFactory)
        {
            Ensure.Param(structureIndexesFactory, "structureIndexesFactory").IsNotNull();
            IndexesFactory = structureIndexesFactory;

            Serializer = new EmptySerializer();
        }

        public IStructure CreateStructure<T>(T item, IStructureSchema structureSchema)
            where T : class
        {
            return CreateStructure(
                item,
                structureSchema,
                StructureIdGenerator.CreateId());
        }

        public IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema) where T : class
        {
            return items.Select(i => CreateStructure(i, structureSchema, StructureIdGenerator.CreateId()));
        }

        public IEnumerable<IStructure[]> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize) where T : class
        {
            var batchSize = items.Count() > maxBatchSize ? maxBatchSize : items.Count();

            var batchNo = 0;
            while (true)
            {
                var sourceBatch = items.Skip(batchNo * batchSize).Take(batchSize).ToArray();
                if (sourceBatch.Length < 1)
                    yield break;

                var structures = new IStructure[sourceBatch.Length];
                var ids = StructureIdGenerator.CreateIds(sourceBatch.Length);

                Parallel.For(0, sourceBatch.Length,
                    i =>
                    {
                        var sourceItem = sourceBatch[i];

                        structureSchema.IdAccessor.SetValue(sourceItem, ids[i]);

                        structures[i] = CreateStructure(sourceItem, structureSchema, ids[i]);
                    });

                yield return structures;

                batchNo++;
            }
        }

        private IStructure CreateStructure<T>(T item, IStructureSchema structureSchema, Guid structureId)
            where T : class
        {
            return new Structure(
                structureSchema.Name,
                structureId,
                IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                Serializer.Serialize(item));
        }
    }
}