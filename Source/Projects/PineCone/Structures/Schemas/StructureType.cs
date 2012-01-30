using System;
using EnsureThat;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructureType : IStructureType
    {
		public Type Type { get; private set; }

    	public string Name
    	{
			get { return Type.Name; }
    	}

        public IStructureProperty IdProperty { get; private set; }

		public IStructureProperty[] IndexableProperties { get; private set; }

        public StructureType(Type type, IStructureProperty idProperty = null, IStructureProperty[] indexableProperties = null)
        {
			Ensure.That(type, "type").IsNotNull();

			Type = type;
            IdProperty = idProperty;
            IndexableProperties = indexableProperties ?? new IStructureProperty[]{};
        }
    }
}