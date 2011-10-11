using System.Collections;
using System.Collections.Generic;
using EnsureThat;
using NCore.Reflections;
using PineCone.Structures;

namespace PineCone.Dynamic
{
    public class DynamicStructureBuilder
    {
        private readonly IStructureIdGenerator _structureIdGenerator;

        public DynamicStructureBuilder(IStructureIdGenerator structureIdGenerator)
        {
            Ensure.That(structureIdGenerator).IsNotNull();

            _structureIdGenerator = structureIdGenerator;
        }

        public IStructure CreateStructure(DynamicStructure dynamicStructure)
        {
            var typeDescriptor = dynamicStructure.Descriptor;
            var kvs = dynamicStructure.ToDictionary();

            var id = _structureIdGenerator.CreateId(null);
            dynamicStructure.StructureId = id;

            var indexes = new List<IStructureIndex>();
            foreach (var mem in typeDescriptor)
            {
                var value = kvs[mem.Name];

                if (!mem.Type.IsEnumerableType())
                    indexes.Add(new StructureIndex(id, mem.Name, value, mem.Type));
                else
                {
                    var subIndexes = new List<IStructureIndex>();

                    foreach (var element in (ICollection)value)
                        subIndexes.Add(new StructureIndex(id, mem.Name, element, mem.Type));

                    indexes.AddRange(subIndexes);
                }
            }

            return new Structure(dynamicStructure.Name, id, indexes);
        }
    }
}