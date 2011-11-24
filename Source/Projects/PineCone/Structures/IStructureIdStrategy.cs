using PineCone.Structures.Schemas;

namespace PineCone.Structures
{
    public interface IStructureIdStrategy
    {
        IStructureId Apply<T>(IStructureSchema structureSchema, T item) where T : class ;
    }
}