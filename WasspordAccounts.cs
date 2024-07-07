using System.Collections.Generic;
using System.Linq;

namespace Wasspord
{
    /*
     * Methods: ManageAccount, AddAccount, GetAccounts, SetAccounts, GetRow
     * Properties/Misc: Account Dictionary, Account Struct
     */

    internal static class WasspordAccounts
    {

        /* Account Dictionary: A dictionary with a key made of 2 parts (location, username)  that contains information on 
          where the account is used, the username and the password. The 2 parts allow flexibility and to have multiple accounts
          under the same username/email at multiple websites.
       */
        public static Dictionary<Account, string> Accounts = new Dictionary<Account, string>();
        /* Account Struct: This is our key when we insert entries into the Account dictionary,
           which is also the same key used to find entries in other functions. where is where 
           the account is used, username is the username/email used.
        */
        public struct Account
        {
            public string location;
            public string username;
        }

        /* ManageAccount: Adds, updates, and deletes accounts to/from the account dictionary.
         * Parameters: operation (add/update/delete), location, username, password (optional) */
        public static void ManageAccount(string operation, string location, string username, string password)
        {
            Account acc;
            acc.location = location;
            acc.username = username;

            if (operation == "add" && Accounts.ContainsKey(acc))
            {
                Logger.Write("Duplicate Account \"" + acc.username + "\".", "ERROR");
            }
            else if ((operation == "update" || operation == "delete") && !Accounts.ContainsKey(acc))
            {
                Logger.Write("Account \"" + acc.username + "\" doesn't exist / is invalid.", "ERROR");
            }
            else
            {
                switch (operation)
                {
                    case "add":
                        password = EncryptDecrypt.Encrypt(password);
                        Accounts.Add(acc, password);
                        Logger.Write("Added Account '" + acc.username + "'.");
                        break;
                    case "update":
                        password = EncryptDecrypt.Encrypt(password);
                        Accounts[acc] = password;
                        Logger.Write("Updated Password of Account '" + acc.username + "'.");
                        break;
                    case "delete":
                        Logger.Write("Deleted Account '" + acc.username + "'.");
                        Accounts.Remove(acc);
                        break;
                    default:
                        Logger.Write("Invalid operation was specified for ManageAccount in the operation parameter.", "ERROR");
                        break;
                }
            }
        }

        public static void ManageAccount(string operation, string location, string username)
        {
            ManageAccount(operation, location, username, "");
        }

        /* AddAccount: Adds a premade account to the dictionary.
         * Parameters: loc (location), user (username), pass (password)
         */
        public static void AddAccount(string loc, string user, string pass)
        {
            Accounts.Add(new Account { location = loc, username = user }, pass);
        }

        /* GetAccounts: Gets the contents of the Accounts Dictionary
         * Returns: Accounts Dictionary */
        public static Dictionary<Account, string> GetAccounts()
        {
            return Accounts;
        }
        /* SetAccounts: Sets account dictionary
         * Parameters: accs (Accounts Dictionary)
         */
        public static void SetAccounts(Dictionary<Account, string> accs)
        {
            Accounts = accs;
        }

        /* GetRow: Returns a specified row in the Accounts Dictionary, including the location, username, and password.
         * Parameters: row (row you want to access)
         * Returns: 3 string array containing the location, username, and password for that row. */
        public static string[] GetRow(int row)
        {
            var acc = Accounts.ElementAt(row);
            return new string[3] { acc.Key.location, acc.Key.username, acc.Value };
        }
    }
}
