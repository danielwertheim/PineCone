namespace PineCone.Serializers
{
    public interface ISerializer
    {
        dynamic Serialize<T>(T item);
    }
}