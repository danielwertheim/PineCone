using System;
using PineCone.Serializers;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests
{
    public class FakeSerializer : IStructureSerializer
    {
        public Func<object, IStructureSchema, string> OnSerialize;

        public string Serialize<T>(T structure, IStructureSchema structureSchema) where T : class
        {
            return OnSerialize(structure, structureSchema);
        }
    }
}