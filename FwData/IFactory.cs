namespace FwData
{
    public interface IFactory<T, G> where G : System.Enum
    {
        T Get(G type);
    }
}