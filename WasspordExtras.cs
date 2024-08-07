using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Wasspord
{
    /*
     * Methods: GeneratePassword, ValidatePassword, Init, AddPassword
     * Properties/Misc: PasswordsFile, Passwords, Characters, RegexPattern, Regex, MaxAttempts
     */
    /* Basically, these are misc features which are extras to Wasspord's general purpose */
    /// <summary>
    /// This class serves extra features to Wasspord, such as password generation and validation.
    /// </summary>
    public static class WasspordExtras
    {
        /* GenPassesFile: Contains our encrypted generated passwords */
        public static string GenPassesFile = "./GeneratedPasswords.passwords";

        /* Passwords: Generated passwords kept in a HashSet to prevent duplicate passwords from being generated. */
        private static HashSet<string> Passwords = new HashSet<string>();

        /* Other Misc Things: Characters, RegexPattern, Regex, MaxAttempts */
        private static string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";

        /* It checks for:
        1 uppercase letter
        1 lowercase letter
        1 number
        1 special character
        should not repeat characters more than 5 times consecutively
        8-32 characters in width
        */
        private static string RegexPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])(?!.*(.)\\1{5,}).{8,32}$";

        private static Regex Regex = new Regex(RegexPattern);

        /* GeneratePassword: Generates a random password that's 16 characters in length,
          while also trying to prevent duplicates and non-regex following attempts
          by recursively calling itself up to a specified amount of times before going with 
          a duplicate / failed password.
          Parameter: attempt (autoincrements on every recursion call, optional parameter)
          Returns: Generated Password */
        public static string GeneratePassword(int attempt = 0)
        {
            StringBuilder password = new StringBuilder(string.Empty);
            Random r = new Random();
            for (int i = 0; i < 16; i++)
            {
                // characters[Random([0, characters' length])];
                password.Append(Characters[r.Next(Characters.Length)]);
            }
            string GeneratedPass = password.ToString();
            // if Passwords doesn't contain GeneratedPass and it's good according to our regex
            if (!Passwords.Contains(GeneratedPass) && Regex.IsMatch(GeneratedPass))
            {
                AddPassword(GeneratedPass); // add it to the list
                Logger.Write("Generated Password Successfully!");
                return GeneratedPass;
            }
            // Otherwise we try again
            else if (attempt != 2000)
            {
                attempt++;
                return GeneratePassword(attempt);
            }
            else // Unfortunately if recursion goes beyond predefined limit we'll have to settle for a duplicate or bad regex password. Don't want to slow the program.
            {
                return GeneratedPass;
            }
        }

        /* ValidatePassword: Validates a given password against a regex (details on what it checks is above).
         * Parameters: password
         * Returns: Regex result (either positive or negative) */
        public static string ValidatePassword(string password)
        {
            return !Regex.IsMatch(password) ? "Sorry, this password isn't strong. A strong password should be a minimum of 8 characters but no longer than 32 and contain an uppercase, lowercase, digit, and special character and no excessive repeating characters." : "This password is strong.";
        }

        /* Init: Initializes the Passwords HashSet with the contents of PasswordsFile, or creates the file. */
        public static void Init()
        {
            // If this is the first time we're running Wasspord OR the user deleted the GeneratedPasswords file
            if (!File.Exists(GenPassesFile))
            {
                File.Create(GenPassesFile).Dispose();
            }
            else // else, we've run this program before, read the content of the file.
            {
                var fs = new FileStream(GenPassesFile, FileMode.Open, FileAccess.Read); // open a FileStream for StreamReader to use
                using (var sr = new StreamReader(fs, Encoding.UTF8)) // creates a StreamReader to read our file
                {
                    string line; // our current line
                    while ((line = sr.ReadLine()) != null) // while the current line StreamReader is reading is not empty
                    {
                        Passwords.Add(Encryption.Decrypt(line)); // Add passwords from file to our Passwords HashSet
                    }
                }
            }
        }

        /* AddPassword: Adds an uniquely generated password to our Passwords hashset, as well as the GeneratedPasswords file 
         * Parameters: password
         */
        public static void AddPassword(string password)
        {
            Passwords.Add(password); // Add the generated password to the Passwords hashset
            string OldKey = Encryption.GetKey(); // Store our current encryption key
            Encryption.SetKey("p055w4rd"); // Because the encryption used for the file uses the old encryption key, we set it here
            using (StreamWriter writer = new StreamWriter(GenPassesFile, true))
            {
                writer.WriteLine(Encryption.Encrypt(password));
                writer.Close();
            }
            Encryption.SetKey(OldKey); // We set the encryption key back to the current one now that AddPassword's job is done
        }
    }
}
