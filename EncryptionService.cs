using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDemo
{
    public class EncryptionService
    {
        private readonly EncryptionHelper _encryptionHelper;

        public EncryptionService(EncryptionHelper encryptionHelper)
        {
            _encryptionHelper = encryptionHelper;
        }

        public void SaveData(string sensitiveData)
        {
            var encryptedData = _encryptionHelper.Encrypt(sensitiveData);
            // Here we just print it to simulate saving to the database
            Console.WriteLine($"Encrypted data: {encryptedData}");
        }

        public void GetData(string encryptedData)
        {
            var decryptedData = _encryptionHelper.Decrypt(encryptedData);
            // Here we just print it to simulate retrieving from the database
            Console.WriteLine($"Decrypted data: {decryptedData}");
        }
    }

}
