namespace Pull
{
    public interface IPull<T, U>
    {
        U Get(T obj);
        void Return(U obj);
    }
}