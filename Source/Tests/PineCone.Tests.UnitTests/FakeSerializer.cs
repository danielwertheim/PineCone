using System;
using PineCone.Serializers;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests
{
    public class FakeSerializer : IStructureSerializer
    {
        public Func<object, IStructureSchema, string> OnSerialize;

        public string Serialize<T>(T item, IStructureSchema structureSchema) where T : class
        {
            return OnSerialize(item, structureSchema);
        }
    }
}