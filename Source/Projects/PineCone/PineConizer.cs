using System.Collections.Generic;
using PineCone.Structures;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.Builders;

namespace PineCone
{
    public class PineConizer
    {
        public IStructureSchemas Schemas{ get; set; }

        public IStructureBuilder Builder { get; set; }

        public PineConizer()
        {
            Schemas = new StructureSchemas(new StructureTypeFactory(), new AutoSchemaBuilder());

            Builder = new StructureBuilder(new GuidStructureIdGenerator(), new StructureIndexesFactory());
        }

        public IStructure CreateStructureFor<T>(T item) where T : class
        {
            return Builder.CreateStructure(item, Schemas.GetSchema<T>());
        }

        public IEnumerable<IStructure> CreateStructuresFor<T>(ICollection<T> items) where T : class
        {
            return Builder.CreateStructures(items, Schemas.GetSchema<T>());
        }

        public IEnumerable<IStructure[]> CreateStructureBatches<T>(ICollection<T> items, int maxBatchSize) where T : class
        {
            return Builder.CreateStructureBatches(items, Schemas.GetSchema<T>(), maxBatchSize);
        }
    }
}