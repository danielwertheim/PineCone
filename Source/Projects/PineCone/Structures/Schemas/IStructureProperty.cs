using System;
using PineCone.Annotations;

namespace PineCone.Structures.Schemas
{
    public interface IStructureProperty
    {
        string Name { get; }
        string Path { get; }
        Type DataType { get; }
        DataTypeCode DataTypeCode { get; }
        IStructureProperty Parent { get; }
        bool IsRootMember { get; }
        bool IsUnique { get; }
        UniqueMode? UniqueMode { get; }
        bool IsEnumerable { get; }
        bool IsElement { get; }
        Type ElementDataType { get; }
        DataTypeCode? ElementDataTypeCode { get; }
        bool IsReadOnly { get; }
        object GetValue(object item);
        void SetValue(object target, object value);
    }
}