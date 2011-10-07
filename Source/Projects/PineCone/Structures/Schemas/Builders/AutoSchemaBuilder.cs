﻿using System.Linq;
using EnsureThat;
using NCore;
using NCore.Cryptography;
using NCore.Reflections;
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
        }

        public IStructureSchema CreateSchema(IStructureType structureType)
        {
            Ensure.That(structureType, "structureType").IsNotNull();

            var idAccessor = GetIdAccessor(structureType);
            var indexAccessors = GetIndexAccessors(structureType);
            if (indexAccessors == null || indexAccessors.Length < 1)
                throw new PineConeException(ExceptionMessages.AutoSchemaBuilder_MissingIndexableMembers.Inject(structureType.Name));

            var schemaName = structureType.Name;
            var schemaHash = _hashService.GenerateHash(schemaName);

            return new StructureSchema(schemaName, schemaHash, idAccessor, indexAccessors);
        }

        private static IIdAccessor GetIdAccessor(IStructureType structureType)
        {
            if (structureType.IdProperty == null)
                throw new PineConeException(ExceptionMessages.AutoSchemaBuilder_MissingIdMember.Inject(structureType.Name));

            if (structureType.IdProperty.PropertyType.IsGuidType() || structureType.IdProperty.PropertyType.IsNullableGuidType()
                || (structureType.IdProperty.PropertyType.IsIntType() || structureType.IdProperty.PropertyType.IsNullableIntType()))
                return new IdAccessor(structureType.IdProperty);

            throw new PineConeException(ExceptionMessages.AutoSchemaBuilder_UnsupportedIdAccessorType.Inject(structureType.IdProperty.Name));
        }

        private static IIndexAccessor[] GetIndexAccessors(IStructureType structureType)
        {
            return structureType.IndexableProperties
                .Select(p => new IndexAccessor(p))
                .ToArray();
        }
    }
}