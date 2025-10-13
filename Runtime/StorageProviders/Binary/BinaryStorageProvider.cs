using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace DTech.DataPersistence
{
	public sealed class BinaryStorageProvider : IStorageProvider
	{
		private readonly BinaryProviderOptions _options;

		public BinaryStorageProvider(BinaryProviderOptions options)
		{
			_options = options;
		}
		
		public bool ContainsKey(string key)
		{
			string filePath = GetFilePath(key);
			return File.Exists(filePath);
		}

		public async Task<bool> WriteAsync(string key, string value)
		{
			string filePath = GetFilePath(key);
			bool result = false;
			
			try
			{
				await using var writer = new StreamWriter(filePath);
				await writer.WriteAsync(value);
				result = true;
			}
			catch (Exception exception)
			{
				Debug.LogError($"[{nameof(BinaryStorageProvider)}] Failed to write to file: {filePath} with exception: {exception}");
			}

			return result;
		}

		public async Task<WriterReadResponse> ReadAsync(string key, string defaultValue)
		{
			string filePath = GetFilePath(key);
			if (!File.Exists(filePath))
			{
				await WriteAsync(key, defaultValue);
				return new WriterReadResponse(true, defaultValue);
			}
			
			try
			{
				using var reader = new StreamReader(filePath);
				string result = await reader.ReadToEndAsync();
				return new WriterReadResponse(true, result);
			}
			catch (Exception exception)
			{
				Debug.LogError($"[{nameof(BinaryStorageProvider)}] Failed to read from file: {filePath} with exception: {exception}");
				return new WriterReadResponse(false, defaultValue, $"Failed to read from file: {filePath} with exception: {exception}");
			}
		}

		public void Remove(string key)
		{
			if (!ContainsKey(key))
			{
				return;
			}

			string filePath = GetFilePath(key);
			File.Delete(filePath);
		}

		private string GetFilePath(string key) => Path.Combine(Application.persistentDataPath, $"{key}.{_options.FileExtension.Replace(".", "")}");
	}
}