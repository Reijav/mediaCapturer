using System;
using System.IO;
using System.Linq;

namespace ConsoleObtenerCadenaInstalador
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese carpeta:");
            var carpeta= Console.ReadLine();

            DirectoryInfo di = new DirectoryInfo(carpeta);

            var files = di.GetFiles();
            int i = 0;
            foreach ( var file in  files)
            {

               var cadena= $"<Component Id=\"{EliminarCaracteresEspeciales(file.Name)}\">\n<File Id =\"{EliminarCaracteresEspeciales(file.Name)}\" Source = \"$(var.CameraCapturer.TargetDir){file.Name}\" KeyPath = \"yes\" Checksum = \"yes\" /> \n</Component>";

                Console.WriteLine(cadena);
                i++;
            }


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }


        private static string  EliminarCaracteresEspeciales(string cadena)
        {
            char[] caracteres = {'-','/', '*', '\\', '(', ')', '&', '^', '%', '$', '#','@','!', '`'};
            char[] nuevoArregloCadena = new char[cadena.Length];

            string cadenaNueva="";
            foreach( var c in cadena.ToCharArray())
            {
                if (!caracteres.Contains(c))
                {
                    cadenaNueva += c.ToString();
                }
            }
            return cadenaNueva;
        }
    }
}
