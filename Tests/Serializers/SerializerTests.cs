using System;
using DTech.DataPersistence;
using NUnit.Framework;

namespace DataPersistence.Tests
{
	internal abstract class SerializerTests<T> where T : ISerializer
	{
		protected ISerializer Serializer { get; private set; }
		
		[SetUp]
		public void Setup()
		{
			Serializer = GetSerializer();
		}

		[Test]
		public void SerializeAndDeserialize_ShouldReturnSameValue()
		{
			var testValue = new TestSaveData
			{
				Guid = Guid.NewGuid().ToString()
			};
			
			string serializedValue = Serializer.Serialize(testValue);
			TestSaveData deserializedValue = default;
			
			Assert.DoesNotThrow(() => deserializedValue = Serializer.Deserialize<TestSaveData>(serializedValue));
			Assert.AreEqual(testValue.Guid, deserializedValue.Guid);
		}
		
		protected abstract T GetSerializer();
	}
}