using DTech.DataPersistence;
using NUnit.Framework;

namespace DataPersistence.Tests
{
	[TestFixture]
	internal sealed class PreferenceStorageProviderTests : StorageProviderTests<PreferenceStorageProvider>
	{
		protected override string TestSaveKey => "DataPersistence.Tests.PreferenceStorageProviderTests";

		protected override PreferenceStorageProvider GetStorageProvider()
		{
			return new PreferenceStorageProvider();
		}
	}
}