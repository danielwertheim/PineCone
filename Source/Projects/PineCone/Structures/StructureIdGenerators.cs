using System.Collections.Generic;

namespace PineCone.Structures
{
    public class StructureIdGenerators : IStructureIdGenerators
    {
        private readonly Dictionary<StructureIdTypes, IStructureIdGenerator> _structureIdGenerators;

        public StructureIdGenerators()
        {
            _structureIdGenerators = new Dictionary<StructureIdTypes, IStructureIdGenerator>();

            Register(StructureIdTypes.Guid, new GuidStructureIdGenerator());
        }

        public bool Contains(StructureIdTypes idType)
        {
            return _structureIdGenerators.ContainsKey(idType);
        }

        public void Register(StructureIdTypes idType, IStructureIdGenerator structureIdGenerator)
        {
            _structureIdGenerators.Add(idType, structureIdGenerator);
        }

        public IStructureIdGenerator Get(StructureIdTypes idType)
        {
            return _structureIdGenerators[idType];
        }
    }
}