using System;
using System.Collections.Generic;
using EnsureThat;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructureType : IStructureType
    {
        public string Name { get; private set; }

        public IStructureProperty IdProperty { get; private set; }

        public IEnumerable<IStructureProperty> IndexableProperties { get; private set; }

        public StructureType(string name, IStructureProperty idProperty, IStructureProperty[] indexableProperties)
        {
            Ensure.That(name, "name").IsNotNullOrWhiteSpace();
            Ensure.That(idProperty, "idProperty").IsNotNull();
            Ensure.That(indexableProperties, "indexableProperties").IsNotNull();
            
            Name = name;
            IdProperty = idProperty;
            IndexableProperties = indexableProperties;
        }
    }
}