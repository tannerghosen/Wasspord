using System;
using System.Windows.Forms;

namespace Wasspord
{
    internal static class Program
    {
        /// <summary>
        /// Program class that calls for the initilization of the Wasspord class and Wasspord GUI class.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logger.Write("Launching Wasspord!");
            Wasspord.Init();
            Logger.Write("Starting Wasspord GUI!");
            Application.Run(new WasspordGUI());
        }
    }
}
