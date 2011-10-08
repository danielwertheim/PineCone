namespace PineCone.Serializers
{
    public interface ISerializer
    {
        string Serialize<T>(T item) where T : class;
    }
}