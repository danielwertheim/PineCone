namespace PineCone.Structures.Schemas.MemberAccessors
{
    public interface IIdAccessor : IMemberAccessor
    {
        StructureId GetValue<T>(T item)
            where T : class;

        void SetValue<T>(T item, StructureId value)
            where T : class;
    }
}