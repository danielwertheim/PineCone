using System.Collections.Generic;

namespace PineCone.Structures
{
    public interface IStructureIdGenerator
    {
        StructureId CreateId();
        IEnumerable<StructureId> CreateIds(int numOfIds);
    }
}