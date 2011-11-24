using PineCone.Serializers;

namespace PineCone.Structures
{
    public interface IStructureBuilderOptions
    {
        IStructureIndexesFactory IndexesFactory { get; set; }
        IStructureIdStrategy StructureIdStrategy { get; set; }
        IStructureSerializer StructureSerializer { get; set; }
    }
}