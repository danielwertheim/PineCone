using System;
using System.Collections.Generic;
using NCore.Validation;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructureType : IStructureType
    {
        public string Name { get; private set; }

        public IStructureProperty IdProperty { get; private set; }

        public IEnumerable<IStructureProperty> IndexableProperties { get; private set; }

        public StructureType(string name, IStructureProperty idProperty, IEnumerable<IStructureProperty> indexableProperties)
        {
            Ensure.Param(name, "name").HasNonWhiteSpaceValue();
            Name = name;

            Ensure.Param(idProperty, "idProperty").IsNotNull();
            IdProperty = idProperty;

            Ensure.Param(indexableProperties, "indexableProperties").IsNotNull();
            IndexableProperties = indexableProperties;
        }
    }
}