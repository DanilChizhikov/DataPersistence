namespace DTech.DataPersistence
{
	public readonly struct WriterReadResponse
	{
		public bool Success { get; }
		public string Result { get; }
		public string Error { get; }

		public WriterReadResponse(bool success, string result, string error = "")
		{
			Success = success;
			Result = result;
			Error = error;
		}
	}
}