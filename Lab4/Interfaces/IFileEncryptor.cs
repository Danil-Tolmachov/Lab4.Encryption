namespace Lab4.Interfaces;

public interface IFileEncryptor
{
    public Task Encrypt(string password,
                        FileStream inputStream,
                        FileStream outputStream,
                        Action<int> updateProgressAction,
                        CancellationToken cancellationToken = default);

    public Task Decrypt(string password,
                        FileStream inputStream,
                        FileStream outputStream,
                        Action<int> updateProgressAction,
                        CancellationToken cancellationToken = default);
}
