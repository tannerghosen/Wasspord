using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.IO;
using System.Security.Cryptography;

namespace Wasspord
{
    /// <summary> 
    /// Wasspord.cs
    /// Methods: AddAccount, UpdatePassword, DeleteAccount, Save, Load, Encrypt, Decrypt
    /// Purpose of File: Brains of the program, handles adding/modifying/delete accounts, saving/loading them
    /// and ensuring passwords are encrypted.
    /// </summary>
    using System.Collections;
    using System.IO.Pipes;

    public static class Wasspord
    {
        private static Dictionary<Account, string> Accounts = new Dictionary<Account, string>();
        private struct Account
        {
            public string where;
            public string username;
        }
        public static void AddAccount(string where, string username, string password)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (Accounts.ContainsKey(acc))
            {
                Console.Error.WriteLine("Duplicate Account");
            }
            else
            {
                password = Encrypt(password);
                Accounts.Add(acc, password);
            }
        }

        public static void UpdatePassword(string where, string username, string password)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (!Accounts.ContainsKey(acc))
            {
                Console.Error.WriteLine("Account doesn't exist / is invalid");
            }
            else
            {
                password = Encrypt(password);
                Accounts[acc] = password;
            }
        }
        public static void DeleteAccount(string where, string username)
        {
            Account acc;
            acc.where = where;
            acc.username = username;
            if (!Accounts.ContainsKey(acc))
            {
                Console.Error.WriteLine("Duplicate Account");
            }
            else
            {
                Accounts.Remove(acc);
            }
        }
        public static void Save(string location, string filename)
        {
            string file = location + @"\" + filename;
            // This try statement is in case someone tries to save with no file open, causing an exception.
            try
            {
                if (!File.Exists(file))
                {
                    // This should really never happen, but if it does let's not cause an error
                    File.Create(file).Dispose();
                }
                using (StreamWriter sw = new StreamWriter(file))
                {
                    foreach (var pair in Accounts)
                    {
                        sw.WriteLine(pair.Key.where + "|" + pair.Key.username + "|" + pair.Value);
                    }
                }
            }
            catch
            {
                Console.Error.WriteLine("Error saving to file");
            }
        }

        public static void Load(string location, string filename)
        {
            string file = location + @"\" + filename;
            if (!File.Exists(file))
            {
                File.Create(file).Dispose();
            }
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
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
        }

        public static string Display()
        {
            string display = "";
            foreach (var pair in Accounts)
            {
                display +=
                "Account Location: " + pair.Key.where
                + " | Account Username: " + pair.Key.username
                + " | Account Password: " + Decrypt(pair.Value) + "\r\n";
            }
            return display;
        }

        private static string Encrypt(string password)
        {
           byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        private static string Decrypt(string password)
        {
            byte[] b;
            string decrypted;
            b = Convert.FromBase64String(password);
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return decrypted; 
        }
    }
}
