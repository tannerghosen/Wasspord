using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Wasspord
{
    /*
     * Methods: AddAccount, UpdatePassword, DeleteAccount, Save, Load, Reset, ClearAccounts,
     * Print, Init, UpdateSettings, SaveSettings
     * Important Variables/Misc: Account Dictionary, Account Struct
     * Variables/Misc: Settings, Autosave, Display, Filename, Folder
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
        public static string Folder { get; set; } // Our folder where .wasspord files go to (by default (folder Wasspord.exe is in)\Accounts)

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
                Logger.Write("Duplicate Account \"" + acc.username + "\"", "ERROR");
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
                Logger.Write("Account \"" + acc.username + "\" doesn't exist / is invalid", "ERROR");
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
                Logger.Write("Account \"" + acc.username + "\" doesn't exist / is invalid", "ERROR");
            }
            else
            {
                Logger.Write("Deleted Account '" + acc.username + "'.");
                Accounts.Remove(acc);
            }
        }
        /* ClearAccounts: Clears the accounts dictionary. */
        public static void ClearAccounts()
        {
            Accounts.Clear();
            Logger.Write("Cleared accounts dictionary.");
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
                    foreach (var acc in Wasspord.Accounts) // for each acc in Accounts, writeline into the file the location, username, value.
                    {
                        sw.WriteLine(acc.Key.location + "|" + acc.Key.username + "|" + acc.Value);
                    }
                }
                Logger.Write("File saved to: " + file);
            }
            catch
            {
                Logger.Write("Error saving to file \"" + file + "\".", "ERROR");
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
                    //if (!Accounts.ContainsKey(new Account { location = acc[0], username = acc[1] }))
                    //{
                        Accounts.Add(new Account { location = acc[0], username = acc[1] }, acc[2]);
                    //}
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
                    default:
                        Logger.Write("Invalid item was specified for Print in the item parameter.", "ERROR");
                        break;
                }
            }
            return print;
        }

        /* Reset: Clears the dictionary AND resets the opened file name / file path. Used when "Load" is clicked in the GUI, respectively */
        public static void Reset()
        {
            ClearAccounts();
            Filename = "";
            string json = File.ReadAllText(Settings); // read the file as a string
            JsonDocument settings = JsonDocument.Parse(json); // parse it as a json string
            Folder = settings.RootElement.GetProperty("Folder").GetString(); // get Folder from our settings, in case the user saved a file in a different folder than the default one (which would change it, requiring it to be reset)
            Logger.Write("Reset filename / filepath.");
        }

        /* Init: Initalizes our program settings, creates settings.json and our Accounts folder */
        public static void Init()
        {
            if (!File.Exists(Settings)) // if settings.json file doesn't exist
            {
                // We initialize it with default settings
                Autosave = false;
                Display = true;
                Folder = Path.Combine(Directory.GetCurrentDirectory() + "\\Accounts\\");

                SaveSettings(); // Save the settings

                Logger.Write("Created settings file.");
            }
            else // else load the settings from settings.json
            {
                string json = File.ReadAllText(Settings); // read the file as a string
                JsonDocument settings = JsonDocument.Parse(json); // parse it as a json string

                Autosave = settings.RootElement.GetProperty("Autosave").GetBoolean(); // we get the properties' value for both Autosave, Display and Folder
                Display = settings.RootElement.GetProperty("Display").GetBoolean(); // and we set our class variables to it.
                Folder = settings.RootElement.GetProperty("Folder").GetString();

                settings.Dispose(); // end the Parse

                Logger.Write("Loaded settings file. Autosave Value = " + Autosave + ", Display Value = " + Display + ", Folder Value = " + Folder + ".");

                // If our custom folder is deleted and it was changed from the autogenerated Accounts, we need to create it again
                if (!Directory.Exists(Folder) && Folder != Path.Combine(Directory.GetCurrentDirectory() + "\\Accounts\\"))
                {
                    Directory.CreateDirectory(Folder);

                    Logger.Write("Custom accounts folder \"" + Folder + "\" was missing; creating it.", "WARNING");
                }
            }

            // If our default Accounts folder is deleted, we'll need to make it again
            if (!Directory.Exists("Accounts"))
            {
                Directory.CreateDirectory("Accounts");

                Logger.Write("Created Accounts folder in \"" + Directory.GetCurrentDirectory() + "\".");
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
                    Logger.Write("Invalid setting was specified for UpdateSettings without value parameter.", "ERROR");
                    break;
            }

            Logger.Write("Updated Settings: Autosave Value = " + Autosave + ", Display Value = " + Display + ".");
            // And we save our settings.
            SaveSettings();
        }
        /* UpdateSettings: Updates a specified setting with a value.
         * Parameters: setting, value
         */
        public static void UpdateSettings(string setting, string value)
        {
            switch (setting)
            {
                case "Folder":
                    Folder = value;
                    break;
                default:
                    Logger.Write("Invalid setting was specified for UpdateSettings with value parameter.", "ERROR");
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
                writer.WriteLine("\"Folder\": " + JsonSerializer.Serialize(Folder)); // we need to make our Folder string into a JSON string that won't cause errors.
                writer.WriteLine("}");
                writer.Close();
            }
            Logger.Write("Saved Settings: Autosave Value = " + Autosave + ", Display Value = " + Display + ", Folder Value = " + Folder + ".");
        }
    }
}
