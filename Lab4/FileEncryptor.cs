using Lab4.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Lab4;

public class FileEncryptor : IFileEncryptor
{
    private const string Salt = "123123";

    private readonly ManualResetEventSlim _pauseEvent = new (true);
    private bool _isCancelled;

    public void Pause() => _pauseEvent.Reset();
    public void Resume() => _pauseEvent.Set();
    public void Cancel() => _isCancelled = true;

    public async Task Encrypt(string password,
                              FileStream inputStream,
                              FileStream outputStream,
                              Action<int> updateProgressAction,
                              CancellationToken cancellationToken = default)
    {
        var key = GetKey(password);
        using Aes aes = GetAes(key);

        await ProcessCryptoTransform(aes.CreateEncryptor(),
                                     inputStream,
                                     outputStream,
                                     updateProgressAction,
                                     cancellationToken);
    }

    public async Task Decrypt(string password,
                              FileStream inputStream,
                              FileStream outputStream,
                              Action<int> updateProgressAction,
                              CancellationToken cancellationToken = default)
    {
        var key = GetKey(password);
        using Aes aes = GetAes(key);

        await ProcessCryptoTransform(aes.CreateDecryptor(),
                                     inputStream,
                                     outputStream,
                                     updateProgressAction,
                                     cancellationToken);
    }

    private async Task ProcessCryptoTransform(ICryptoTransform transform,
                                                     FileStream inputStream,
                                                     FileStream outputStream,
                                                     Action<int> updateProgressAction,
                                                     CancellationToken cancellationToken = default)
    {
        using var cs = new CryptoStream(outputStream, transform, CryptoStreamMode.Write);

        byte[] buffer = new byte[4096];
        long totalBytes = inputStream.Length;
        long totalRead = 0;
        int lastReportedProgress = -1;

        int bytesRead;
        while ((bytesRead = await inputStream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            if (_isCancelled)
                throw new OperationCanceledException("Encryption was canceled.");

            _pauseEvent.Wait();

            await cs.WriteAsync(buffer, cancellationToken);
            totalRead += bytesRead;

            int progress = (int)(100.0 * totalRead / totalBytes);
            if (progress != lastReportedProgress)
            {
                updateProgressAction(progress);
                lastReportedProgress = progress;
            }
        }
    }

    private static Rfc2898DeriveBytes GetKey(string password)
    {
        var encodedPassword = Encoding.UTF8.GetBytes(password);
        var encodedSalt = Encoding.UTF8.GetBytes(Salt);

        return new (encodedPassword,
                    encodedSalt,
                    100_000,
                    HashAlgorithmName.SHA256);
    }

    private static Aes GetAes(Rfc2898DeriveBytes key)
    {
        var aes = Aes.Create();
        aes.Key = key.GetBytes(32);
        aes.IV = key.GetBytes(16);

        return aes;
    }
}
