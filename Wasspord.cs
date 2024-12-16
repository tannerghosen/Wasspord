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
            WasspordSettings.Init(); // Initalize our Settings
            WasspordFilesHandler.Init(); // Initialize our FilesHandler settings
            WasspordExtras.Init(); // Initialize WasspordExtras' stuff
            Encryption.GenerateKey(); // Create a key
            WasspordAccounts.SetAccounts(new Dictionary<WasspordAccounts.Account, string>()); // This prevents a null reference error by giving it a value instead of letting it be initialized as null on the Load method being used.
        }

        /// <summary>
        /// Resets some of our program settings
        /// </summary>
        public static void Reset()
        {
            WasspordFilesHandler.SetWasspordPassword(""); // Set our WasspordPassword to an empty string
            WasspordAccounts.SetAccounts(new Dictionary<WasspordAccounts.Account, string>()); // Reset our account dictionary
            WasspordFilesHandler.Filename = ""; // Set the filename to nothing
            Encryption.GenerateKey(); // Generate a new key
            Logger.Write("Resetted / cleared several items.");
        }
    }
}
