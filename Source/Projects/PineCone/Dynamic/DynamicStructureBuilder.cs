using System.Collections;
using System.Collections.Generic;
using EnsureThat;
using NCore.Reflections;
using PineCone.Structures;

namespace PineCone.Dynamic
{
    public class DynamicStructureBuilder
    {
        public IStructure CreateStructure(DynamicStructure dynamicStructure)
        {
            Ensure.That(dynamicStructure, "dynamicStructure").IsNotNull();

            var typeDescriptor = dynamicStructure.Descriptor;
            var kvs = dynamicStructure.ToDictionary();

            var id = StructureId.Create(SequentialGuid.New());
            dynamicStructure.StructureId = id;

            var indexes = new List<IStructureIndex>();
            foreach (var mem in typeDescriptor)
            {
                var value = kvs[mem.Name];

                if (!mem.Type.IsEnumerableType())
                    indexes.Add(new StructureIndex(id, mem.Name, value, mem.Type, mem.Type.ToDataTypeCode()));
                else
                {
                    var subIndexes = new List<IStructureIndex>();

                    foreach (var element in (ICollection)value)
                        subIndexes.Add(new StructureIndex(id, mem.Name, element, mem.Type, mem.Type.ToDataTypeCode()));

                    indexes.AddRange(subIndexes);
                }
            }

            return new Structure(dynamicStructure.Name, id, indexes.ToArray());
        }
    }
}