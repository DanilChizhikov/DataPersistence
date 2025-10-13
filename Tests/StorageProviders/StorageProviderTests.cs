using System;
using System.Collections;
using System.Runtime.CompilerServices;
using DTech.DataPersistence;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DataPersistence.Tests
{
	internal abstract class StorageProviderTests<T> where T : IStorageProvider
	{
		protected T StorageProvider { get; private set; }
		protected abstract string TestSaveKey { get; }

		[SetUp]
		public virtual void Setup()
		{
			StorageProvider = GetStorageProvider();
		}
		
		[Test]
		public void HasKey_WhenNoDataExists_ShouldReturnFalse()
		{
			Assert.False(StorageProvider.ContainsKey(TestSaveKey));
		}
		
		[UnityTest]
		public IEnumerable WriteAsync_WhenCalled_ShouldWriteDataSuccessfully()
		{
			string defaultValue = Guid.NewGuid().ToString();
			TaskAwaiter<bool> awaiter = StorageProvider.WriteAsync(TestSaveKey, defaultValue).GetAwaiter();

			yield return new WaitUntil(() => awaiter.IsCompleted);
			
			Assert.True(awaiter.GetResult());
			Assert.IsTrue(StorageProvider.ContainsKey(TestSaveKey));
		}
		
		[UnityTest]
		public IEnumerable ReadAsync_WhenCalled_ShouldReadDataSuccessfully()
		{
			string defaultValue = Guid.NewGuid().ToString();
			
			TaskAwaiter<WriterReadResponse> awaiter = StorageProvider.ReadAsync(TestSaveKey, defaultValue).GetAwaiter();

			yield return new WaitUntil(() => awaiter.IsCompleted);
			
			Assert.True(awaiter.GetResult().Success);
			Assert.AreEqual(defaultValue, awaiter.GetResult().Result);
		}
		
		[UnityTest]
		public IEnumerable RemoveData_WhenDataExists_ShouldRemoveDataSuccessfully()
		{
			string defaultValue = Guid.NewGuid().ToString();

			bool hasKey = StorageProvider.ContainsKey(TestSaveKey);

			TaskAwaiter<bool> awaiter = StorageProvider.WriteAsync(TestSaveKey, defaultValue).GetAwaiter();

			yield return new WaitUntil(() => awaiter.IsCompleted);
			
			Assert.True(awaiter.IsCompleted);
			Assert.False(hasKey);
			Assert.False(StorageProvider.ContainsKey(TestSaveKey));
		}
		
		[TearDown]
		public virtual void TearDown()
		{
			StorageProvider.Remove(TestSaveKey);
		}
		
		protected abstract T GetStorageProvider();
	}
}