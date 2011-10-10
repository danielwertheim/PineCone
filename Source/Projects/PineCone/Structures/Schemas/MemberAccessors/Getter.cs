using System;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    internal static class Getter
    {
        internal static IGetter For(StructureIdTypes structureIdType, bool isNullable)
        {
            switch (structureIdType)
            {
                case StructureIdTypes.Guid:
                    return isNullable ? (IGetter)new NullableGuidGetter() : new GuidGetter();
                case StructureIdTypes.Identity:
                    return isNullable ? (IGetter)new NullableIntGetter() : new IntGetter();
                case StructureIdTypes.BigIdentity:
                    return isNullable ? (IGetter)new NullableLongGetter() : new LongGetter();
                default:
                    throw new ArgumentOutOfRangeException("structureIdType");
            }
        }

        internal interface IGetter
        {
            IStructureId GetValue<T>(T item, IStructureProperty property) where T : class;
        }

        private class GuidGetter : IGetter
        {
            public IStructureId GetValue<T>(T item, IStructureProperty property) where T : class
            {
                return StructureId.Create((Guid)property.GetValue(item));
            }
        }

        private class NullableGuidGetter : IGetter
        {
            public IStructureId GetValue<T>(T item, IStructureProperty property) where T : class
            {
                return StructureId.Create((Guid?)property.GetValue(item));
            }
        }

        private class IntGetter : IGetter
        {
            public IStructureId GetValue<T>(T item, IStructureProperty property) where T : class
            {
                return StructureId.Create((int)property.GetValue(item));
            }
        }

        private class NullableIntGetter : IGetter
        {
            public IStructureId GetValue<T>(T item, IStructureProperty property) where T : class
            {
                return StructureId.Create((int?)property.GetValue(item));
            }
        }

        private class LongGetter : IGetter
        {
            public IStructureId GetValue<T>(T item, IStructureProperty property) where T : class
            {
                return StructureId.Create((long)property.GetValue(item));
            }
        }

        private class NullableLongGetter : IGetter
        {
            public IStructureId GetValue<T>(T item, IStructureProperty property) where T : class
            {
                return StructureId.Create((long?)property.GetValue(item));
            }
        }
    }
}