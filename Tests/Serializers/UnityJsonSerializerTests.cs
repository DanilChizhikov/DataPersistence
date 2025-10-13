using DTech.DataPersistence;
using NUnit.Framework;

namespace DataPersistence.Tests
{
	[TestFixture]
	internal sealed class UnityJsonSerializerTests : SerializerTests<UnityJsonSerializer>
	{
		protected override UnityJsonSerializer GetSerializer()
		{
			return new UnityJsonSerializer();
		}
	}
}