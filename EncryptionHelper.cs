using EncryptionDemo;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
// EncryptionHelper.cs
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EncryptionDemo
{
    // EncryptionHelper.cs
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.Extensions.Configuration;

    // EncryptionHelper.cs
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using Microsoft.Extensions.Configuration;
    using System.Buffers.Text;

    public class EncryptionHelper
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionHelper(IConfiguration configuration)
        {
            var encryptionSettings = configuration.GetSection("EncryptionSettings").Get<EncryptionSettings>();
            _key = Convert.FromBase64String(encryptionSettings.Key);
            _iv = Convert.FromBase64String(encryptionSettings.IV);

            // Validate key and IV lengths
            if (_key.Length != 32)  // Ensure key is 32 bytes (256 bits) for AES-256
                throw new ArgumentException("Key length must be 32 bytes for AES-256.");
            if (_iv.Length != 16)   // Ensure IV is 16 bytes (128 bits) for AES
                throw new ArgumentException("IV length must be 16 bytes for AES.");
        }

        public string Encrypt(string plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText));

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            if (cipherText == null)
                throw new ArgumentNullException(nameof(cipherText));

            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(buffer))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }




    //public class EncryptionHelper
    //{
    //    private readonly byte[] _key;
    //    private readonly byte[] _iv;

    //    public EncryptionHelper(IConfiguration configuration)
    //    {
    //        var encryptionSettings = configuration.GetSection("EncryptionSettings").Get<EncryptionSettings>();
    //        _key = Encoding.UTF8.GetBytes(encryptionSettings.Key);
    //        _iv = Encoding.UTF8.GetBytes(encryptionSettings.IV);


    //        //var encryptionSettings = configuration.GetSection("EncryptionSettings").Get<EncryptionSettings>();
    //        //_key = Encoding.UTF8.GetBytes(encryptionSettings.Key);
    //        //_iv = Encoding.UTF8.GetBytes(encryptionSettings.IV);


    //        //var encryptionSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("EncryptionSettings");
    //        //string Key = encryptionSettings["Key"];
    //        //string IV = encryptionSettings["IV"];

    //        //_key = Encoding.UTF8.GetBytes(Key);
    //        //_iv = Encoding.UTF8.GetBytes(IV);
    //    }

    //    public string Encrypt(string plainText)
    //    {
    //        if (plainText == null)
    //            return null;

    //        using (Aes aesAlg = Aes.Create())
    //        {
    //            aesAlg.Key = _key;
    //            aesAlg.IV = _iv;

    //            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

    //            using (var msEncrypt = new MemoryStream())
    //            {
    //                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //                {
    //                    using (var swEncrypt = new StreamWriter(csEncrypt))
    //                    {
    //                        swEncrypt.Write(plainText);
    //                    }
    //                    return Convert.ToBase64String(msEncrypt.ToArray());
    //                }
    //            }
    //        }
    //    }

    //    public string Decrypt(string cipherText)
    //    {
    //        if (cipherText == null)
    //            return null;


    //        using (Aes aesAlg = Aes.Create())
    //        {
    //            aesAlg.Key = _key;
    //            aesAlg.IV = _iv;

    //            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

    //            byte[] buffer = Convert.FromBase64String(cipherText);
    //            using (var msDecrypt = new MemoryStream(buffer))
    //            {
    //                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //                {
    //                    using (var srDecrypt = new StreamReader(csDecrypt))
    //                    {
    //                        return srDecrypt.ReadToEnd();
    //                    }
    //                }
    //            }
    //        }
    //    }
}

