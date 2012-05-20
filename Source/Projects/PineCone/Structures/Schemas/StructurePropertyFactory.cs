using System;
using System.Linq;
using System.Reflection;
using NCore.Reflections;
using PineCone.Annotations;

namespace PineCone.Structures.Schemas
{
    public class StructurePropertyFactory : IStructurePropertyFactory
    {
        private static readonly Type UniqueAttributeType = typeof(UniqueAttribute);

        public StructurePropertyFactoryRules Rules { get; protected set; }

        public StructurePropertyFactory()
        {
            Rules = new StructurePropertyFactoryRules();
        }

        public virtual IStructureProperty CreateRootPropertyFrom(PropertyInfo propertyInfo)
        {
            return new StructureProperty(
                GetInfo(propertyInfo),
                DynamicPropertyFactory.GetterFor(propertyInfo),
                DynamicPropertyFactory.SetterFor(propertyInfo));
        }

        public virtual IStructureProperty CreateChildPropertyFrom(IStructureProperty parent, PropertyInfo propertyInfo)
        {
            return new StructureProperty(
                GetInfo(propertyInfo, parent),
                DynamicPropertyFactory.GetterFor(propertyInfo),
                DynamicPropertyFactory.SetterFor(propertyInfo));
        }

        protected virtual StructurePropertyInfo GetInfo(PropertyInfo propertyInfo, IStructureProperty parent = null)
        {
            return new StructurePropertyInfo(
                propertyInfo.Name, 
                propertyInfo.PropertyType, 
                GetDataTypeCode(propertyInfo), 
                parent, 
                GetUniqueMode(propertyInfo));
        }

        protected virtual DataTypeCode GetDataTypeCode(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;

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
                return Rules.MemberNameIsForTextType(propertyInfo.Name) 
                    ? DataTypeCode.Text 
                    : DataTypeCode.String;
            }

            if (type.IsAnyEnumType())
                return DataTypeCode.Enum;

            return DataTypeCode.Unknown;
        }

        protected virtual UniqueMode? GetUniqueMode(PropertyInfo propertyInfo)
        {
            var uniqueAttribute = (UniqueAttribute)propertyInfo.GetCustomAttributes(UniqueAttributeType, true).FirstOrDefault();

            UniqueMode? uniqueMode = null;
            if (uniqueAttribute != null)
                uniqueMode = uniqueAttribute.Mode;

            return uniqueMode;
        }
    }
}