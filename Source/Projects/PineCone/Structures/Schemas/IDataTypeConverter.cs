using System;

namespace PineCone.Structures.Schemas
{
    public interface IDataTypeConverter 
    {
        Func<string, bool> MemberNameIsForTextType { get; set; }
    
        DataTypeCode Convert(IStructureProperty property);
    }
}