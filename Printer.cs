namespace Wasspord
{
    /*
     * Methods: Print
     */
    // Simply a class with a method to print out content to our view (GUI).
    // If I was to remake this program in a different GUI framework this class would probably not return.
    internal static class Printer
    {
        /* Print: Prints out specific account information depending on the value of parameter item.
         * Parameters: item (the item we want to print out a list for, i.e. locations, usernames, passwords) */
        public static string Print(string item)
        {
            string print = "";
            foreach (var pair in WasspordAccounts.GetAccounts())
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
    }
}
