using System.Collections.Generic;
using EnsureThat;
using PineCone.Structures;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.Builders;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone
{
    public class PineConizer : IPineConizer
    {
        private IStructureSchemas _schemas;
        private IStructureBuilder _builder;

        public IStructureSchemas Schemas
        {
            get { return _schemas; }
            set
            {
                Ensure.That(value, "Schemas").IsNotNull();

                _schemas = value;
            }
        }
        
        public IStructureBuilder Builder
        {
            get { return _builder; }
            set
            {
                Ensure.That(value, "Builder").IsNotNull();

                _builder = value;
            }
        }

        public PineConizer()
        {
            var structureTypeFactory = new StructureTypeFactory();
            var schemaBuilder = new AutoSchemaBuilder();

            Schemas = new StructureSchemas(structureTypeFactory, schemaBuilder);
            Builder = new StructureBuilder();
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