namespace DTech.DataPersistence
{
	public interface ICryptographer
	{
		string Encrypt(string value);
		string Decrypt(string value);
	}
}