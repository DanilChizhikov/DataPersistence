using DTech.DataPersistence;
using DTech.DataPersistence.Serializers;
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