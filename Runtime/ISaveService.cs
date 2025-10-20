using System.Threading.Tasks;

namespace DTech.DataPersistence
{
    public interface ISaveService
    {
        bool HasSave(string key);
        Task SaveAsync<T>(string key, T value, bool isCrypted = true);
        Task<T> LoadAsync<T>(string key, T defaultValue, bool isCrypted = true);
        void Remove(string key);
    }
}