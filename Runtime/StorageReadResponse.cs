namespace DTech.DataPersistence
{
	public readonly struct StorageReadResponse
	{
		public bool Success { get; }
		public string Result { get; }
		public string Error { get; }

		public StorageReadResponse(bool success, string result, string error = "")
		{
			Success = success;
			Result = result;
			Error = error;
		}
	}
}