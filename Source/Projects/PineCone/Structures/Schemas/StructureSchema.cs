using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Structures.Schemas
{
    public class StructureSchema : IStructureSchema
    {
        public const string IdMemberName = "StructureId";

        public string Name { get; private set; }

        public IIdAccessor IdAccessor { get; private set; }

        public IList<IIndexAccessor> IndexAccessors { get; private set; }

        public IList<IIndexAccessor> UniqueIndexAccessors { get; private set; }
        
        public StructureSchema(string name, IIdAccessor idAccessor, ICollection<IIndexAccessor> indexAccessors = null)
        {
            Ensure.That(name, "name").IsNotNullOrWhiteSpace();
            Ensure.That(idAccessor, "idAccessor").IsNotNull();

            Name = name;
            IdAccessor = idAccessor;
            
            IndexAccessors = indexAccessors != null ? new List<IIndexAccessor>(indexAccessors) 
                : new List<IIndexAccessor>();

            UniqueIndexAccessors = indexAccessors != null ? new List<IIndexAccessor>(indexAccessors.Where(iac => iac.IsUnique))
                : new List<IIndexAccessor>();
        }
    }
}