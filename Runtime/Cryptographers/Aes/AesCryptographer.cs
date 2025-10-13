using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DTech.DataPersistence.Cryptographers.Aes
{
	public sealed class AesCryptographer : ICryptographer
	{
		private const string Salt = "DTechSalt1234";
		
		private static readonly byte[] _salt = Encoding.UTF8.GetBytes(Salt);
		
		private readonly AesConfig _config;

		public AesCryptographer(AesConfig config)
		{
			_config = config;
		}
		
		public string Encrypt(string value)
		{
			using var aes = System.Security.Cryptography.Aes.Create();
			using var keyDeriver = new Rfc2898DeriveBytes(_config.Password, _salt, 10000);

			aes.Key = keyDeriver.GetBytes(32);
			aes.GenerateIV();

			using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
			using var ms = new MemoryStream();
			
			ms.Write(aes.IV, 0, aes.IV.Length);

			using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
			{
				using (var sw = new StreamWriter(cs))
				{
					sw.Write(value);
				}
			}

			return Convert.ToBase64String(ms.ToArray());
		}

		public string Decrypt(string value)
		{
			byte[] fullBuffer = Convert.FromBase64String(value);

			using var aes = System.Security.Cryptography.Aes.Create();
			using var keyDeriver = new Rfc2898DeriveBytes(_config.Password, _salt, 10000);

			aes.Key = keyDeriver.GetBytes(32);
			
			byte[] iv = new byte[16];
			Array.Copy(fullBuffer, 0, iv, 0, iv.Length);
			aes.IV = iv;
			
			byte[] cipher = new byte[fullBuffer.Length - iv.Length];
			Array.Copy(fullBuffer, iv.Length, cipher, 0, cipher.Length);

			using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
			using var ms = new MemoryStream(cipher);
			using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
			using var sr = new StreamReader(cs);

			return sr.ReadToEnd();
		}
	}
}