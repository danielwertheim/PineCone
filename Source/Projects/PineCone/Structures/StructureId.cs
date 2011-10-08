using System;
using NCore;
using NCore.Reflections;
using PineCone.Resources;

namespace PineCone.Structures
{
    [Serializable]
    public class StructureId : IStructureId
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

        public static StructureId Create<T>(T value) where T : struct
        {
            return new StructureId(value, typeof(T));
        }

        public static StructureId Create<T>(T? value) where T : struct 
        {
            return new StructureId(value, typeof(T?));
        }

        public static StructureId Create(ValueType value)
        {
            return new StructureId(value, value.GetType());
        }

        public static StructureId Create(ValueType value, Type dataType)
        {
            return new StructureId(value, dataType);
        }

        private StructureId(ValueType value, Type dataType)
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
                return StructureIdTypes.Identity;

            if (type.IsLongType() || type.IsNullableLongType())
                return StructureIdTypes.Identity;

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
            return Equals(obj as IStructureId);
        }

        public bool Equals(IStructureId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Value, Value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}