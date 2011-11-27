namespace PineCone.Structures
{
    public interface IStructureIdStrategyResult<out T> where T : class 
    {
        IStructureId StructureId { get; }

        T Item { get; }
    }
}