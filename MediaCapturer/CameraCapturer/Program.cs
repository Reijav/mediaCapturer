using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraCapturer
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Count() > 0)
            {

                Application.Run(new FormCapturador(args[0]));
            }
            else
            {
                Application.Run(new FormUsandoControlLib());
            }
        }
    }
}
