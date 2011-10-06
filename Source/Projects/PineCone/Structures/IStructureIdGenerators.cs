namespace PineCone.Structures
{
    public interface IStructureIdGenerators
    {
        bool Contains(StructureIdTypes idType);

        void Register(StructureIdTypes idType, IStructureIdGenerator structureIdGenerator);

        IStructureIdGenerator Get(StructureIdTypes idType);
    }
}