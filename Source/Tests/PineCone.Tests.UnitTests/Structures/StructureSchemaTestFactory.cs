﻿using System;
using PineCone.Structures.Schemas;
using PineCone.Structures.Schemas.Builders;

namespace PineCone.Tests.UnitTests.Structures
{
    internal static class StructureSchemaTestFactory
    {
        private static readonly IStructureTypeFactory StructureTypeFactory = new StructureTypeFactory();
        private static readonly IStructureSchemaBuilder StructureSchemaBuilder = new AutoStructureSchemaBuilder();

        internal static IStructureSchema CreateRealFrom<T>() where T : class
        {
            return StructureSchemaBuilder.CreateSchema(StructureTypeFactory.CreateFor<T>());
        }

        internal static IStructureSchema CreateRealFrom(Type type)
        {
            return StructureSchemaBuilder.CreateSchema(StructureTypeFactory.CreateFor(type));
        }

        internal static IStructureSchema CreateRealFrom(IStructureType structureType)
        {
            return StructureSchemaBuilder.CreateSchema(structureType);
        }
    }
}