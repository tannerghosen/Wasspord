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
    /// <summary> 
    /// Wasspord.cs
    /// Methods: AddAccount, UpdatePassword, DeleteAccount, Save, Load, Encrypt, Decrypt
    /// Purpose of File: Brains of the program, handles adding/modifying/delete accounts, saving/loading them
    /// and ensuring passwords are encrypted.
    /// </summary>

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
                Console.Error.WriteLine("Duplicate Account");
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
		/* Encrypt: Encrypts the account password before it's saved to the account dictionary in Base64. Can be decrypted by Decrypt.
		 * Parameters: password 
		 * Returns: encrypted password */
		private static string Encrypt(string password)
        {
            byte[] b = ASCIIEncoding.ASCII.GetBytes(password);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
		/* Decrypt: Decrypts the account password previously encrypted and saved to either an account dictionary or a .wasspord file.
		 * Parameters: password
		 * Returns: decrypted password */
		private static string Decrypt(string password)
        {
            byte[] b;
            string decrypted;
            b = Convert.FromBase64String(password);
            decrypted = ASCIIEncoding.ASCII.GetString(b);
            return decrypted; 
        }
    }
}
