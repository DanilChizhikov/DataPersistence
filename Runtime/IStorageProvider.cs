using System.Threading.Tasks;

namespace DTech.DataPersistence
{
	public interface IStorageProvider
	{
		bool ContainsKey(string key);
		Task<bool> WriteAsync(string key, string value);
		Task<StorageReadResponse> ReadAsync(string key, string defaultValue);
		void Remove(string key);
	}
}