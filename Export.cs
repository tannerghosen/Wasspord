using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasspord
{
    public static class Export
    {
        public static void ExportToTxt(string location, string filename)
        {
            if (WasspordAccounts.Accounts.Count == 0) return;
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
                    foreach (var acc in WasspordAccounts.GetAccounts()) 
                    {
                        sw.WriteLine(Encryption.Decrypt(acc.Key.location) + "," + Encryption.Decrypt(acc.Key.username) + "," + Encryption.Decrypt(acc.Value));
                    }
                }
                Logger.Write("Exported text file: " + file);
            }
            catch (System.Exception e)
            {
                Logger.Write("Error saving to file \"" + file + "\". (Error: " + e + ")", "ERROR");
            }

        }

        public static void ExportToCsv(string location, string filename)
        {
            if (WasspordAccounts.Accounts.Count == 0) return;
        }
    }
}
