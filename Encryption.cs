using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Wasspord
{
    /*
     * Methods: Encrypt, Decrypt, Init, GetKey, SetKey, GenerateKey, Validate
     * Properties/Misc: Key, Salt
     */
    /// <summary>
    /// This class handles all encryption and decryption related tasks of the program, including generating a Key to be used for a Key and IV in both tasks as well as validating Base64 strings.
    /// </summary>
    public static class Encryption // mutable class
    {
        /* Why do I use a Key and Salt to get an encryption key instead of password and Salt?
         * Mostly this comes down to me not being extremely knowledgeable in Encryption / Decryption, 
         * but part of the why has to do with the fact I don't want each password to have its own key for 
         * decryption, as it would complex the program more than it needs to be for a minimal security trade-off.
         * Considering the application is offline and does not store this info online where this is a critical point 
         * of attack that could be exploited, I believe this is acceptable as of this time. 
         */
        /// <summary>
        /// Our key which is initialized as "p0ssw4rd" for backwards compatability.
        /// Used in Rfc2898DeriveBytes in tandem with Bytes to get our encryption Key and IV.
        /// </summary>
        private static string Key = "p055w4rd";
        /// <summary>
        /// Our salt for key derivation (these are not changed at all by the program).
        /// Used in Rfc2898DeriveBytes in tandem with Key to get our encryption Key and IV.
        /// </summary>
        private static readonly byte[] Salt = { 0x31, 0xAB, 0xA7, 0x91, 0x93, 0x9B, 0x7D, 0x1F, 0x3B, 0xF7, 0x8D, 0x3F, 0x9A };

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

        /// <summary>
        /// Encrypts the account password before it's saved to the account dictionary using AES. Can be decrypted by Decrypt.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>encrypted password </returns>
        public static string Encrypt(string password) // O(n)
        {
            // Get bytes from our string password
            byte[] b = Encoding.Unicode.GetBytes(password);

            // Create Aes object
            using (Aes aes = Aes.Create())
            {
                // Create our encryption key and IV using PBKDF2 with Key and Salt
                Rfc2898DeriveBytes d = new Rfc2898DeriveBytes(Key, Salt);
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
                    // Convert the encrypted bytes in memorystream to an Base64 string.
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }
        /// <summary>
        /// Decrypts the account password previously encrypted and saved to either an account dictionary or a .wasspord file.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>decrypted password</returns>
        public static string Decrypt(string password) // O(n)
        {
            // This is in case passwords had a space in them prior to decrypting (so if it was encrypted as "hello world"),
            // it would error out without this line below.
            password = password.Replace(" ", "+");

            /* We called Validate to ensure it's a valid Base64 string. If it is, we're good.
               If not, return "error".
               This may cause program problems, but the chances this happens in normal use is slim, as I can only see it happening 
               if I am working with the program adding new features that interact with Decrypt (in which case I have already seen a few fatal 
               errors from this method without the try-catch), or if somebody alters their .wasspord file's key / password / account password.
             */
            bool validate = Validate(password);
            if (validate == false)
            {
                //Logger.Write("Validate has determined most likely the password given isn't a Base64 string! (password: " + password + ")", "ERROR");
                return "error";
            }

            // Get bytes from our base64 string encrypted password
            byte[] b = Convert.FromBase64String(password);

            // Create Aes object
            using (Aes aes = Aes.Create())
            {
                // Create our encryption key and IV using PBKDF2 with Key and Salt
                Rfc2898DeriveBytes d = new Rfc2898DeriveBytes(Key, Salt);
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
                        // Convert the decrypted bytes in memorystream to an Unicode string.
                        password = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                catch
                {
                    //Logger.Write("Decrypt failed beyond validation. This shouldn't happen unless the password is severely messed up or manually altered.", "ERROR");
                    return "error";
                }
            }
            return password;
        }
        /// <summary>
        /// Generates a new Key for our Encryption / Decryption
        /// </summary>
        public static void GenerateKey() // O(1)
        {
            var rng = new RNGCryptoServiceProvider(); // create secure number generator
            var bytes = new byte[16]; // create bytes array
            rng.GetBytes(bytes); // fill bytes array with secure random bytes
            Key = Convert.ToBase64String(bytes); // convert bytes array to a Base64 string, sets Key to it
        }

        /// <summary>
        /// Gets key used in encryption
        /// </summary>
        /// <returns>Key</returns>
        public static string GetKey()
        {
            return Key;
        }

        /// <summary>
        /// Sets key for encryption
        /// </summary>
        /// <param name="key"></param>
        public static void SetKey(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Validates the authenticity of a string by trying to convert it from a Base64 String.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Boolean value depending on the result of whether or not it's a valid Base64 string</returns>
        public static bool Validate(string s)
        {
            /* Validate uses a try-catch to see if the string causes an exception, if it doesn't, return true, if the catch occurs,
               it's a bad string, therefore return false.
               This does not handle manually altered strings in the .wasspord file itself unless it's turned into an invalid Base64 string.
               I unfortunately cannot really handle that and that would be the user's choice to risk corrupting or ruining their .wasspord files.
            */
            try 
            {
                // Convert.FromBase64String() will cause an exception if the string is not a valid base 64 string.
                Convert.FromBase64String(s);
                return true;
            }
            catch
            {
                Logger.Write("Validate has detected an invalid Base64 string. (String: " + s + ")", "ERROR");
                return false;
            }
        }
    }
}
