using PineCone.Structures.Schemas;

namespace PineCone.Serializers
{
    public interface IStructureSerializer
    {
        string Serialize<T>(T item, IStructureSchema structureSchema) where T : class;
    }
}