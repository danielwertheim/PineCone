namespace PineCone.Structures
{
    public class StructureIdStrategyResult<T> : IStructureIdStrategyResult<T> where T : class
    {
        public IStructureId StructureId { get; private set; }

        public T Item { get; private set; }

        public StructureIdStrategyResult(IStructureId structureId, T item)
        {
            StructureId = structureId;
            Item = item;
        }
    }
}