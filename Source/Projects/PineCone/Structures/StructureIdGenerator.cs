using System.Collections.Generic;

namespace PineCone.Structures
{
    public class GuidStructureIdGenerator : IStructureIdGenerator
    {
        public IStructureId CreateId()
        {
            return StructureId.Create(SequentialGuid.NewSqlCompatibleGuid());
        }

        public IEnumerable<IStructureId> CreateIds(int numOfIds)
        {
            for (var c = 0; c < numOfIds; c++)
                yield return StructureId.Create(SequentialGuid.NewSqlCompatibleGuid());
        }
    }
}