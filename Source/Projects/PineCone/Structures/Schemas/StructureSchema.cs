using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Structures.Schemas
{
	public class StructureSchema : IStructureSchema
    {
        public string Name { get; private set; }

        public string Hash { get; private set; }

		public bool HasId
		{
			get { return IdAccessor != null; }
		}

		public IIdAccessor IdAccessor { get; private set; }

        public IList<IIndexAccessor> IndexAccessors { get; private set; }

        public IList<IIndexAccessor> UniqueIndexAccessors { get; private set; }
        
        public StructureSchema(string name, string hash, IIdAccessor idAccessor = null, ICollection<IIndexAccessor> indexAccessors = null)
        {
            Ensure.That(name, "name").IsNotNullOrWhiteSpace();
            Ensure.That(hash, "hash").IsNotNullOrWhiteSpace();

            Name = name;
            Hash = hash;
            IdAccessor = idAccessor;
            
            IndexAccessors = indexAccessors != null ? new List<IIndexAccessor>(indexAccessors) 
                : new List<IIndexAccessor>();

            UniqueIndexAccessors = indexAccessors != null ? new List<IIndexAccessor>(indexAccessors.Where(iac => iac.IsUnique))
                : new List<IIndexAccessor>();
        }
    }
}