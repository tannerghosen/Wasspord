using System.Collections.Generic;
using System.Linq;

namespace Wasspord
{
    /*
     * Methods: ManageAccount, AddAccount, GetAccounts, SetAccounts, GetRow
     * Properties/Misc: Account Dictionary, Account Struct
     */
    /// <summary>
    /// This class handles everything to do with adding, updating, removing, and getting Accounts from individual .wasspord files.
    /// </summary>
    public static class WasspordAccounts // mutable class
    {
        /// <summary>
        /// A dictionary with a key made of 2 parts (location, username)  that contains information on 
        /// where the account is used, the username and the password. The 2 parts allow flexibility and to have multiple accounts
        /// under the same username/email at multiple websites.
        /// </summary>
        public static Dictionary<Account, string> Accounts = new Dictionary<Account, string>();

        /// <summary>
        /// This is our key when we insert entries into the Account dictionary,
        /// which is also the same key used to find entries in other functions. In the struct, 
        /// location is where the account is used, username is the username/email used.
        /// </summary>
        public struct Account
        {
            public string location;
            public string username;
        }

        /// <summary>
        /// ManageAccount: Adds, updates, and deletes accounts to/from the account dictionary.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="location"></param>
        /// <param name="username"></param>
        /// <param name="password">optional</param>
        public static void ManageAccount(string operation, string location, string username, string password) 
        {
            Account acc = new Account { location = Encryption.Encrypt(location), username = Encryption.Encrypt(username) };

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
                        password = Encryption.Encrypt(password);
                        Accounts.Add(acc, password);
                        Logger.Write("Added Account '" + acc.username + "'.");
                        break;
                    case "update":
                        password = Encryption.Encrypt(password);
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

        /// <summary>
        /// Adds a premade account to the dictionary.
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        public static void AddAccount(string loc, string user, string pass) 
        {
            if (!Encryption.Validate(loc) || !Encryption.Validate(user)) // if the file is older, it won't have encrypted names / locations
            {
                // however now it will
                Accounts.Add(new Account { location = Encryption.Encrypt(loc), username = Encryption.Encrypt(user) }, pass);
            }
            else
            {
                Accounts.Add(new Account { location = loc, username = user }, pass);
            }
        }

        /// <summary>
        /// Gets the contents of the Accounts Dictionary
        /// </summary>
        /// <returns>Accounts Dictionary</returns>
        public static Dictionary<Account, string> GetAccounts()
        {
            return Accounts;
        }

        /// <summary>
        /// Sets Accounts dictionary
        /// </summary>
        /// <param name="accs"></param>
        public static void SetAccounts(Dictionary<Account, string> accs)
        {
            Accounts = accs;
        }

        /// <summary>
        /// Returns a specified row in the Accounts Dictionary, including the location, username, and password.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>3 string array containing the location, username, and password for that row</returns>
        public static string[] GetRow(int row)
        {
            var acc = Accounts.ElementAt(row);
            return new string[3] { acc.Key.location, acc.Key.username, acc.Value };
        }

        /// <summary>
        /// Clears our Accounts Dictionary
        /// </summary>
        public static void ClearAccounts()
        {
            SetAccounts(new Dictionary<Account, string>()); // This prevents a null reference error by giving it a value instead of letting it be initialized as null on the Load method being used.
        }
    }
}
