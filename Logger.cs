using System;
using System.Diagnostics;
using System.IO;

namespace Wasspord
{
    /*
     * Methods: Write
     * Properties/Misc: Log
     */
    /// <summary>
    /// This class handles logging for various classes throughout the program.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Our log file is located in the program's folder
        /// </summary>
        private static string Log = $"./Wasspord.log";
        /// <summary>
        /// Our log file if we have 'logs per session' enabled
        /// </summary>
        private static string PerSessionLogName { get; set; }
        /// <summary>
        /// Writes a message to our Wasspord.log, usually important info such as errors, warnings, or debug info I'd appreciate if an issue arises.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messagetype"></param>
        /// 
        public static void Write(string message, string messagetype = "LOG") 
        {
            if (WasspordSettings.LoggerSetting != 0)
            {
                string[] flavortexts = { "The problem probably lies in", "The stack is as follows:" };
                string Time = DateTime.Now.ToString("M/d/yyyy h:mm:ss tt");
                StackTrace st = new StackTrace(); // Create a stack trace
                StackFrame parentsf = st.GetFrame(1); // this is the parent of the method call
                StackFrame grandparentsf = st.GetFrame(2); // this is the grandparent (parent's parent) of the method call
                using (StreamWriter writer = new StreamWriter(Log, true))
                {
                    writer.WriteLine("(" + Time + ") [" + messagetype + "]: " + message);
                    if (messagetype == "ERROR" || messagetype == "DEBUG") // if error, let's help out by giving the stack trace
                    {
                        string stack = grandparentsf != null ? grandparentsf.GetMethod().Name + " -> " + parentsf.GetMethod().Name : parentsf.GetMethod().Name; // this is a string that says Grandparent -> Parent
                        writer.WriteLine("(" + Time + ") [" + messagetype + "]: " + messagetype == "ERROR" ? flavortexts[0] : flavortexts[1] + " " + stack + ".");
                    }
                    writer.Close();
                }
            }
        }

        public static void Update()
        {
            switch (WasspordSettings.LoggerSetting)
            {
                case 1:
                    Log = "./Wasspord.log";
                    break;
                case 2:
                    if (PerSessionLogName == null)
                    {
                        PerSessionLogName = $"./Wasspord {DateTime.Now.ToString("M-d-yyyy h-mm-ss tt")}.log";
                    }
                    Log = PerSessionLogName;
                    break;
                case 0:
                default:
                    break;
            }
        }
    }
}
