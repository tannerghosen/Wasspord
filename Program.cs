using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wasspord
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
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
