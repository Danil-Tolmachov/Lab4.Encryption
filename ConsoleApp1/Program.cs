using Lab4;
using System.Diagnostics;

string input = @"C:\Users\pacma\OneDrive\Рабочий стол\lab4\secret.txt";
string output = @"C:\Users\pacma\OneDrive\Рабочий стол\lab4\secret_out.txt";


var encryptor = new FileEncryptor();

Console.WriteLine("Input password: ");

var password = Console.ReadLine()
    ?? throw new ArgumentException("Invalid input.");


var thread = new Thread(async () =>
{
    var sw = new Stopwatch();

    using var inputFile = new FileStream(input, FileMode.Open);
    using var outputFile = new FileStream(output, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    await outputFile.FlushAsync();

    sw.Start();
    try
    {
        await encryptor.Encrypt(password, inputFile, outputFile, p => Console.WriteLine($"Progress: {p}%"));
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Encryption was cancelled.");
    }
    sw.Stop();

    var fileSize = new FileInfo(output).Length;

    Console.WriteLine("Done.");
    Console.WriteLine($"Time: {sw.Elapsed}");
    Console.WriteLine($"Size: {fileSize} bytes");
});

thread.Start();

while (true)
{
    var key = Console.ReadKey(true).Key;
    switch (key)
    {
        case ConsoleKey.P:
            Console.WriteLine("Paused.");
            encryptor.Pause();
            break;
        case ConsoleKey.R:
            Console.WriteLine("Resumed.");
            encryptor.Resume();
            break;
        case ConsoleKey.C:
            Console.WriteLine("Cancelling...");
            encryptor.Cancel();
            thread.Join();
            Environment.Exit(0);
            break;
    }
}

