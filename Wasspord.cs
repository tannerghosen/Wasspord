using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Wasspord
{
    /*
     * Methods: Save, Load, Reset, Init, UpdateSettings, SaveSettings, GetWasspordPassword, SetWasspordPassword
     * Properties/Misc: Settings, Autosave, Display, Filename, Folder
     */
    /// <summary>
    /// This class serves as the main class which handles settings, saving and loading, .wasspord-specific passwords, reseting the program
    /// and initializing it.
    /// </summary>
    public static class Wasspord
    {
        /* Wasspord Program Settings:
           This includes our program's settings (located at ./settings.json), Autosave and Display settings,
           and the file path and file name of our loaded .wasspord file
        */
        /// <summary>
        /// Settings file
        /// </summary>
        public static string Settings = "./settings.json";
        /// <summary>
        /// Autosave variable
        /// </summary>
        public static bool Autosave { get; set; }
        /// <summary>
        /// Display variable, handles whether or not info like passwords will be shown by default or not on file load.
        /// </summary>
        public static bool Display { get; set; }
        /// <summary>
        /// Our opened file's name
        /// </summary>
        public static string Filename { get; set; }
        /// <summary>
        /// Our folder where .wasspord files go to (by default (folder Wasspord.exe is in)\Accounts))
        /// </summary>
        public static string Folder { get; set; }
        /// <summary>
        /// Our .wasspord's password (optional)
        /// </summary>
        public static string WasspordPassword { get; set; }
        /* 
           In both Save and Load, we use Streams to write to a file and read a file.
           In .NET, Streams are an abstraction of a sequence of bytes (a file in this case)

           StreamWriter writes to a specified file, FileStream opens a file with various
           options (mode, read/write) and creates a stream, and StreamReader reads the FileStream 
           (or any Stream) so long as it's given the encoding (in this case because we use UTF-8 it's Encoding.UTF8) 
        */
        /// <summary>
        /// Saves account information to a .wasspord file for future use by the end user by writing data down from the Account dictionary.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="filename"></param>
        public static void Save(string location, string filename)
        {
            string file = location + @"\" + filename;
            if (WasspordPassword == null)
            {
                WasspordPassword = "";
            }
            try
            {
                // This should only ever happen if the file being saved is deleted (via the Save option, Save As should be fine.)
                if (!File.Exists(file))
                {
                    File.Create(file).Dispose();
                }
                using (StreamWriter sw = new StreamWriter(file)) // creates a StreamWriter which writes into our file
                {
                    sw.WriteLine(Encryption.GetKey());
                    if (WasspordPassword != "")
                    {
                        sw.WriteLine(Encryption.Encrypt(WasspordPassword)); // Pre-planning Password Support for .wasspord files
                    }
                    foreach (var acc in WasspordAccounts.GetAccounts()) // for each acc in Accounts, writeline into the file the location, username, value.
                    {
                        sw.WriteLine(acc.Key.location + "|" + acc.Key.username + "|" + acc.Value);
                    }
                }
                Logger.Write("File saved to: " + file);
            }
            catch(System.Exception e)
            {
                Logger.Write("Error saving to file \"" + file + "\". (Error: " + e + ")", "ERROR");
            }
        }

        /// <summary>
        /// Loads previously saved account information from a .wasspord file to the account dictionary for current use. 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="filename"></param>
        public static void Load(string location, string filename)
        {
            Reset(); // Reset ahead of time so we don't have errors down the line.
            Encryption.SetKey("p055w4rd"); // Set it to the old key prior to starting, in case we have an old .wasspord file.
            string file = location + @"\" + filename;
            try
            {
                // This should never happen, but if it does create a blank file.
                if (!File.Exists(file))
                {
                    File.Create(file).Dispose();
                }
                var fs = new FileStream(file, FileMode.Open, FileAccess.Read); // open a FileStream for StreamReader to use
                using (var sr = new StreamReader(fs, Encoding.UTF8)) // creates a StreamReader to read our file
                {
                    string line; // our current line
                    int Line = 1;

                    while ((line = sr.ReadLine()) != null) // while the current line StreamReader is reading is not empty
                    {
                        if (Line == 1 && !line.Contains("|")) // If the .wasspord file has a key set
                        {
                            bool IsValid = Encryption.Validate(line); // Validate it, it could be a bad file
                            if (IsValid) // Is this a Valid Base64 String Key?
                            {
                                Logger.Write("Setting Key to " + line);
                                Encryption.SetKey(line);
                            }
                            else // If it isn't, don't bother loading the file.
                            {
                                Logger.Write("Invalid key in the .wasspord file \"" + file + "\" (Key was \"" + line + "\").", "ERROR");
                                fs.Dispose();
                                break; // Break the while loop, no point in trying to load any further
                            }
                        }
                        else if (Line == 2 && !line.Contains("|")) // Does the .wasspord file have a password to unlock it?
                        {
                            // If so, set WasspordPassword's value to the decrypted line.
                            //WasspordPassword = line;
                            WasspordPassword = Encryption.Decrypt(line);
                        }
                        else
                        {
                            try // try-catch in case the acc cannot be split by |
                            {
                                // Split the details we need (location, username, password) by the |'s into an array
                                // acc[0] = location, acc[1] = username, acc[2] = password
                                var acc = line.Split('|');
                                WasspordAccounts.AddAccount(acc[0], acc[1], acc[2]);
                            }
                            catch // if we get a bad file after the key check
                            {
                                Logger.Write("Bad .wasspord file \"" + file + "\".", "ERROR");
                                fs.Dispose();
                                break; // Break the while loop, no point in trying to load any further
                            }
                        }
                        Line++;
                    }
                }
                Logger.Write("File loaded: " + file);
                fs.Dispose(); // Dispose of FileStream once we're done.
            }
            catch (System.Exception e)
            {
                Logger.Write("Error loading file \"" + file + "\". (Error: " + e + ")", "ERROR");
            }
        }

        /// <summary>
        /// Clears the dictionary, generates a new key (which may be overwritten if load was used) and resets the opened file name.
        /// </summary>
        public static void Reset()
        {
            WasspordPassword = "";
            WasspordAccounts.SetAccounts(new Dictionary<WasspordAccounts.Account, string>()); // Clear account dictionary
            Filename = ""; // reset the filename to nothing
            Encryption.GenerateKey(); // Generate a new key
            Logger.Write("Resetted / cleared several items.");
        }

        /// <summary>
        /// Initalizes our program settings, creates settings.json and our Accounts folder.
        /// </summary>
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

                    Logger.Write("Custom Accounts folder \"" + Folder + "\" is missing; recreating it.", "WARNING");
                }
            }

            // If our default Accounts folder is deleted and it is still set as the default folder, we'll need to make it again
            if (!Directory.Exists("Accounts") && Folder == Path.Combine(Directory.GetCurrentDirectory() + "\\Accounts\\"))
            {
                Directory.CreateDirectory("Accounts");

                Logger.Write("Created Accounts folder in \"" + Directory.GetCurrentDirectory() + "\".");
            }

            WasspordPassword = ""; // Set our WasspordPassword to an empty string
            WasspordExtras.Init(); // Initialize WasspordExtras' stuff.
            Encryption.GenerateKey(); // Create a key
            WasspordAccounts.SetAccounts(new Dictionary<WasspordAccounts.Account, string>()); // This prevents a null reference error by giving it a value instead of letting it be initialized as null on the Load method being used.
        }

        /// <summary>
        /// Updates a specified setting.
        /// </summary>
        /// <param name="setting"></param>
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
        /// <summary>
        /// Updates a specified setting with a value.
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        public static void UpdateSettings(string setting, string value)
        {
            // While WasspordGUI might change Folder's value, this is only temporary - unless this method is called, it only lasts as long as the program is open.
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
        /// <summary>
        /// Saves our settings to settings.json.
        /// </summary>
        public static void SaveSettings()
        {
            // We write into our settings.json file a JSON object
            // This contains our settings.
            string autosave = Autosave.ToString().ToLower(), display = Display.ToString().ToLower(), folder = JsonSerializer.Serialize(Folder);
            using (StreamWriter writer = new StreamWriter(Settings))
            {
                // Because C# bools are capitalized, we need to lowercase it before we send it,
                // as shown in the code below.
                writer.WriteLine("{");
                writer.WriteLine("\"Autosave\":" + autosave + ",");
                writer.WriteLine("\"Display\":" + display + ",");
                writer.WriteLine("\"Folder\": " + folder); // we need to make our Folder string into a JSON string that won't cause errors.
                writer.WriteLine("}");
                writer.Close();
            }
            Logger.Write("Saved Settings: Autosave Value = " + Autosave + ", Display Value = " + Display + ", Folder Value = " + Folder + ".");
        }

        /// <summary>
        /// Returns the .wasspord file's current password.
        /// </summary>
        /// <returns>.wasspord file's current password</returns>
        public static string GetWasspordPassword()
        {
            return WasspordPassword;
        }

        /// <summary>
        /// Sets the .wasspord file's password.
        /// </summary>
        /// <param name="pass"></param>
        public static void SetWasspordPassword(string pass)
        {
            WasspordPassword = pass;
        }
    }
}
