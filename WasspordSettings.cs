﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wasspord
{
    /// <summary>
    /// This class handles Wasspord's settings.
    /// </summary>
    public static class WasspordSettings
    {
        /* Wasspord Program Settings:
           This includes our program's settings (located at ./settings.json), Autosave and Display settings,
           and the file path and file name of our loaded .wasspord file
        */
        /// <summary>
        /// Settings file
        /// </summary>
        public static string SettingsFile = "./settings.json";
        /// <summary>
        /// Autosave variable
        /// </summary>
        public static bool Autosave { get; set; }
        /// <summary>
        /// Display variable, handles whether or not info like passwords will be shown by default or not on file load.
        /// </summary>
        public static bool Display { get; set; }

        /// <summary>
        /// Our folder where .wasspord files go to (by default (folder Wasspord.exe is in)\Accounts))
        /// </summary>
        public static string Folder { get; set; }

        public static void Init()
        {
            if (!File.Exists(SettingsFile)) // if settings.json file doesn't exist
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
                string json = File.ReadAllText(SettingsFile); // read the file as a string
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
            using (StreamWriter writer = new StreamWriter(SettingsFile))
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
        public static string GetFolder()
        {
            return Folder;
        }
    }
}