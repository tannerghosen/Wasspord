using System.Collections.Generic;

namespace Wasspord
{
    /*
     * Methods: Reset, Init
     */
    /// <summary>
    /// This class initializes other classes as well as reset the classes various settings
    /// </summary>
    public static class Wasspord // mutable class
    {
        /// <summary>
        /// Initalizes our program settings by calling multiple other classes Init methods and generating a key.
        /// </summary>
        public static void Init()
        {
            WasspordSettings.Init(); // Initalize our Settings
            WasspordFilesHandler.Init(); // Initialize our FilesHandler settings
            WasspordExtras.Init(); // Initialize WasspordExtras' stuff
            Encryption.GenerateKey(); // Create a key
            WasspordAccounts.ClearAccounts(); // Initialize our accounts dictionary
            Logger.Init();
        }

        /// <summary>
        /// Resets some of our program settings
        /// </summary>
        public static void Reset()
        {
            WasspordFilesHandler.SetWasspordPassword(""); // Set our WasspordPassword to an empty string
            WasspordAccounts.ClearAccounts(); // Clears our accounts dictionary
            Encryption.GenerateKey(); // Generate a new key
            Logger.Write("Resetted / cleared several items.");
        }
    }
}
