using System;
using System.Linq;
using NCore.Reflections;

namespace PineCone.Structures.Schemas
{
    public class DataTypeConverter : IDataTypeConverter
    {
        public Func<string, bool> MemberNameIsForTextType { get; set; }

        public static readonly string[] DefaultTextDataTypeConventions = new[] { "Text", "Content", "Description" };

        public DataTypeConverter()
        {
            MemberNameIsForTextType = OnMemberNameIsForTextType;
        }

        protected virtual bool OnMemberNameIsForTextType(string memberName)
        {
            return DefaultTextDataTypeConventions.Any(convention => memberName.EndsWith(convention, StringComparison.OrdinalIgnoreCase));
        }

        public virtual DataTypeCode Convert(IStructureProperty property)
        {
            var type = property.ElementDataType ?? property.DataType;

            if (type.IsAnyIntegerNumberType())
                return DataTypeCode.IntegerNumber;

            if (type.IsAnyFractalNumberType())
                return DataTypeCode.FractalNumber;

            if (type.IsAnyBoolType())
                return DataTypeCode.Bool;

            if (type.IsAnyDateTimeType())
                return DataTypeCode.DateTime;

            if (type.IsAnyGuidType())
                return DataTypeCode.Guid;

            if (type.IsStringType())
            {
                return MemberNameIsForTextType(property.Name)
                           ? DataTypeCode.Text
                           : DataTypeCode.String;
            }

            if (type.IsAnyEnumType())
                return DataTypeCode.Enum;

            return DataTypeCode.Unknown;
        }
    }
}