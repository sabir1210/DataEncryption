// Program.cs
using System;
using System.Security.Cryptography;
using EncryptionDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main1()
    {
        // Generate random key and IV
        byte[] key = GenerateRandomBytes(32); // 32 bytes for AES-256
        byte[] iv = GenerateRandomBytes(16);  // 16 bytes for AES

        // Convert key and IV to Base64 strings
        string base64Key = Convert.ToBase64String(key);
        string base64IV = Convert.ToBase64String(iv);

        // Print Base64-encoded key and IV
        Console.WriteLine("Base64-encoded key: " + base64Key);
        Console.WriteLine("Base64-encoded IV: " + base64IV);
    }

    static byte[] GenerateRandomBytes(int length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return bytes;
        }
    }
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var service = host.Services.GetRequiredService<EncryptionService>();

        const string data = "Sensitive data to encrypt asdfasdfasdf adsfhfoqi qew dsaf eea df efas dfaef adsf adsfieo2io3iiiiiiiasdfadsff asdlfja ea fadioiow ew";
        Console.WriteLine($"Original data: {data}");

        service.SaveData(data);

        var encryptedData = "+dddGy+tsKwEGfjyYgyAj1r2JOv5Y6ia5n3juEbhYdgxg/nA5VdZ5hoMnS2Tq6Qg3jul9noU8k0D5zwT5Xj7Do6RrNHgWasyYNgo/SqDpZw1bCp33gvG8igoq5EaEMVLEDW5a74OJEjrwUN9+U32Or4mRTueLRvOye2/s2MgWbLD/XH2OyemHYYEdrnewfF4"; // i copied the encrypted data to get back previous data
        service.GetData(encryptedData);
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<EncryptionSettings>(context.Configuration.GetSection("EncryptionSettings"));
                services.AddSingleton<EncryptionHelper>();
                services.AddTransient<EncryptionService>();
            });
}
