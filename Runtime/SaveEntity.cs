using System;

namespace DTech.DataPersistence
{
	[Serializable]
	internal struct SaveEntity
	{
		public string Value;
		public bool IsCrypted;
		public long LastWriteTime;
	}
}