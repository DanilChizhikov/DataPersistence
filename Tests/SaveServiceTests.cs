using System;
using System.Collections;
using System.Runtime.CompilerServices;
using DTech.DataPersistence;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DataPersistence.Tests
{
	[TestFixture]
	internal sealed class SaveServiceTests
	{
		private const string TestSaveKey = "DataPersistence.Tests.SaveServiceTests";
		
		private ISaveService _saveService;

		[SetUp]
		public void Setup()
		{
			var serialized = new UnityJsonSerializer();
			var storageProvider = new BinaryStorageProvider(new BinaryProviderOptions(".bin"));
			var cryptographer = new AesCryptographer(new AesConfig(Guid.NewGuid().ToString()));
			_saveService = new SaveService(serialized, cryptographer, storageProvider);
		}
		
		[Test]
		public void HasKey_WhenNoDataExists_ShouldReturnFalse()
		{
			Assert.False(_saveService.HasSave(TestSaveKey));
		}
		
		[UnityTest]
		public IEnumerable SaveAsync_WhenCalled_ShouldSaveDataSuccessfully()
		{
			var defaultSaveData = new TestSaveData
			{
				Guid = Guid.NewGuid().ToString(),
			};

			TaskAwaiter<TestSaveData> awaiter = _saveService.LoadAsync(TestSaveKey, defaultSaveData).GetAwaiter();

			yield return new WaitUntil(() => awaiter.IsCompleted);
			
			Assert.True(awaiter.IsCompleted);
			Assert.True(_saveService.HasSave(TestSaveKey));
		}

		[UnityTest]
		public IEnumerable LoadAsync_WhenDataExists_ShouldReturnSavedData()
		{
			var data = new TestSaveData
			{
				Guid = Guid.NewGuid().ToString()
			};

			TaskAwaiter awaiter = _saveService.SaveAsync(TestSaveKey, data).GetAwaiter();

			yield return new WaitUntil(() => awaiter.IsCompleted);
			
			Assert.True(awaiter.IsCompleted);

			var defaultSaveData = new TestSaveData
			{
				Guid = Guid.NewGuid().ToString(),
			};

			TaskAwaiter<TestSaveData> loadAwaiter = _saveService.LoadAsync(TestSaveKey, defaultSaveData).GetAwaiter();
			
			yield return new WaitUntil(() => loadAwaiter.IsCompleted);
			
			Assert.True(loadAwaiter.IsCompleted);
			Assert.AreEqual(data.Guid, loadAwaiter.GetResult().Guid);
		}
		
		[UnityTest]
		public IEnumerable LoadAsync_WhenNoDataExists_ShouldReturnDefaultValue()
		{
			var defaultSaveData = new TestSaveData
			{
				Guid = Guid.NewGuid().ToString(),
			};

			TaskAwaiter<TestSaveData> awaiter = _saveService.LoadAsync(TestSaveKey, defaultSaveData).GetAwaiter();

			yield return new WaitUntil(() => awaiter.IsCompleted);
			
			Assert.True(awaiter.IsCompleted);
			
			Assert.AreEqual(defaultSaveData.Guid, awaiter.GetResult().Guid);
		}
		
		[UnityTest]
		public IEnumerable RemoveData_WhenDataExists_ShouldRemoveDataSuccessfully()
		{
			var defaultSaveData = new TestSaveData
			{
				Guid = Guid.NewGuid().ToString(),
			};

			bool hasKey = _saveService.HasSave(TestSaveKey);

			TaskAwaiter<TestSaveData> awaiter = _saveService.LoadAsync(TestSaveKey, defaultSaveData).GetAwaiter();

			yield return new WaitUntil(() => awaiter.IsCompleted);
			
			Assert.True(awaiter.IsCompleted);
			Assert.False(hasKey);
			Assert.False(_saveService.HasSave(TestSaveKey));
		}

		[TearDown]
		public void TearDown()
		{
			_saveService.Remove(TestSaveKey);
		}
	}
}