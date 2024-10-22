using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Wasspord;
using System;

namespace WasspordTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestSave()
        {
            string filename = "testcase.wasspord";
            try
            {
                Wasspord.WasspordFilesHandler.Save("", filename);
            }
            catch
            {
                Assert.Fail("Save method had an exception. Caught in Unit Tests.");
            }
        }

        [TestMethod]
        public void TestLoad()
        {
            string filename = "testcase.wasspord";
            if (!File.Exists(filename))
            {
                File.Create(filename).Dispose();
            }
            try
            {
                Wasspord.WasspordFilesHandler.Load("", filename);
            }
            catch
            {
                Assert.Fail("Load method had an exception. Caught in Unit Tests.");
            }
        }

        [TestMethod]
        public void TestDecryptValidBase64String()
        {
            try
            {
                Encryption.Decrypt("4OWaPe1CCzJuBaA+11uaWQ==");
            }
            catch
            {
                Assert.Fail("Failed to Decrypt Valid Base64 string");
            }
        }

        [TestMethod]
        public void TestDecryptInvalidBase64String()
        {
            string test = Encryption.Decrypt("test55551231@@##");
            if (test != "error")
            {
                Assert.Fail("Failed to handle invalid Base64 string");
            }
        }

        [TestMethod]
        public void TestAccountCreateAndUpdate()
        {
            try
            {
                WasspordAccounts.ManageAccount("add", "test", "admin", "root");
                if (!WasspordAccounts.GetAccounts().ContainsKey(new WasspordAccounts.Account { location = Encryption.Encrypt("test"), username = Encryption.Encrypt("admin") }))
                {
                    Assert.Fail("Failed to make an account");
                }
                WasspordAccounts.ManageAccount("update", "test", "admin", "boot");
                if (!WasspordAccounts.GetAccounts().ContainsKey(new WasspordAccounts.Account { location = Encryption.Encrypt("test"), username = Encryption.Encrypt("admin") }))
                {
                    Assert.Fail("Failed to update account because it doesn't exist");
                }
            }
            catch
            {
                Assert.Fail("Failed to either create an account or update an account, likely methods don't work anymore");
            }
        }

        [TestMethod]
        public void TestAccountDelete()
        {
            try
            {
                WasspordAccounts.ManageAccount("delete", "test", "admin");
                if (WasspordAccounts.GetAccounts().ContainsKey(new WasspordAccounts.Account { location = "test", username = "admin" }))
                {
                    Assert.Fail("Failed to delete account");
                }
            }
            catch
            {
                Assert.Fail("Failed to delete account, method doesn't likely work anymore");
            }
        }
        [TestMethod]
        public void TestValidate()
        {
            bool test1 = Encryption.Validate("4OWaPe1CCzJuBaA+11uaWQ==");
            if (test1 != true)
            {
                Assert.Fail("Validate Method Test 1 failed");
            }
            bool test2 = Encryption.Validate("test55551231@@##");
            if (test2 != false)
            {
                Assert.Fail("Validate Method Test 2 failed");
            }
        }

        [TestMethod]
        public void TestEncryption()
        {
            Encryption.SetKey("wYAberIJyVjTYQxawGL1XQ==");
            string encrypt = Encryption.Encrypt("5");
            if (encrypt != "80De3FurcgXG4yZqLWBKXA==")
            {
                Assert.Fail("Failed to encrypt, only reason this mismatch would happen is if the Key / Encrypt / Decrypt methods were changed significantly.");
            }
        }

        [TestMethod]
        public void TestReset()
        {
            Wasspord.Wasspord.Reset();
            Wasspord.WasspordFilesHandler.SetWasspordPassword("test");
            WasspordAccounts.ManageAccount("add", "test", "admin", "root");
            string key = Encryption.GetKey();
            Wasspord.WasspordFilesHandler.Save("", "test");
            Wasspord.Wasspord.Reset();
            if (WasspordAccounts.GetAccounts().Count() != new Dictionary<WasspordAccounts.Account, string>().Count())
            {
                Assert.Fail("Reset did not properly work. (Accounts Dictionary)");
            }
            if (key == Encryption.GetKey())
            {
                Assert.Fail("Reset did not properly work. (Key)");
            }
            if (Wasspord.WasspordFilesHandler.GetWasspordPassword() == "test")
            {
                Assert.Fail("Reset did not properly work. (WasspordPassword)");
            }
            if (Wasspord.WasspordFilesHandler.Filename == "test")
            {
                Assert.Fail("Reset did not properly work. (Filename)");
            }
        }
    }
}
