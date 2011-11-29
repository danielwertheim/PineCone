using System;
using System.Threading.Tasks;
using PineCone.Structures.Schemas;

namespace PineCone.Structures.IdGenerators
{
    public class GuidStructureIdGenerator : IStructureIdGenerator 
    {
        public IStructureId Generate(IStructureSchema structureSchema)
        {
            return StructureId.Create(SequentialGuid.New());
        }

        public IStructureId[] Generate(IStructureSchema structureSchema, int numOfIds)
        {
            var ids = new IStructureId[numOfIds];

            Parallel.For(0, numOfIds, i => ids[i] = StructureId.Create(Guid.NewGuid()));

            return ids;
        }
    }
}