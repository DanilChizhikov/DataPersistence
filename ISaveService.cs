using System.Threading.Tasks;

namespace DTech.DataPersistence
{
    public interface ISaveService
    {
        Task SaveAsync<T>(string key, T value);
        Task<T> LoadAsync<T>(string key, T defaultValue);
        void Remove(string key);
    }
}