using System.Collections.Generic;

namespace Wasspord
{
    /*
     * Methods: Reset, Init
     */
    /// <summary>
    /// This class initializes other classes as well as reset the classes various settings
    /// </summary>
    public static class Wasspord
    {
        /// <summary>
        /// Initalizes our program settings, creates settings.json and our Accounts folder.
        /// </summary>
        public static void Init()
        {
            WasspordSettings.Init();
            WasspordFilesHandler.Init();
            WasspordFilesHandler.SetWasspordPassword(""); // Set our WasspordPassword to an empty string
            WasspordExtras.Init(); // Initialize WasspordExtras' stuff.
            Encryption.GenerateKey(); // Create a key
            WasspordAccounts.SetAccounts(new Dictionary<WasspordAccounts.Account, string>()); // This prevents a null reference error by giving it a value instead of letting it be initialized as null on the Load method being used.
        }

        /// <summary>
        /// Clears the dictionary, generates a new key (which may be overwritten if load was used) and resets the opened file name.
        /// </summary>
        public static void Reset()
        {
            WasspordFilesHandler.SetWasspordPassword("");
            WasspordAccounts.SetAccounts(new Dictionary<WasspordAccounts.Account, string>()); // Clear account dictionary
            WasspordFilesHandler.Filename = ""; // reset the filename to nothing
            Encryption.GenerateKey(); // Generate a new key
            Logger.Write("Resetted / cleared several items.");
        }
    }
}
