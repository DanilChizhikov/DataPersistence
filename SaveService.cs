using System.Threading.Tasks;
using UnityEngine;

namespace DTech.DataPersistence
{
	public sealed class SaveService : ISaveService
	{
		private readonly ISerializer _serializer;
		private readonly ICryptographer _cryptographer;
		private readonly IStorageProvider _storageProvider;
		
		public async Task SaveAsync<T>(string key, T value)
		{
			string encryptedValue = SerializeAndEncrypt(value);
			await _storageProvider.WriteAsync(key, encryptedValue);
		}

		public async Task<T> LoadAsync<T>(string key, T defaultValue)
		{
			T result = defaultValue;
			string encryptedDefaultValue = SerializeAndEncrypt(defaultValue);
			WriterReadResponse response = await _storageProvider.ReadAsync(key, encryptedDefaultValue);
			if (!response.Success)
			{
				Debug.LogError($"[{nameof(SaveService)}] Failed to load value for key: {key} with error: {response.Error}");
			}
			else
			{
				result = _serializer.Deserialize<T>(response.Result);
			}

			return result;
		}

		public void Remove(string key)
		{
			_storageProvider.Remove(key);
		}

		private string SerializeAndEncrypt<T>(T value)
		{
			string serializedValue = _serializer.Serialize(value);
			string encryptedValue = _cryptographer.Encrypt(serializedValue);
			return encryptedValue;
		}
	}
}