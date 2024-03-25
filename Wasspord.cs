﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Wasspord
{
    /*
     * Methods: AddAccount, UpdatePassword, DeleteAccount, Save, Load, Reset, Encrypt, Decrypt, 
     * GeneratePassword, ValidatePassword, Display
     * Important Variables/Misc: Account Dictionary, Account Struct, Key, Bytes, Passwords
     * Variables/Misc: regex, regexpattern, characters
    */

    public static class Wasspord
    {
        /* Account Dictionary: A dictionary with a key made of 2 parts (location, username)  that contains information on 
           where the account is used, the username and the password. The 2 parts allow flexibility and to have multiple accounts
           under the same username/email at multiple websites.
        */
        private static Dictionary<Account, string> Accounts = new Dictionary<Account, string>();
        /* Account Struct: This is our key when we insert entries into the Account dictionary,
           which is also the same key used to find entries in other functions. where is where 
           the account is used, username is the username/email used.
        */
        private struct Account
        {
            public string location;
            public string username;
        }
        /* Key and Bytes: These are used when we encrypt/decrypt passwords. Our key
           is an encryption key used in the encryption/decryption and our bytes is 
           our IV (initialization vector, or initial state). 
        */
        private static string Key = "p055w4rd";
        private static byte[] Bytes = { 0x31, 0xab, 0xa7, 0x91, 0x93, 0x9b, 0x7d, 0x1f, 0x3b, 0xf7, 0x8d, 0x3f, 0x9a };

        /* Passwords: Generated passwords kept track of by the program to prevent duplicates. */
        private static HashSet<string> Passwords = new HashSet<string>();

        /* Other Misc Things: characters, regexpattern, regex */
        private static string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
        private static string regexpattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])(?!.*(.)\\1{5,}).{8,32}$";
        /* It checks for:
        1 uppercase letter
        1 lowercase letter
        1 number
        1 special character
        should not repeat characters more than 5 times consecutively
        8-32 characters in width
        */
        private static Regex regex = new Regex(regexpattern);

        /* AddAccount: Adds an account with a location *where* it's used, the username and the encrypted password, to above mentioned
		   account dictionary, which can be saved to a .wasspord file down the line.
         * Parameters: location, username, password. */
        public static void AddAccount(string location, string username, string password)
        {
            Account acc;
            acc.location = location;
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
		 * Parameters: location, username, password. */
		public static void UpdatePassword(string location, string username, string password)
        {
            Account acc;
            acc.location = location;
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
        * Parameters: location, username. */
        public static void DeleteAccount(string location, string username)
        {
            Account acc;
            acc.location = location;
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
                using (StreamWriter sw = new StreamWriter(file)) // creates a StreamWriter which writes into our file
                {
                    foreach (var acc in Accounts) // for each acc in Accounts, writeline into the file the location, username, value.
                    {
                        sw.WriteLine(acc.Key.location + "|" + acc.Key.username + "|" + acc.Value);
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
            Reset(); // Reset ahead of time so we don't have errors down the line.
			string file = location + @"\" + filename;
			// This should never happen, but if it does create a blank file.
			if (!File.Exists(file))
            {
                File.Create(file).Dispose();
            }
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read); // open a FileStream for StreamReader to use
            using (var sr = new StreamReader(fs, Encoding.UTF8)) // creates a StreamReader to read our file
            {
                string line; // our current line
                while ((line = sr.ReadLine()) != null) // while the current line StreamReader is reading is not empty
                {
                    // Split the details we need (where, username, password) by the |'s into arrays
                    // acc[0] = where, acc[1] = username, acc[2] = password
                    var acc = line.Split('|');
                    // Below is in case we have duplicate keys, which can happen if you try to load the same file again.
                    // If the dictionary doesn't already have this key, add it.
                    if (!Accounts.ContainsKey(new Account { location = acc[0], username = acc[1] }))
                    {
                        Accounts.Add(new Account { location = acc[0], username = acc[1] }, acc[2]);
                    }
                }
            }
            fs.Dispose(); // Dispose of FileStream once we're done.
        }
        /* Display: Displays account information from the account dictionary. Used to output to a textbox in WasspordGUI as of this time. 
         * Returns: Output from account dictionary. */
        public static string Display()
        {
            string display = "";
            foreach (var pair in Accounts)
            {
                    display +=
                    "Account Location: " + pair.Key.location
               + " | Account Username: " + pair.Key.username
               + " | Account Password: " + Decrypt(pair.Value) 
               + "\r\n";
            }
            return display;
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rfc2898derivebytes?view=net-8.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.io.memorystream?view=net-8.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptostream?view=net-8.0

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
                // Derive bytes from Key and Bytes to create a key for encryption
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Key, Bytes);
                encrypt.Key = key.GetBytes(32); // this is our key
                encrypt.IV = key.GetBytes(16); // this is our IV (initialization vector)
                // Create streams for encryption
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encrypt.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(b, 0, b.Length);
                        cs.Close();
                    }
                    // Password is encrypted as a Base64 String based on the content from above streams.
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

            // Get bytes from password
            byte[] b = Convert.FromBase64String(password);

            // Create Aes object
            using (Aes encrypt = Aes.Create())
            {
                // Derive bytes from Key and Bytes to create a key for decryption
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Key, Bytes);
                encrypt.Key = key.GetBytes(32); // this is our key
                encrypt.IV = key.GetBytes(16); // this is our IV (initialization vector)

                try // Let's try our current method unless an error occurs
                {
                    // Create streams for decryption
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encrypt.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(b, 0, b.Length);
                            cs.Close();
                        }
                        // Password is converted to a regular Unicode String based on contents from above streams.
                        password = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                catch
                {
                    // This comes from older code, pre-commit 14ff6c5
                    // This is to keep compability with older files.
                    byte[] oldb;
                    string oldpassword;
                    oldb = Convert.FromBase64String(password);
                    oldpassword = ASCIIEncoding.ASCII.GetString(b);
                    return oldpassword;
                }
            }
            return password;
        }
        /* GeneratePassword: Generates a random password that's 16 characters in length,
           while also trying to prevent duplicates and non-regex following attempts
           by recursively calling itself up to 500 times before going with a duplicate / failed password.
         * Returns: Generated Password */
        public static string GeneratePassword(int attempt = 0)
        {
            StringBuilder password = new StringBuilder(string.Empty);
            Random r = new Random();
            int i = 0;
            while (i < 16)
            {
                password.Append(characters[r.Next(characters.Length)]);
                i++;
            }
            string GeneratedPass = password.ToString();
            // if Passwords doesn't contain GeneratedPass and it's good according to our regex
            if (!Passwords.Contains(GeneratedPass) && regex.IsMatch(GeneratedPass)) 
            {
                Passwords.Add(GeneratedPass); // add it to the list
                return GeneratedPass;
            }
            // Otherwise we try again
            else if (attempt < 500) 
            {
                attempt++; 
                return GeneratePassword(attempt);
            }
            else if (attempt == 500) // Unfortunately if recursion goes beyond 500 we'll have to settle for a duplicate. Don't want to slow the program.
            {
                return GeneratedPass; 
            }
            return "";
        }

        /* ValidatePassword: Validates a given password against a regex (details on what it checks is above).
         * Parameters: password
         * Returns: Regex result (either positive or negative) */
        public static string ValidatePassword(string password)
        {
            return !regex.IsMatch(password) ? "Sorry, this password isn't strong. A strong password should be a minimum of 8 characters but no longer than 32 and contain an uppercase, lowercase, digit, and special character and no excessive repeating characters." : "This password is strong.";
        }

        /* Reset: Clears the dictionary and "resets". Used when "New" or "Load" is clicked in the GUI. */
        public static void Reset()
        {
            Accounts.Clear();
        }
    }
}
