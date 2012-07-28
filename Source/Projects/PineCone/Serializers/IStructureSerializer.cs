using PineCone.Structures.Schemas;

namespace PineCone.Serializers
{
    public interface IStructureSerializer
    {
        string Serialize<T>(T structure, IStructureSchema structureSchema) where T : class;
    }
}