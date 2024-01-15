using System;
using System.IO;

namespace ShopMate
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Get the base directory of the application (output directory)
            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //Console.WriteLine($"Base Directory: {baseDirectory}");
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string admin_file = Path.Combine(baseDirectory, "Database\\userInfos\\adminPass.txt");
            //StreamReader sr = new StreamReader(admin_file);
            //string line;
            //line = sr.ReadLine();
            //sr.Close();
            //Console.WriteLine(line);
            //Console.ReadKey(); 
            utility.mainMenu();
            Console.ReadLine();
            //string path = Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt");

            //string[] lines = File.ReadAllLines(path);
            //Console.WriteLine(File.ReadAllLines(path).Length);

        }

    }
}

