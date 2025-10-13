using UnityEngine;

namespace DTech.DataPersistence.Serializers
{
	public sealed class UnityJsonSerializer : ISerializer
	{
		public string Serialize(object value)
		{
			return JsonUtility.ToJson(value);
		}

		public T Deserialize<T>(string args)
		{
			return JsonUtility.FromJson<T>(args);
		}
	}
}