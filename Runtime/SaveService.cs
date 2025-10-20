using System;
using System.Threading.Tasks;
using UnityEngine;

namespace DTech.DataPersistence
{
	public sealed class SaveService : ISaveService
	{
		private readonly ISerializer _serializer;
		private readonly ICryptographer _cryptographer;
		private readonly IStorageProvider _storageProvider;

		public SaveService(ISerializer serializer, ICryptographer cryptographer, IStorageProvider storageProvider)
		{
			_serializer = serializer;
			_cryptographer = cryptographer;
			_storageProvider = storageProvider;
		}

		public bool HasSave(string key)
		{
			return _storageProvider.ContainsKey(key);
		}

		public async Task SaveAsync<T>(string key, T value, bool isCrypted = true)
		{
			SaveEntity saveEntity = GetSaveEntity(value, isCrypted);
			string serializedSaveEntity = _serializer.Serialize(saveEntity);
			await _storageProvider.WriteAsync(key, serializedSaveEntity);
		}

		public async Task<T> LoadAsync<T>(string key, T defaultValue, bool isCrypted = true)
		{
			T result = defaultValue;
			SaveEntity defaultSaveEntity = GetSaveEntity(defaultValue, isCrypted);
			string serializedDefaultEntity = _serializer.Serialize(defaultSaveEntity);
			StorageReadResponse response = await _storageProvider.ReadAsync(key, serializedDefaultEntity);
			if (!response.Success)
			{
				Debug.LogError($"[{nameof(SaveService)}] Failed to load value for key: {key} with error: {response.Error}");
			}
			else
			{
				SaveEntity entity = _serializer.Deserialize<SaveEntity>(response.Result);
				string serializedValue = entity.IsCrypted ? _cryptographer.Decrypt(entity.Value) : entity.Value;
				result = _serializer.Deserialize<T>(serializedValue);
			}

			return result;
		}

		public void Remove(string key)
		{
			_storageProvider.Remove(key);
		}

		private SaveEntity GetSaveEntity<T>(T value, bool isCrypted)
		{
			var saveEntity = new SaveEntity
			{
				Value = _serializer.Serialize(value),
				IsCrypted = isCrypted,
				LastWriteTime = DateTime.Now.Ticks,
			};
			
			if (isCrypted)
			{
				saveEntity.Value = _cryptographer.Encrypt(saveEntity.Value);
			}
			
			return saveEntity;
		}
	}
}