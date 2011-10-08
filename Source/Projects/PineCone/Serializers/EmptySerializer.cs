namespace PineCone.Serializers
{
    public class EmptySerializer : ISerializer
    {
        public string Serialize<T>(T item) where T : class
        {
            return null;
        }
    }
}