using System;
using System.Collections.Generic;

namespace PineCone.Structures
{
    public class GuidStructureIdGenerator : IStructureIdGenerator
    {
        private static Type DataType = typeof (Guid);

        public StructureId CreateId()
        {
            return new StructureId(SequentialGuid.NewSqlCompatibleGuid(), DataType);
        }

        public IEnumerable<StructureId> CreateIds(int numOfIds)
        {
            for (var c = 0; c < numOfIds; c++)
                yield return new StructureId(SequentialGuid.NewSqlCompatibleGuid(), DataType);
        }
    }
}