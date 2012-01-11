namespace PineCone.Structures.Schemas.Builders
{
    public interface ISchemaBuilder
    {
		bool AllowMissingIdMember { get; set; }

    	IStructureSchema CreateSchema(IStructureType structureType);
    }
}