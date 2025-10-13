using System.Threading.Tasks;
using UnityEngine;

namespace DTech.DataPersistence.StorageProviders
{
	public sealed class PreferenceStorageProvider : IStorageProvider
	{
		public bool ContainsKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public Task<bool> WriteAsync(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
			return Task.FromResult(true);
		}

		public Task<StorageReadResponse> ReadAsync(string key, string defaultValue)
		{
			string result = PlayerPrefs.GetString(key, defaultValue);
			var response = new StorageReadResponse(true, result);
			return Task.FromResult(response);
		}

		public void Remove(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}
	}
}