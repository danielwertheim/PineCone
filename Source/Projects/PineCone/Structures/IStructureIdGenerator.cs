using System.Collections.Generic;
using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public interface IStructureIdGenerator
    {
        IStructureId CreateId(IStructureSchema structureSchema);
        IEnumerable<IStructureId> CreateIds(int numOfIds, IStructureSchema structureSchema);
    }
}