using System;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.Builders;

namespace PineCone.Tests.UnitTests.Structures
{
    internal static class StructureSchemaTestFactory
    {
        private static readonly IStructureTypeFactory StructureTypeFactory = new StructureTypeFactory();
        private static readonly ISchemaBuilder SchemaBuilder = new AutoSchemaBuilder();

        internal static IStructureSchema CreateRealFrom<T>() where T : class
        {
            return SchemaBuilder.CreateSchema(StructureTypeFactory.CreateFor<T>());
        }

        internal static IStructureSchema CreateRealFrom(Type type)
        {
            return SchemaBuilder.CreateSchema(StructureTypeFactory.CreateFor(type));
        }

        internal static IStructureSchema CreateRealFrom(IStructureType structureType)
        {
            return SchemaBuilder.CreateSchema(structureType);
        }
    }
}