using EnsureThat;
using NCore;
using PineCone.Resources;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Structures.Schemas.Builders
{
	public class AutoSchemaBuilder : ISchemaBuilder
    {
    	public virtual IStructureSchema CreateSchema(IStructureType structureType)
        {
            Ensure.That(structureType, "structureType").IsNotNull();

            var idAccessor = GetIdAccessor(structureType);
    	    var concurrencyTokenAccessor = GetConcurrencyTokenAccessor(structureType);
    	    var timeStampAccessor = GetTimeStampAccessor(structureType);
            var indexAccessors = GetIndexAccessors(structureType);
            if (indexAccessors == null || indexAccessors.Length < 1)
                throw new PineConeException(ExceptionMessages.AutoSchemaBuilder_MissingIndexableMembers.Inject(structureType.Name));

			return new StructureSchema(structureType, idAccessor, concurrencyTokenAccessor, timeStampAccessor, indexAccessors);
        }

	    protected virtual IIdAccessor GetIdAccessor(IStructureType structureType)
        {
            if (structureType.IdProperty == null)
                throw new PineConeException(ExceptionMessages.AutoSchemaBuilder_MissingIdMember.Inject(structureType.Name));

            return new IdAccessor(structureType.IdProperty);
        }

	    protected virtual IConcurrencyTokenAccessor GetConcurrencyTokenAccessor(IStructureType structureType)
        {
            return structureType.ConcurrencyTokenProperty == null 
                ? null 
                : new ConcurrencyTokenAccessor(structureType.ConcurrencyTokenProperty);
        }

	    protected virtual ITimeStampAccessor GetTimeStampAccessor(IStructureType structureType)
        {
            return structureType.TimeStampProperty == null
                ? null
                : new TimeStampAccessor(structureType.TimeStampProperty);
        }

	    protected virtual IIndexAccessor[] GetIndexAccessors(IStructureType structureType)
        {
        	var accessors = new IIndexAccessor[structureType.IndexableProperties.Length];

			for (var i = 0; i < accessors.Length; i++)
				accessors[i] = new IndexAccessor(structureType.IndexableProperties[i]);

        	return accessors;
        }
    }
}