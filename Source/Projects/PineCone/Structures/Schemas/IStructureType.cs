namespace PineCone.Structures.Schemas
{
    public interface IStructureType
    {
        string Name { get; }
        IStructureProperty IdProperty { get; }
		IStructureProperty[] IndexableProperties { get; }
    }
}