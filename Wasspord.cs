using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.IO;
using System.Collections;
using System.IO.Pipes;
using System.Security.Cryptography;


namespace Wasspord
{
    /*
     * Methods: AddAccount, UpdatePassword, DeleteAccount, Save, Load, Encrypt, Decrypt
     * Important Variables/Misc: Account Dictionary, Account Struct
    */

    public static class Wasspord
    {
        /* Account Dictionary: 2 key (where, username) dictionary that contains information on 
           where the account is used, the username and the password. The struct below this is a part
           of this and other functions that look for/add accounts to the dictionary, as it's the only way
           to have a key that's 2 things (where, username) combined, which in turn means any looking/adding 
           requires both to be defined. */
        private static Dictionary<Account, string> Accounts = new Dictionary<Account, string>();
        private struct Account
        {
            public string where;
            public string username;
        }
        private static string Key = "p055w4rd";
        private static byte[] Bytes = { 0x31, 0xab, 0xa7, 0x91, 0x93, 0x9b, 0x7d, 0x1f, 0x3b, 0xf7, 0x8d, 0x3f, 0x9a };
        /* AddAccount: Adds an account with a location *where* it's used, the username and the encrypted password, to above mentioned
		   account dictionary, which can be saved to a .wasspord file down the line.
         * Parameters: where (location), username, password. */
        public static void AddAccount(string where, string username, string password)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (Accounts.ContainsKey(acc))
            {
                Console.Error.WriteLine("Duplicate Account");
            }
            else
            {
                password = Encrypt(password);
                Accounts.Add(acc, password);
            }
        }
		/* UpdatePassword: Updates a password for a defined location and username in the account dictionary.
		 * Parameters: where (location), username, password. */
		public static void UpdatePassword(string where, string username, string password)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (!Accounts.ContainsKey(acc))
            {
                Console.Error.WriteLine("Account doesn't exist / is invalid");
            }
            else
            {
                password = Encrypt(password);
                Accounts[acc] = password;
            }
        }
	   /* DeleteAccount: Deletes an account specified for a location and username from the account dictionary.
        * Parameters: where (location), username. */
        public static void DeleteAccount(string where, string username)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (!Accounts.ContainsKey(acc))
            {
                Console.Error.WriteLine("Account doesn't exist / is invalid");
            }
            else
            {
                Accounts.Remove(acc);
            }
        }
        /* Save: Saves account information to a .wasspord file for future use by the end user by writing data 
           down from the Account dictionary.
         * Parameters: location (filepath to file), filename (name of the file). */
        public static void Save(string location, string filename)
        {
            string file = location + @"\" + filename;
            try
            {
				// This should only ever happen if the file being saved is deleted (via the Save option, Save As should be fine.)
				if (!File.Exists(file))
                {
                    File.Create(file).Dispose();
                }
                using (StreamWriter sw = new StreamWriter(file))
                {
                    foreach (var pair in Accounts)
                    {
                        sw.WriteLine(pair.Key.where + "|" + pair.Key.username + "|" + pair.Value);
                    }
                }
            }
            catch
            {
                Console.Error.WriteLine("Error saving to file");
            }
        }
		/* Load: Loads previously saved account information from a .wasspord file to the account dictionary for current use. 
         * Parameters: location (filepath to file), filename (name of the file). */
		public static void Load(string location, string filename)
        {
            Accounts.Clear();
			string file = location + @"\" + filename;
			// This should never happen, but if it does create a blank file.
			if (!File.Exists(file))
            {
                File.Create(file).Dispose();
            }
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var spl = line.Split('|');
                    // Below is in case we have duplicate keys, which can happen if you try to load the same file again.
                    if (!Accounts.ContainsKey(new Account { where = spl[0], username = spl[1] }))
                    {
                        Accounts.Add(new Account { where = spl[0], username = spl[1] }, spl[2]);
                    }
                }
            }
        }
        /* Display: Displays account information from the account dictionary. Used to output to a textbox in WasspordGUI as of this time. 
         * Returns: Output from account dictionary. */
        public static string Display()
        {
            string display = "";
            foreach (var pair in Accounts)
            {
                display +=
                "Account Location: " + pair.Key.where
                + " | Account Username: " + pair.Key.username
                + " | Account Password: " + Decrypt(pair.Value) + "\r\n";
            }
            return display;
        }

        /* Encrypt: Encrypts the account password before it's saved to the account dictionary using AES. Can be decrypted by Decrypt.
		 * Parameters: password 
		 * Returns: encrypted password */

        public static string Encrypt(string password)
        {
            // Get bytes from password
            byte[] b = Encoding.Unicode.GetBytes(password);

            // Create Aes object
            using (Aes encrypt = Aes.Create())
            {
                // Derieve bytes from Key and Bytes
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Key, Bytes);
                encrypt.Key = pdb.GetBytes(32); 
                encrypt.IV = pdb.GetBytes(16);
                // Create streams for encryption
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encrypt.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(b, 0, b.Length);
                        cs.Close();
                    }
                    // Password is encrypted as a Base64 String based on the content from above Streams.
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
            // This is in case passwords had a space in them (like "test world"), it would error out without this.
            password = password.Replace(" ", "+");

            // Get bytes from password
            byte[] b = Convert.FromBase64String(password);

            // Create Aes object
            using (Aes encrypt = Aes.Create())
            {
                // Derieve bytes from Key and Bytes
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Key, Bytes);
                encrypt.Key = pdb.GetBytes(32);
                encrypt.IV = pdb.GetBytes(16);

                // Create streams for encryption
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encrypt.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(b, 0, b.Length);
                        cs.Close();
                    }
                    // Password is converted to a regular Unicode String based on contents from above stream.
                    password = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return password;
        }
        /* GeneratePassword: Generates a random password that's 16 characters in length.
         * Returns: Generated Password */
        public static string GeneratePassword()
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            StringBuilder password = new StringBuilder(string.Empty);
            Random r = new Random();
            int i = 0;
            while (i < 16)
            {
                password.Append(characters[r.Next(characters.Length)]);
                i++;
            }
            return password.ToString();
        }

        /* Reset: Clears the dictionary and resets the program. Used when "New" is clicked in the GUI. */
        public static void Reset()
        {
            Accounts.Clear();
        }
    }
}
