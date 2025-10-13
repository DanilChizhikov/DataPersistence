using System;
using System.Security.Cryptography;
using DTech.DataPersistence;
using DTech.DataPersistence.Cryptographers.Aes;
using NUnit.Framework;

namespace DataPersistence.Tests
{
	[TestFixture]
	internal sealed class AesCryptographerTests : CryptographerTests<AesCryptographer>
	{
		[Test]
		[TestCase("password1", "password2")]
		public void EncryptAndDecrypt_WithDifferentPasswords_ShouldThrowCryptographicException(string firtsPassword, string secondPassword)
		{
			string value = "test";
			var config1 = new AesConfig(firtsPassword);
			var config2 = new AesConfig(secondPassword);
			var cryptographer1 = new AesCryptographer(config1);
			var cryptographer2 = new AesCryptographer(config2);
			
			string encrypted = cryptographer1.Encrypt(value);
			string decrypted = string.Empty;

			Assert.Throws<CryptographicException>(() => decrypted = cryptographer2.Decrypt(encrypted));
		}
		
		protected override AesCryptographer GetCryptographer()
		{
			var config = new AesConfig(Guid.NewGuid().ToString());
			return new AesCryptographer(config);
		}
	}
}