using System;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    internal static class Setter
    {
        internal static ISetter For(StructureIdTypes structureIdType, bool isNullable)
        {
            switch (structureIdType)
            {
                case StructureIdTypes.Guid:
                    return isNullable ? (ISetter)new NullableGuidSetter() : new GuidSetter();
                case StructureIdTypes.Identity:
                    return isNullable ? (ISetter)new NullableIntSetter() : new IntSetter();
                case StructureIdTypes.BigIdentity:
                    return isNullable ? (ISetter)new NullableLongSetter() : new LongSetter();
                default:
                    throw new ArgumentOutOfRangeException("structureIdType");
            }
        }

        internal interface ISetter
        {
            void SetValue<T>(T item, IStructureId id, IStructureProperty property) where T : class;
        }

        private class GuidSetter : ISetter
        {
            public void SetValue<T>(T item, IStructureId id, IStructureProperty property) where T : class
            {
                property.SetValue(item, (Guid)id.Value);
            }
        }

        private class NullableGuidSetter : ISetter
        {
            public void SetValue<T>(T item, IStructureId id, IStructureProperty property) where T : class
            {
                property.SetValue(item, (Guid?)id.Value);
            }
        }

        private class IntSetter : ISetter
        {
            public void SetValue<T>(T item, IStructureId id, IStructureProperty property) where T : class
            {
                property.SetValue(item, (int)id.Value);
            }
        }

        private class NullableIntSetter : ISetter
        {
            public void SetValue<T>(T item, IStructureId id, IStructureProperty property) where T : class
            {
                property.SetValue(item, (int?)id.Value);
            }
        }

        private class LongSetter : ISetter
        {
            public void SetValue<T>(T item, IStructureId id, IStructureProperty property) where T : class
            {
                property.SetValue(item, (long)id.Value);
            }
        }

        private class NullableLongSetter : ISetter
        {
            public void SetValue<T>(T item, IStructureId id, IStructureProperty property) where T : class
            {
                property.SetValue(item, (long?)id.Value);
            }
        }
    }
}