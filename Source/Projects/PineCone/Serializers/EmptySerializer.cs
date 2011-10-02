namespace PineCone.Serializers
{
    public class EmptySerializer : ISerializer
    {
        public dynamic Serialize<T>(T item)
        {
            return null;
        }
    }
}