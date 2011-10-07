using System.Collections.Generic;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public class GuidStructureIdGenerator : IStructureIdGenerator
    {
        public IStructureId CreateId(IStructureSchema structureSchema)
        {
            return StructureId.Create(SequentialGuid.NewSqlCompatibleGuid());
        }

        public IEnumerable<IStructureId> CreateIds(int numOfIds, IStructureSchema structureSchema)
        {
            for (var c = 0; c < numOfIds; c++)
                yield return StructureId.Create(SequentialGuid.NewSqlCompatibleGuid());
        }
    }
}