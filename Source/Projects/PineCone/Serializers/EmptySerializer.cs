namespace PineCone.Serializers
{
    public class EmptySerializer : ISerializer
    {
        public string Serialize<T>(T item)
        {
            return null;
        }
    }
}