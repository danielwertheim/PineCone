using System;
using NCore.Reflections;

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

            if(dataType.IsGuidType() || dataType.IsNullableGuidType())
                _idType = StructureIdTypes.Guid;

            if(dataType.IsIntType() || dataType.IsNullableIntType())
                _idType = StructureIdTypes.SmallIdentity;

            if (dataType.IsLongType() || dataType.IsNullableLongType())
                _idType = StructureIdTypes.BigIdentity;
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