using System;
using System.Collections.Generic;
using PineCone.Structures.Schemas.Builders;

namespace PineCone.Structures.Schemas
{
    public interface IStructureSchemas
    {
        IStructureTypeFactory StructureTypeFactory { get; set; }
        
        ISchemaBuilder SchemaBuilder { get; set; }

        IStructureSchema GetSchema<T>() where T : class;

        IStructureSchema GetSchema(Type type);

        IEnumerable<IStructureSchema> GetSchemas();

        void RemoveSchema(Type type);
        
        void Clear();
    }
}