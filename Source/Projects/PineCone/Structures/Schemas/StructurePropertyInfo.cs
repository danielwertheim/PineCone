using System;
using EnsureThat;
using PineCone.Annotations;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructurePropertyInfo
    {
        public readonly IStructureProperty Parent;
        public readonly string Name;
        public readonly Type DataType;
        public readonly DataTypeCode DataTypeCode;
        public readonly bool IsEnumerable;
        public readonly bool IsElement;
        public readonly UniqueMode? UniqueMode;

        public StructurePropertyInfo(string name, Type dataType, DataTypeCode dataTypeCode, IStructureProperty parent = null, UniqueMode? uniqueMode = null)
        {
            Ensure.That(name, "name").IsNotNullOrWhiteSpace();
            Ensure.That(dataType, "dataType").IsNotNull();

            Parent = parent;
            Name = name;
            DataType = dataType;
            DataTypeCode = dataTypeCode;
            UniqueMode = uniqueMode;
        }
    }
}