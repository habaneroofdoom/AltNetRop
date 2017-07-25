namespace Demo.Domain
{
    public interface IDatabase
    {
        T Get<T>(int id) where T: class, IPersisted;
        void Insert<T>(T obj) where T: class, IPersisted;
    }

    public interface IPersisted
    {
        int Id { get; set; }
    }
}