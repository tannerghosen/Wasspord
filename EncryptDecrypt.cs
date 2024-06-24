using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Wasspord
{
    /*
     * Methods: Encrypt, Decrypt, Init, GetKey, SetKey, GenerateKey, ValidateKey
     * Properties/Misc: Key, Bytes
     */
    internal class EncryptDecrypt
    {
        /* Key and Bytes: These are used when we encrypt/decrypt passwords. Our key
           is an encryption key used in the encryption/decryption and our bytes is 
           our IV (initialization vector, or initial state). 
        */

        private static string Key = "p055w4rd";
        private static byte[] Bytes = { 0x31, 0xAB, 0xA7, 0x91, 0x93, 0x9B, 0x7D, 0x1F, 0x3B, 0xF7, 0x8D, 0x3F, 0x9A };

        // Reference on Encryption / Decryption being done here:
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rfc2898derivebytes?view=net-8.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.io.memorystream?view=net-8.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptostream?view=net-8.0

        /* MemoryStreams are simply containers of empty memory initialized with nothing in it, and is expandable.

           CryptoStreams are made by giving the stream, the mode it'll be using, and any overloads
           (such as CryptoStreamMode.Write).

           When CryptoStream is used inside a MemoryStream, the data inside the MemoryStream can be encrypted
           or decrypted, depending on the desired result.
         */

        /* Encrypt: Encrypts the account password before it's saved to the account dictionary using AES. Can be decrypted by Decrypt.
		 * Parameters: password 
		 * Returns: encrypted password */
        public static string Encrypt(string password)
        {
            // Get bytes from our string password
            byte[] b = Encoding.Unicode.GetBytes(password);

            // Create Aes object
            using (Aes aes = Aes.Create())
            {
                // Create a deriver to derive bytes from Key and Bytes using PBKDF2 to get our Key and IV for encryption
                Rfc2898DeriveBytes d = new Rfc2898DeriveBytes(Key, Bytes);
                aes.Key = d.GetBytes(32); // this is our key
                aes.IV = d.GetBytes(16); // this is our IV (initialization vector)
                // Create streams for encryption
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(b, 0, b.Length); // write bytes to cryptostream
                        cs.Close(); // close the cryptostream
                    }
                    // Our data in ms (our encrypted password) is encrypted as a Base64 String based on the content from above CryptoStream.
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }
        /* Decrypt: Decrypts the account password previously encrypted and saved to either an account dictionary or a .wasspord file.
		 * Parameters: password
		 * Returns: decrypted password */
        public static string Decrypt(string password)
        {
            // This is in case passwords had a space in them prior to decrypting (so if it was encrypted as "hello world"),
            // it would error out without this line below.
            password = password.Replace(" ", "+");

            // Get bytes from our base64 string encrypted password
            byte[] b = Convert.FromBase64String(password);

            // Create Aes object
            using (Aes aes = Aes.Create())
            {
                // Create a deriver to derive bytes from Key and Bytes using PBKDF2 to get our Key and IV for decryption
                Rfc2898DeriveBytes d = new Rfc2898DeriveBytes(Key, Bytes);
                aes.Key = d.GetBytes(32); // this is our key
                aes.IV = d.GetBytes(16); // this is our IV (initialization vector)

                try // Let's try our current method unless an error occurs
                {
                    // Create streams for decryption
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(b, 0, b.Length); // write bytes to cryptostream
                            cs.Close(); // close the cryptostream
                        }
                        // Our data in ms (our decrypted password) is converted to a regular Unicode String based on contents from above CryptoStream.
                        password = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                catch
                {
                    /* This comes from older code, pre-commit 14ff6c5
                       This is to keep compability with older files and not cause errors / invalid passwords being loaded.
                       Because we already get our bytes from our base64 string earlier in our code,
                       we don't need to redclare all of the code from our old code.
                       (namely, declaring a new bytes array and calling another Convert.FromBase64String())
                    */
                    Logger.Write("Caught old password encryption.", "WARNING");
                    string oldpassword; // string container for our decrypted password
                    oldpassword = Encoding.ASCII.GetString(b); // get string out of our bytes
                    return oldpassword;
                }
            }
            return password;
        }
        public static void GenerateKey()
        {
            var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[16];
            rng.GetBytes(bytes);
            Key = Convert.ToBase64String(bytes);
        }

        public static string GetKey()
        {
            return Key;
        }

        public static void SetKey(string key)
        {
            Key = key;
        }

        public static bool ValidateKey(string key)
        {
            try
            {
                Convert.FromBase64String(key);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
