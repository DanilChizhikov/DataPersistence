using DTech.DataPersistence;
using DTech.DataPersistence.StorageProviders.Binary;
using NUnit.Framework;

namespace DataPersistence.Tests
{
	[TestFixture]
	internal sealed class BinaryStorageProviderTests : StorageProviderTests<BinaryStorageProvider>
	{
		protected override string TestSaveKey => "BinaryStorageProviderTests";
		
		protected override BinaryStorageProvider GetStorageProvider()
		{
			var options = new BinaryProviderOptions(".bin");
			return new BinaryStorageProvider(options);
		}
	}
}