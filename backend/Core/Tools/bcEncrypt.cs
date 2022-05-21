using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace bitcomTickets.Core.Tools
{
    internal static class bcEncrypt
    {
        private static string passParse = "GdziePanieniskimRumiencemDziecielinaPala";

        internal static bool ComparePlainWithDecrypted(string plainString, string encpryptedString)
        {
            return (plainString == DecryptStringToString(encpryptedString));
        }

        internal static string DecryptStringToString(string encpryptedString)
        {
            string decryptedString = string.Empty;
            byte[] encrypted = System.Convert.FromBase64String(encpryptedString);
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                byte[] key;
                byte[] iv;
                GetByteArrays(out key, out iv);
                myRijndael.Key = key;
                myRijndael.IV = iv;
                decryptedString = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);
            }

            return decryptedString;
        }

        public static string EncryptStringToString(string plainString)
        {
            string encryptedString = string.Empty;
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                byte[] key;
                byte[] iv;
                GetByteArrays(out key, out iv);
                myRijndael.Key = key;
                myRijndael.IV = iv;
                byte[] encrypted = EncryptStringToBytes(plainString, myRijndael.Key, myRijndael.IV);
                encryptedString = System.Convert.ToBase64String(encrypted);
            }
            return encryptedString;
        }

        private static void GetByteArrays(out byte[] key, out byte[] iv)
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(passParse, new byte[] { 0x48, 0x76, 0x61, 0x6e, 0x20, 0x4f, 0x65, 0xcc, 0x76, 0x65, 0x64, 0x65, 0x76 });
            key = pdb.GetBytes(32);
            iv = pdb.GetBytes(16);

        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;

        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
