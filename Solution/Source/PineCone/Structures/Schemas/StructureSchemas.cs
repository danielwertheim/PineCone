using System;
using System.Collections.Generic;
using NCore.Validation;
using PineCone.Structures.Schemas.Builders;

namespace PineCone.Structures.Schemas
{
    public class StructureSchemas : IStructureSchemas
    {
        private readonly Dictionary<string, IStructureSchema> _schemas;

        public IStructureTypeFactory StructureTypeFactory { get; set; }
        
        public ISchemaBuilder SchemaBuilder { get; set; }
        
        public StructureSchemas(IStructureTypeFactory structureTypeFactory, ISchemaBuilder schemaBuilder)
        {
            Ensure.Param(structureTypeFactory, "structureTypeFactory").IsNotNull();
            StructureTypeFactory = structureTypeFactory;

            Ensure.Param(schemaBuilder, "schemaBuilder").IsNotNull();
            SchemaBuilder = schemaBuilder;

            _schemas = new Dictionary<string, IStructureSchema>();
        }

        public IStructureSchema GetSchema<T>() where T : class 
        {
            return GetSchema(TypeFor<T>.Type);
        }

        public IStructureSchema GetSchema(Type type)
        {
            Ensure.Param(type, "type").IsNotNull();

            if (!_schemas.ContainsKey(type.Name))
                Register(type);

            return _schemas[type.Name];
        }

        public void RemoveSchema(Type type)
        {
            Ensure.Param(type, "type").IsNotNull();

            _schemas.Remove(type.Name);
        }

        public void Clear()
        {
            _schemas.Clear();
        }

        private void Register(Type type)
        {
            var structureType = StructureTypeFactory.CreateFor(type);

            _schemas.Add(
                structureType.Name,
                SchemaBuilder.CreateSchema(structureType));
        }
    }
}