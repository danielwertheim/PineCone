using System;
using System.Collections.Generic;

namespace PineCone.Structures
{
    public class GuidStructureIdGenerator : IStructureIdGenerator
    {
        private static readonly Type DataType = typeof (Guid);

        public IStructureId CreateId()
        {
            return new StructureId(SequentialGuid.NewSqlCompatibleGuid(), DataType);
        }

        public IEnumerable<IStructureId> CreateIds(int numOfIds)
        {
            for (var c = 0; c < numOfIds; c++)
                yield return new StructureId(SequentialGuid.NewSqlCompatibleGuid(), DataType);
        }
    }
}