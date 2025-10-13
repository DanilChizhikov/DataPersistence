using System;
using UnityEngine;

namespace DTech.DataPersistence.StorageProviders.Binary
{
	[Serializable]
	public struct BinaryProviderOptions
	{
		private const string DefaultFileExtension = ".bin";
		
		[field: SerializeField] public string FileExtension { get; private set; }
		
		public BinaryProviderOptions(string fileExtension)
		{
			if (string.IsNullOrEmpty(fileExtension))
			{
				Debug.LogError($"[{nameof(BinaryProviderOptions)}] {nameof(fileExtension)} is null or empty!");
				fileExtension = DefaultFileExtension;
			}

			FileExtension = fileExtension;
		}
	}
}