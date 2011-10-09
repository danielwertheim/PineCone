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

        public IStructureIndexesFactory IndexesFactory
        {
            get { return _indexesFactory; }
            set
            {
                Ensure.That(value, "IndexesFactory").IsNotNull();

                _indexesFactory = value;
            }
        }

        public StructureBuilder()
        {
            _indexesFactory = new StructureIndexesFactory();
        }

        public IStructure CreateStructure<T>(T item, IStructureSchema structureSchema, StructureBuilderOptions options = null)
            where T : class
        {
            options = options ?? new StructureBuilderOptions();

            if (options.KeepStructureId)
                return CreateStructureItemAndKeepId(item, structureSchema, options.Serializer);

            var id = options.IdGenerator.CreateId(structureSchema);

            return CreateStructureItemAndSetNewId(item, structureSchema, id, options.Serializer);
        }

        public IEnumerable<IStructure> CreateStructures<T>(ICollection<T> items, IStructureSchema structureSchema, StructureBuilderOptions options = null) where T : class
        {
            return CreateStructureBatches(items, structureSchema, items.Count, options).SelectMany(s => s);
        }

        public IEnumerable<IStructure[]> CreateStructureBatches<T>(ICollection<T> items, IStructureSchema structureSchema, int maxBatchSize, StructureBuilderOptions options = null) where T : class
        {
            options = options ?? new StructureBuilderOptions();
            
            var batchSize = items.Count() > maxBatchSize ? maxBatchSize : items.Count();
            var batchNo = 0;
            
            while (true)
            {
                var sourceBatch = items.Skip(batchNo * batchSize).Take(batchSize).ToArray();
                if (sourceBatch.Length < 1)
                    yield break;

                var structures = new IStructure[sourceBatch.Length];

                if (options.KeepStructureId)
                {
                    Parallel.For(0, sourceBatch.Length, i =>
                    {
                        structures[i] = CreateStructureItemAndKeepId(sourceBatch[i], structureSchema, options.Serializer);
                    });
                }
                else
                {
                    var ids = options.IdGenerator.CreateIds(sourceBatch.Length, structureSchema).ToArray();

                    Parallel.For(0, sourceBatch.Length, i =>
                    {
                        structures[i] = CreateStructureItemAndSetNewId(sourceBatch[i], structureSchema, ids[i], options.Serializer);
                    });
                }
                yield return structures;

                batchNo++;
            }
        }

        private IStructure CreateStructureItemAndKeepId<T>(T item, IStructureSchema structureSchema, ISerializer serializer)
            where T : class
        {
            var structureId = structureSchema.IdAccessor.GetValue(item);

            return new Structure(
                structureSchema.Name,
                structureId,
                IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                serializer.Serialize(item));
        }

        private IStructure CreateStructureItemAndSetNewId<T>(T item, IStructureSchema structureSchema, IStructureId structureId, ISerializer serializer)
            where T : class
        {
            structureSchema.IdAccessor.SetValue(item, structureId);

            return new Structure(
                structureSchema.Name,
                structureId,
                IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                serializer.Serialize(item));
        }
    }
}