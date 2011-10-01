namespace PineCone.Structures.Schemas.Builders
{
    public interface ISchemaBuilder
    {
        IStructureSchema CreateSchema(IStructureType structureType);
    }
}