using System;
using System.IO;

namespace Wasspord
{
    /*
     * Methods: Write
     * Properties/Misc: Log
     */
    public static class Logger
    {
        /* Our log file is located in the program's folder */
        public static string Log = "./Wasspord.log";
        /* Write: Writes a message to our Wasspord.log, usually important info such as errors, warnings, or debug info I'd appreciate if an issue arises. */
        public static void Write(string message, string messagetype = "LOG")
        {
            string Time = DateTime.Now.ToString("M/d/yyyy h:mm:ss tt");
            using (StreamWriter writer = new StreamWriter(Log, true))
            {
                writer.WriteLine("(" + Time + ") [" + messagetype + "]: " + message);
                writer.Close();
            }
        }
    }
}
