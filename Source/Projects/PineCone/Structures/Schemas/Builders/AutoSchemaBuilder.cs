using EnsureThat;
using NCore;
using NCore.Cryptography;
using PineCone.Resources;
using PineCone.Structures.Schemas.MemberAccessors;

namespace PineCone.Structures.Schemas.Builders
{
	public class AutoSchemaBuilder : ISchemaBuilder
    {
        private readonly IHashService _hashService;

        public AutoSchemaBuilder()
        {
            _hashService = new Crc32HashService();

        	AllowMissingIdMember = false;
        }

		public bool AllowMissingIdMember { get; set; }

    	public IStructureSchema CreateSchema(IStructureType structureType)
        {
            Ensure.That(structureType, "structureType").IsNotNull();

            var idAccessor = GetIdAccessor(structureType);
    	    var concurrencyTokenAccessor = GetConcurrencyTokenAccessor(structureType);
    	    var timeStampAccessor = GetTimeStampAccessor(structureType);
            var indexAccessors = GetIndexAccessors(structureType);
            if (indexAccessors == null || indexAccessors.Length < 1)
                throw new PineConeException(ExceptionMessages.AutoSchemaBuilder_MissingIndexableMembers.Inject(structureType.Name));

            var schemaHash = _hashService.GenerateHash(structureType.Name);

			return new StructureSchema(structureType, schemaHash, idAccessor, concurrencyTokenAccessor, timeStampAccessor, indexAccessors);
        }

        private IIdAccessor GetIdAccessor(IStructureType structureType)
        {
			if (structureType.IdProperty == null)
			{
				if(AllowMissingIdMember)
					return null;

				throw new PineConeException(ExceptionMessages.AutoSchemaBuilder_MissingIdMember.Inject(structureType.Name));
			}

            return new IdAccessor(structureType.IdProperty);
        }

        private IConcurrencyTokenAccessor GetConcurrencyTokenAccessor(IStructureType structureType)
        {
            return structureType.ConcurrencyTokenProperty == null 
                ? null 
                : new ConcurrencyTokenAccessor(structureType.ConcurrencyTokenProperty);
        }

        private ITimeStampAccessor GetTimeStampAccessor(IStructureType structureType)
        {
            return structureType.TimeStampProperty == null
                ? null
                : new TimeStampAccessor(structureType.TimeStampProperty);
        }

	    private static IIndexAccessor[] GetIndexAccessors(IStructureType structureType)
        {
        	var accessors = new IIndexAccessor[structureType.IndexableProperties.Length];

			for (var i = 0; i < accessors.Length; i++)
				accessors[i] = new IndexAccessor(structureType.IndexableProperties[i]);

        	return accessors;
        }
    }
}