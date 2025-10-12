using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DTech.DataPersistence
{
	public sealed class AesCryptographer : ICryptographer
	{
		private readonly AesConfig _config;

		public AesCryptographer(AesConfig config)
		{
			_config = config;
		}
		
		public string Encrypt(string value)
		{
			byte[] encryptedBuffer = null;
			using (var aesCrypt = Aes.Create())
			{
				PrepareAes(aesCrypt);
				ICryptoTransform encryptor = aesCrypt.CreateEncryptor(aesCrypt.Key, aesCrypt.IV);
				using (var memoryStream = new MemoryStream())
				{
					using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
					{
						using (var writer = new StreamWriter(cryptoStream))
						{
							writer.Write(value);
						}
					}

					encryptedBuffer = memoryStream.ToArray();
				}
			}

			return BitConverter.ToString(encryptedBuffer).Replace("-", string.Empty);
		}

		public string Decrypt(string value)
		{
			byte[] encryptedBuffer = StringToByte(value);
			string result = string.Empty;
			using (var aesCrypt = Aes.Create())
			{
				PrepareAes(aesCrypt);
				ICryptoTransform decryptor = aesCrypt.CreateDecryptor(aesCrypt.Key, aesCrypt.IV);
				using (var memoryStream = new MemoryStream(encryptedBuffer))
				{
					using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (var reader = new StreamReader(cryptoStream))
						{
							result = reader.ReadToEnd();
						}
					}
				}
			}

			return result;
		}
		
		private void PrepareAes(Aes aesCrypt)
		{
			byte[] passwordBuffer = Encoding.ASCII.GetBytes(_config.Password);
			byte[] ivBuffer = GetIv(passwordBuffer);
			aesCrypt.BlockSize = 128;
			aesCrypt.Mode = CipherMode.CBC;
			aesCrypt.Padding = PaddingMode.PKCS7;
			aesCrypt.Key = passwordBuffer;
			aesCrypt.IV = ivBuffer;
		}

		private byte[] GetIv(byte[] passwordBuffer)
		{
			byte[] iv = new byte[passwordBuffer.Length / 2];
			Buffer.BlockCopy(passwordBuffer, 0, iv, 0, iv.Length);
			return iv;
		}

		private byte[] StringToByte(string value)
		{
			int charCount = value.Length;
			byte[] buffer = new byte[charCount / 2];
			for (int i = 0; i < charCount; i += 2)
			{
				buffer[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
			}

			return buffer;
		}
	}
}