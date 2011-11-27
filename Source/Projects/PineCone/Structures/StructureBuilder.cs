using System.Collections.Generic;
using EnsureThat;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class StructureBuilder : IStructureBuilder
    {
        private IStructureBuilderOptions _options;

        public IStructureBuilderOptions Options
        {
            get { return _options; }
            set
            {
                Ensure.That(value, "Options").IsNotNull();

                _options = value;
            }
        }

        public StructureBuilder()
        {
            Options = new DefaultStructureBuilderOptions();
        }

        public virtual IStructure CreateStructure<T>(T item, IStructureSchema structureSchema) where T : class
        {
            var structureId = Options.StructureIdStrategy.Apply(structureSchema, item);

            return new Structure(
                structureSchema.Name,
                structureId,
                Options.IndexesFactory.CreateIndexes(structureSchema, item, structureId),
                Options.StructureSerializer.Serialize(item));
        }

        public virtual IEnumerable<IStructure> CreateStructures<T>(IEnumerable<T> items, IStructureSchema structureSchema) where T : class
        {
            foreach (var tuple in Options.StructureIdStrategy.Apply(structureSchema, items))
            {
                yield return new Structure(
                    structureSchema.Name,
                    tuple.StructureId,
                    Options.IndexesFactory.CreateIndexes(structureSchema, tuple.Item, tuple.StructureId),
                    Options.StructureSerializer.Serialize(tuple.Item));
            }
        }
    }
}