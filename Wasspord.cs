using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.IO;

namespace Wasspord
{
    using System.Collections;
    using System.IO.Pipes;

    public static class Wasspord
    {
        private static Dictionary<Account,string> Accounts = new Dictionary<Account,string>();
        private struct Account
        {
            public string where;
            public string username;
        }
        public static string AddAccount(string where, string username, string password)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (Accounts.ContainsKey(acc))
            {
                return "Duplicate Account";
            }
            else
            {
                Accounts.Add(acc, password);
                return "Account '" + username + "' for '"+where+"' Added.";
            }
        }

        public static string UpdatePassword(string where, string username, string password)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (!Accounts.ContainsKey(acc))
            {
                return "Invalid Account";
            }
            else
            {
                Accounts[acc] = password;
                return "Updated Password for " + username + " for '" + where + "'.";
            }
        }
        public static string DeleteAccount(string where, string username)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (!Accounts.ContainsKey(acc))
            {
                return "Invalid Account";
            }
            else
            {
                Accounts.Remove(acc);
                return "Removed " + username + " for '" + where + "'.";
            }
        }
        public static string Save()
        {
            if (!File.Exists("Accounts.txt"))
            {
                // This should really never happen, but if it does let's not cause an error
                File.Create("Accounts.txt").Dispose();
            }
            using (StreamWriter sw = new StreamWriter("Accounts.txt"))
            {
                foreach (var pair in Accounts)
                {
                    sw.WriteLine(pair.Key.where + "|" + pair.Key.username + "|" + pair.Value);
                }
            }
            return "Saved Username/Password List";
        }

        public static string Load()
        {
            if (!File.Exists("Accounts.txt"))
            {
                File.Create("Accounts.txt").Dispose();
            }
            var fileStream = new FileStream("Accounts.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var spl = line.Split('|');
                    // below is in case we have duplicate keys, which can happen if you try to load again.
                    if (!Accounts.ContainsKey(new Account { where = spl[0], username = spl[1] }))
                    {
                        Accounts.Add(new Account { where = spl[0], username = spl[1] }, spl[2]);
                    }
                }
            }
            return "Loaded Username/Password List";

        }

        public static string Display()
        {
            string display = "";
            foreach (var pair in Accounts)
            {
                display += 
                "Account Location: " + pair.Key.where 
                + " | Account Username: " + pair.Key.username 
                + " | Account Password: " + pair.Value + "\r\n";
            }
            return display;
        }
    }
}
