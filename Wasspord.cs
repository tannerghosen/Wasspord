using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace Wasspord
{
    /*
     * Methods: AddAccount, UpdatePassword, DeleteAccount, Save, Load, Reset, ClearAccounts,
     * GeneratePassword, ValidatePassword, Print, Init, UpdateSettings, SaveSettings
     * Important Variables/Misc: Account Dictionary, Account Struct, Key, Bytes, Passwords
     * Variables/Misc: regex, regexpattern, characters, Settings, Autosave, Display, Filename, Folder
    */

    public static class Wasspord
    {
        /* Wasspord Program Settings:
           This includes our program's settings (located at ./settings.json), Autosave and Display settings,
           and the file path and file name of our loaded .wasspord file
         */
        public static string Settings = "./settings.json";
        public static bool Autosave { get; set; }
        public static bool Display { get; set; }
        public static string Filename { get; set; } // Our file's name
        public static string Folder { get; set; }// Our folder where .wasspord files go to (by default (folder Wasspord.exe is in)\Accounts)

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

        /* Passwords: Generated passwords kept in a HashSet to prevent duplicate passwords from being generated. */
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
                Logger.Write("Duplicate Account '" + acc.username + "'", "ERROR");
            }
            else
            {
                password = EncryptDecrypt.Encrypt(password);
                Logger.Write("Added Account '" + acc.username + "'.");
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
                Logger.Write("Account '" + acc.username + "' doesn't exist / is invalid", "ERROR");
            }
            else
            {
                password = EncryptDecrypt.Encrypt(password);
                Logger.Write("Updated Password of Account '" + acc.username + "'.");
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
                Logger.Write("Account '" + acc.username + "' doesn't exist / is invalid", "ERROR");
            }
            else
            {
                Logger.Write("Deleted Account '" + acc.username + "'.");
                Accounts.Remove(acc);
            }
        }
        /* 
           In both Save and Load, we use Streams to write to a file and read a file.
           In .NET, Streams are an abstraction of a sequence of bytes (a file in this case)

           StreamWriter writes to a specified file, FileStream opens a file with various
           options (mode, read/write) and creates a stream, and StreamReader reads the FileStream 
           (or any Stream) so long as it's given the encoding (in this case because we use UTF-8 it's Encoding.UTF8) 
        */

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
                Logger.Write("File saved to: " + file);
            }
            catch
            {
                Logger.Write("Error saving to file "+file, "ERROR");
            }
        }
		/* Load: Loads previously saved account information from a .wasspord file to the account dictionary for current use. 
         * Parameters: location (filepath to file), filename (name of the file). */
		public static void Load(string location, string filename)
        {
            //Reset(); // Reset ahead of time so we don't have errors down the line.
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
            Logger.Write("File loaded: " + file);
            fs.Dispose(); // Dispose of FileStream once we're done.
        }

        /* Print: Prints out specific account information depending on the value of parameter item.
         * Parameters: item (the item we want to print out a list for, i.e. locations, usernames, passwords) */

        public static string Print(string item)
        {
            string print = "";
            foreach (var pair in Accounts)
            {
                switch (item)
                {
                    case "Location":
                        print += pair.Key.location + "\r\n";
                        break;
                    case "Username":
                        print += pair.Key.username + "\r\n";
                        break;
                    case "Password":
                        print += EncryptDecrypt.Decrypt(pair.Value) + "\r\n";
                        break;
                }
            }
            return print;
        }

        /* GeneratePassword: Generates a random password that's 16 characters in length,
           while also trying to prevent duplicates and non-regex following attempts
           by recursively calling itself up to 500 times before going with a duplicate / failed password.
         * Returns: Generated Password */
        public static string GeneratePassword(int attempt = 0)
        {
            StringBuilder password = new StringBuilder(string.Empty);
            Random r = new Random();
            for(int i = 0; i < 16; i++)
            {
                                // characters[Random([0, characters' length])];
                password.Append(characters[r.Next(characters.Length)]);
            }
            string GeneratedPass = password.ToString();
            // if Passwords doesn't contain GeneratedPass and it's good according to our regex
            if (!Passwords.Contains(GeneratedPass) && regex.IsMatch(GeneratedPass)) 
            {
                Passwords.Add(GeneratedPass); // add it to the list
                Logger.Write("Generated Password Successfully!");
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
                Logger.Write("Failed to give a unique password after predefined attempt limit.","WARNING");
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

        /* Reset & ClearAccounts: Clears the dictionary AND resets the opened file name / file path back to the starting point, and ONLY clear the accounts dictionary. Used when "New" and "Load" is clicked in the GUI, respectively */
        public static void Reset()
        {
            ClearAccounts();
            Filename = "";
            string json = File.ReadAllText(Settings);
            JsonDocument settings = JsonDocument.Parse(json);
            Folder = settings.RootElement.GetProperty("Folder").GetString();
            Logger.Write("Reset filename / filepath.");
        }
        public static void ClearAccounts()
        {
            Accounts.Clear();
            Logger.Write("Cleared accounts dictionary.");
        }

        /* Init: Initalizes our program settings, creates settings.json and our Accounts folder */
        public static void Init()
        {
            if (!File.Exists(Settings)) // if config file doesn't exist
            {
                Autosave = false;
                Display = true;
                Folder = Path.Combine(Directory.GetCurrentDirectory() + "\\Accounts\\");

                SaveSettings();

                Logger.Write("Created settings file.");
            }
            else // else load the settings from the config
            {
                string json = File.ReadAllText(Settings); // read the file as a string
                JsonDocument settings = JsonDocument.Parse(json); // parse it as a json string

                Autosave = settings.RootElement.GetProperty("Autosave").GetBoolean(); // we get the properties' value for both Autosave, Display and Folder
                Display = settings.RootElement.GetProperty("Display").GetBoolean(); // and we set our class variables to it.
                Folder = settings.RootElement.GetProperty("Folder").GetString();
                if (!Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);

                    Logger.Write("Custom accounts folder was missing; creating it.");
                }

                settings.Dispose();

                Logger.Write("Loaded settings file. Autosave Value = " + Autosave + ", Display Value = " + Display + ", Folder Value = " + Folder + ".");
            }

            if (!Directory.Exists("Accounts"))
            {
                Directory.CreateDirectory("Accounts");

                Logger.Write("Created Accounts folder.");
            }
        }
        /* UpdateSettings: Updates a specified setting.
         * Parameters: setting
         */
        public static void UpdateSettings(string setting)
        {
            // Simply enough, this switch inverts our setting
            switch (setting)
            {
                case "Autosave":
                    Autosave = !Autosave;
                    break;
                case "Display":
                    Display = !Display;
                    break;
                default:
                    Logger.Write("Invalid setting was specified for UpdateSettings without value parameter", "ERROR");
                    break;
            }

            Logger.Write("Updated Settings: Autosave Value = " + Autosave + ", Display Value = " + Display + ".");
            // And we save our settings.
            SaveSettings();
        }
        public static void UpdateSettings(string setting, string value)
        {
            switch (setting)
            {
                case "Folder":
                    Folder = value;
                    break;
                default:
                    Logger.Write("Invalid setting was specified for UpdateSettings with value parameter", "ERROR");
                    break;
            }

            Logger.Write("Updated Settings: Folder Value = " + Folder + ".");
            
            SaveSettings();
        }

        /* SaveSettings: Saves our settings to settings.json. */
        public static void SaveSettings()
        {
            // We write into our settings.json file an JSON object
            // This contains our settings.
            using (StreamWriter writer = new StreamWriter(Settings))
            {
                // Because C# bools are capitalized, we need to lowercase it before we send it,
                // as shown in the code below.
                writer.WriteLine("{");
                writer.WriteLine("\"Autosave\":" + Autosave.ToString().ToLower() + ",");
                writer.WriteLine("\"Display\":" + Display.ToString().ToLower() + ",");
                writer.WriteLine("\"Folder\": " + JsonSerializer.Serialize(Folder));
                writer.WriteLine("}");
                writer.Close();
            }
            Logger.Write("Saved Settings: Autosave Value = " + Autosave + ", Display Value = " + Display + ", Folder Value = " + Folder + ".");
        }
    }
}
