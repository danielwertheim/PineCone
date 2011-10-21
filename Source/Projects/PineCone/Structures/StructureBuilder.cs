using System;
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

            Action<IStructure[], T[], IStructureSchema, StructureBuilderOptions> a;
            if (options.KeepStructureId)
                a = FillStructureArrayFromSourceWithPreservedId;
            else
                a = FillStructureArrayFromSourceWithNewId;
            
            while (true)
            {
                var sourceBatch = items.Skip(batchNo * batchSize).Take(batchSize).ToArray();
                if (sourceBatch.Length < 1)
                    yield break;

                var structures = new IStructure[sourceBatch.Length];
                a.Invoke(structures, sourceBatch, structureSchema, options);
                yield return structures;

                batchNo++;
            }
        }

        private void FillStructureArrayFromSourceWithPreservedId<T>(IStructure[] structures, T[] sourceItems, IStructureSchema structureSchema, StructureBuilderOptions options) where T : class
        {
            Parallel.For(0, sourceItems.Length, i =>
            {
                structures[i] = CreateStructureItemAndKeepId(sourceItems[i], structureSchema, options.Serializer);
            });
        }

        private void FillStructureArrayFromSourceWithNewId<T>(IStructure[] structures, T[] sourceItems, IStructureSchema structureSchema, StructureBuilderOptions options) where T : class
        {
            var ids = options.IdGenerator.CreateIds(sourceItems.Length, structureSchema).ToArray();

            Parallel.For(0, sourceItems.Length, i =>
            {
                structures[i] = CreateStructureItemAndSetNewId(sourceItems[i], structureSchema, ids[i], options.Serializer);
            });
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