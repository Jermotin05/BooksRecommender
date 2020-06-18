using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GoodBooksRecommender.Helpers
{
    public class Encryption
    {

        public class RandomStringGenerator
        {
            private readonly Random _random;
            private readonly string _chars;

            public RandomStringGenerator()
            {
                _random = new Random();
                _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            }

            public string Get(int size)
            {
                var buffer = new char[size];

                for (var i = 0; i < size; i++)
                {
                    buffer[i] = _chars[_random.Next(_chars.Length)];
                }
                return new string(buffer);
            }
        }

        #region "String Encryption"

        public static string Encrypt(string plainText)
        {

            string passPhrase = "G~od!b@k#s$o%r^E&n*c(r)y_p+t~i!o@n#";

            string saltValue = "SaltValue";

            string hashAlgorithm = "SHA1";

            int passwordIterations = 2;

            string initVector = "@1B2c3D4e5F6g7H8";

            int keySize = 256;

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();

            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
                return "";

            string passPhrase = "G~od!b@k#s$o%r^E&n*c(r)y_p+t~i!o@n#";

            string saltValue = "SaltValue";

            string hashAlgorithm = "SHA1";

            int passwordIterations = 2;

            string initVector = "@1B2c3D4e5F6g7H8";

            int keySize = 256;

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();

            cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }

        public static string HashPassword(string password)
        {
            return ComputeHash(password, new MD5CryptoServiceProvider());
        }

        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashedBytes);
        }

        #endregion
    }
}
