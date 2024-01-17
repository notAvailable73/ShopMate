using System;
using System.IO;

namespace ShopMate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SHOPMATE";
            utility.Intro();
            utility.mainMenu();
            Console.ReadLine();

        }

    }
}

