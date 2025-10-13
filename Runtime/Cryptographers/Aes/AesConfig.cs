using System;
using UnityEngine;

namespace DTech.DataPersistence.Cryptographers.Aes
{
	[Serializable]
	public struct AesConfig
	{
		[field: SerializeField] public string Password { get; private set; }

		public AesConfig(string password)
		{
			Password = password;
		}
	}
}