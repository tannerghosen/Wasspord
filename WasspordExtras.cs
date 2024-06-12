using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;

namespace Wasspord
{
    /*
     * Methods: GeneratePassword, ValidatePassword, Init, AddPassword
     * Properties/Misc: PasswordsFile, Passwords, Characters, RegexPattern, Regex
     */
    /* Basically, these are misc features which are extras to Wasspord's general purpose */

    internal class WasspordExtras
    {
        /* PasswordsFile: Contains our encrypted generated passwords */
        public static string PasswordsFile = "./GeneratePasswords.wasspord";

        /* Passwords: Generated passwords kept in a HashSet to prevent duplicate passwords from being generated. */
        private static HashSet<string> Passwords = new HashSet<string>();

        /* Other Misc Things: characters, regexpattern, regex */
        private static string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
        private static string RegexPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])(?!.*(.)\\1{5,}).{8,32}$";
        /* It checks for:
        1 uppercase letter
        1 lowercase letter
        1 number
        1 special character
        should not repeat characters more than 5 times consecutively
        8-32 characters in width
        */
        private static Regex Regex = new Regex(RegexPattern);

        /* GeneratePassword: Generates a random password that's 16 characters in length,
          while also trying to prevent duplicates and non-regex following attempts
          by recursively calling itself up to a predefined amount of times before going with 
          a duplicate / failed password.
        * Returns: Generated Password */
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
            else if (attempt < 1000)
            {
                attempt++;
                return GeneratePassword(attempt);
            }
            else if (attempt == 1000) // Unfortunately if recursion goes beyond 1000 we'll have to settle for a duplicate. Don't want to slow the program.
            {
                Logger.Write("Failed to give a unique / regex matching password after predefined attempt limit.", "WARNING");
                return GeneratedPass;
            }
            return "";
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
            if (!File.Exists(PasswordsFile))
            {
                File.Create(PasswordsFile).Dispose();
            }
            else // else, we've run this program before, read the content of the file.
            {
                var fs = new FileStream(PasswordsFile, FileMode.Open, FileAccess.Read); // open a FileStream for StreamReader to use
                using (var sr = new StreamReader(fs, Encoding.UTF8)) // creates a StreamReader to read our file
                {
                    string line; // our current line
                    while ((line = sr.ReadLine()) != null) // while the current line StreamReader is reading is not empty
                    {
                        Passwords.Add(EncryptDecrypt.Decrypt(line)); // Add passwords from file to our Passwords HashSet
                    }
                }
            }
        }

        /* AddPassword: Adds an uniquely generated password to our Passwords hashset, as well as the PasswordsFile file */
        public static void AddPassword(string password)
        {
            Passwords.Add(password);
            using (StreamWriter writer = new StreamWriter(PasswordsFile, true))
            {
                writer.WriteLine(EncryptDecrypt.Encrypt(password));
                writer.Close();
            }
        }
    }
}
