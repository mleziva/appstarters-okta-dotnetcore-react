namespace alpha.mongo.netcore.Repository
{
    public interface IAlphaRepository<T>
    {
        void Delete(string id);
        T Get(string id);
        T GetAll();
        void Insert(T insertDocument);
        void Update(T updateDocument);
    }
}