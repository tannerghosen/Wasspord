using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasspord
{
    /// <summary>
    /// This class handles exporting the contents of a loaded .wasspord file to other file types.
    /// </summary>
    public static class Export
    {
        /// <summary>
        /// Exports accounts dictionary of loaded file to various file types.
        /// </summary>
        /// <param name="location">The location of where this file will go.</param>
        /// <param name="filename">File name</param>
        /// <param name="type">The file type</param>
        public static void export(string location, string filename, string type)
        {
            if (WasspordAccounts.GetCount() == 0) return;
            string file = location + @"\" + filename + "." + type;
            if (!File.Exists(file))
            {
                File.Create(file).Dispose();
            }
            switch (type)
            {
                case "txt":
                case "csv":
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(file)) // creates a StreamWriter which writes into our file
                        {
                            foreach (var acc in WasspordAccounts.GetAccounts())
                            {
                                sw.WriteLine(Encryption.Decrypt(acc.Key.location) + "," + Encryption.Decrypt(acc.Key.username) + "," + Encryption.Decrypt(acc.Value));
                            }
                        }
                        Logger.Write("Exported " + type + " file: " + file);
                    }
                    catch (System.Exception e)
                    {
                        Logger.Write("Error saving to file \"" + file + "\". (Error: " + e + ")", "ERROR");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
