using DTech.DataPersistence;
using NUnit.Framework;

namespace DataPersistence.Tests
{
	internal abstract class CryptographerTests<T> where T : ICryptographer
	{
		protected T Cryptographer { get; private set; }
		
		[SetUp]
		public void Setup()
		{
			Cryptographer = GetCryptographer();
		}
		
		[Test]
		public void EncryptAndDecrypt_ShouldReturnSameValue()
		{
			string value = "test";
			string encryptedValue = Cryptographer.Encrypt(value);
			string decryptedValue = Cryptographer.Decrypt(encryptedValue);
			Assert.AreEqual(value, decryptedValue);
		}
		
		protected abstract T GetCryptographer();
	}
}