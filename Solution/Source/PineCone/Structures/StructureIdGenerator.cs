using System;

namespace PineCone.Structures
{
    internal static class StructureIdGenerator
    {
        internal static Guid CreateId()
        {
            return SequentialGuid.NewSqlCompatibleGuid();
        }

        internal static Guid[] CreateIds(int numOfIds)
        {
            var ids = new Guid[numOfIds];

            for (var c = 0; c < numOfIds; c++)
                ids[c] = SequentialGuid.NewSqlCompatibleGuid();

            return ids;
        }
    }
}