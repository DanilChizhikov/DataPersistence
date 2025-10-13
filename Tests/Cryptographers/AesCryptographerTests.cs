using System;
using DTech.DataPersistence;
using NUnit.Framework;

namespace DataPersistence.Tests
{
	[TestFixture]
	internal sealed class AesCryptographerTests : CryptographerTests<AesCryptographer>
	{
		protected override AesCryptographer GetCryptographer()
		{
			var config = new AesConfig(Guid.NewGuid().ToString());
			return new AesCryptographer(config);
		}
	}
}