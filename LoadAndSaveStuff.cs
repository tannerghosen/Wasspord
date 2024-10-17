using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasspord
{
    public static class LoadAndSaveStuff
    {
        /// <summary>
        /// Our opened file's name
        /// </summary>
        public static string Filename { get; set; }
        /// <summary>
        /// Our folder where .wasspord files go to
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
            catch (System.Exception e)
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

        public static void Init()
        {
            string folder = Settings.GetFolder();
            if (folder != null)
            {
                Folder = folder;
            }
        }
    }
}
