using System;
using NCore;
using NCore.Reflections;
using PineCone.Resources;

namespace PineCone.Structures
{
    [Serializable]
    public class StructureId : IEquatable<StructureId>
    {
        private readonly StructureIdTypes _idType;
        private readonly ValueType _value;
        private readonly Type _dataType;
        private readonly bool _hasValue;

        public StructureIdTypes IdType
        {
            get { return _idType; }
        }

        public ValueType Value 
        {
            get { return _value; }
        }

        public Type DataType
        {
            get { return _dataType; }
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public StructureId(ValueType value, Type dataType)
        {
            _value = value;
            _hasValue = value != null;
            _dataType = dataType;
            _idType = GetIdTypeFrom(dataType);
        }

        public static StructureIdTypes GetIdTypeFrom(Type type)
        {
            if (type.IsGuidType() || type.IsNullableGuidType())
                return StructureIdTypes.Guid;

            if (type.IsIntType() || type.IsNullableIntType())
                return StructureIdTypes.SmallIdentity;

            if (type.IsLongType() || type.IsNullableLongType())
                return StructureIdTypes.BigIdentity;

            throw new PineConeException(ExceptionMessages.StructureId_InvalidType.Inject(type.Name));
        }

        public static bool IsValidDataType(Type type)
        {
            var isGuidType = type.IsGuidType() || type.IsNullableGuidType();
            var isIntType = type.IsIntType() || type.IsNullableIntType();
            var isLongType = type.IsLongType() || type.IsNullableLongType();

            return isGuidType || isIntType || isLongType;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StructureId);
        }

        public bool Equals(StructureId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._value, _value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}