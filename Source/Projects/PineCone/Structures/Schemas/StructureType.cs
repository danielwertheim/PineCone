using System;
using EnsureThat;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructureType : IStructureType
    {
        public string Name { get; private set; }

        public IStructureProperty IdProperty { get; private set; }

		public IStructureProperty[] IndexableProperties { get; private set; }

        public StructureType(string name, IStructureProperty idProperty = null, IStructureProperty[] indexableProperties = null)
        {
            Ensure.That(name, "name").IsNotNullOrWhiteSpace();
            
            Name = name;
            IdProperty = idProperty;
            IndexableProperties = indexableProperties ?? new IStructureProperty[]{};
        }
    }
}