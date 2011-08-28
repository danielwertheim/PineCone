using System.Collections;
using System.Collections.Generic;
using NCore;
using NCore.Reflections;
using PineCone.Structures;

namespace PineCone.Dynamic
{
    public class DynamicStructureBuilder
    {
        public IStructure CreateStructure(DynamicStructure dynamicStructure)
        {
            var ts = dynamicStructure.Descriptor;
            var kvs = dynamicStructure.ToDictionary();

            var id = StructureIdGenerator.CreateId();
            dynamicStructure.StructureId = id;

            var indexes = new List<IStructureIndex>();
            foreach (var mem in ts)
            {
                var kv = kvs[mem.Name];

                if (!mem.Type.IsEnumerableType())
                    indexes.Add(new StructureIndex(id, mem.Name, kv));
                else
                {
                    var subIndexes = new List<IStructureIndex>();

                    foreach (var value in (ICollection)kv)
                        subIndexes.Add(new StructureIndex(id, mem.Name, value));

                    indexes.AddRange(subIndexes);
                }
            }

            return new Structure(dynamicStructure.Name, id, indexes, dynamicStructure);
        }
    }
}