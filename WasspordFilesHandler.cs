using System.IO;
using System.Text;

namespace Wasspord
{
    /*
     * Methods: Save, Load
     * Properties/Misc: Filename, Folder, WasspordPassword, GetWasspordPassword, SetWasspordPassword, Init
     */
    /// <summary>
    /// This class handles saving and loading of .wasspord files, as well as setting/getting the wasspord file's password for accessing it.
    /// </summary>
    public static class WasspordFilesHandler // mutable class
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
                    sw.WriteLine(Encryption.GetKey()); // write our key to the file
                    if (WasspordPassword != "") // if wasspordpassword is not null
                    {
                        sw.WriteLine(Encryption.Encrypt(WasspordPassword)); // write down our key-encrypted wasspordpassword down
                    }
                    foreach (var acc in WasspordAccounts.GetAccounts()) // for each acc in Accounts, writeline into the file the location, username, value.
                    {
                        sw.WriteLine(acc.Key.location + "|" + acc.Key.username + "|" + acc.Value); // seperate by |'s
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
            Wasspord.Reset(); // Reset ahead of time so we don't have errors down the line.
            Encryption.SetKey("p055w4rd"); // Set it to the old key prior to starting, in case we have an old .wasspord file.
            string file = location + @"\" + filename; // this is our directory to the file
            bool fail = false;
            try
            {
                // This should never happen, but if it does create a blank file.
                if (!File.Exists(file))
                {
                    File.Create(file).Dispose();
                }
                using (StreamReader sr = new StreamReader(file))
                {
                    string line; // our current line as a string
                    int Line = 1; // the count of the line we're on, acts as our iterator

                    while ((line = sr.ReadLine()) != null) // while the current line StreamReader is reading isn't null
                    {
                        if (Line == 1 && !line.Contains("|")) // if we're on Line 1 and it doesn't contain a |, it's our key, assumedly.
                        {
                            bool IsValid = Encryption.Validate(line); // Validate it, it could be a bad file
                            if (IsValid) // Is this a Valid Base64 String Key?
                            {
                                Logger.Write("Setting Key to " + line); 
                                Encryption.SetKey(line); // We set it as our key
                            }
                            else // If it isn't, don't bother loading the file.
                            {
                                Logger.Write("Invalid key in the .wasspord file \"" + file + "\" (Key was \"" + line + "\").", "ERROR");
                                fail = true;
                                break; // Break the while loop, no point in trying to load any further
                            }
                        }
                        else if (Line == 2 && !line.Contains("|")) // If we're on Line 2 and it doesn't contain a |, it's likely our WassprodPassword
                        {
                            // Set WasspordPassword's value to the decrypted line.
                            WasspordPassword = Encryption.Decrypt(line);
                        }
                        else // Otherwise we add the accounts to the Accounts Dictionary
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
                                fail = true;
                                Wasspord.Reset(); // reset the program
                                break; // Break the while loop, no point in trying to load any further
                            }
                        }
                        Line++; // Increase Line iterator by 1
                    }
                }
                if(!fail) Logger.Write("File loaded: " + file);
            }
            catch (System.Exception e)
            {
                Logger.Write("Error loading file \"" + file + "\". (Error: " + e + ")", "ERROR");
            }
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

        /// <summary>
        /// Initializes the settings for WasspordFilesHandler
        /// </summary>
        public static void Init()
        {
            string folder = WasspordSettings.GetFolder();
            if (folder != null)
            {
                Folder = folder;
            }
            WasspordPassword = "";
        }
    }
}
