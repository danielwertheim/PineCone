using System.Collections.Generic;

namespace PineCone.Structures
{
    public interface IStructureIdGenerator
    {
        IStructureId CreateId();
        IEnumerable<IStructureId> CreateIds(int numOfIds);
    }
}