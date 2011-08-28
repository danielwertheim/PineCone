using System;
using System.Collections.Generic;
using System.Linq;
using NCore;
using NCore.Validation;
using PineCone.Resources;

namespace PineCone.Structures
{
    [Serializable]
    public class Structure : IStructure, IEquatable<IStructure>
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public dynamic Data { get; set; }

        public IList<IStructureIndex> Indexes { get; private set; }

        public IList<IStructureIndex> Uniques { get; private set; }

        private Structure()
        {
            Indexes = new List<IStructureIndex>();
            Uniques = new List<IStructureIndex>();
        }

        public Structure(string name, Guid id, ICollection<IStructureIndex> indexes, dynamic data = null)
        {
            Ensure.Param(name, "name").HasNonWhiteSpaceValue();
            Name = name;

            Ensure.Param(id, "id").IsNotEmpty();
            Id = id;
            
            Data = data;

            Indexes = new List<IStructureIndex>(indexes);
            Uniques = new List<IStructureIndex>(indexes.Where(i => i.IsUnique));

            if (Uniques.Count > 0)
            {
                var firstUniqueNotBeingUnique =
                    Uniques.FirstOrDefault(u => indexes.Count(i => i.Path.Equals(u.Path)) > 1);
                if (firstUniqueNotBeingUnique != null)
                {
                    var idValue = Sys.StringConverter.AsString(firstUniqueNotBeingUnique.StructureId);
                    var uniqueValue = Sys.StringConverter.AsString(firstUniqueNotBeingUnique.Value);
                    throw new PineConeException(
                        ExceptionMessages.Structure_DuplicateUniques.Inject(
                            Name,
                            idValue,
                            firstUniqueNotBeingUnique.Path,
                            uniqueValue));
                }
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IStructure);
        }

        public bool Equals(IStructure other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id) && Equals(other.Name, Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}