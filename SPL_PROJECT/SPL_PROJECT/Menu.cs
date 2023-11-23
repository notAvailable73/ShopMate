using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public class Menu
    {
        private int selectedIndex;
        private string[] options;

        public Menu(string[] options)
        {
            this.selectedIndex = 0;
            this.options = options;
        }

        private void Displayoptions()
        {
            for(int i=0;i<options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;

                if(i==selectedIndex) 
                {
                    prefix = "-->";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{prefix} << {currentOption} >>");
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey keypressed;
            do
            {
                Console.Clear();
                Displayoptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keypressed = keyInfo.Key;

                if(keypressed==ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if(selectedIndex ==-1)
                    {
                        selectedIndex = options.Length-1;
                    }
                }

                else if(keypressed==ConsoleKey.DownArrow)
                { 
                    selectedIndex++; 
                    if(selectedIndex == options.Length)
                    {
                        selectedIndex = 0;
                    }
                }

            } while (keypressed != ConsoleKey.Enter);

            return selectedIndex;
        }

        public int Run(IProduct product)
        {
            ConsoleKey keypressed;
            do
            {
                Console.Clear();
                Console.WriteLine("Name: " +product.name);
                Console.WriteLine("Price: " + product.price);
                Console.WriteLine("Description: " + product.description);
                Console.WriteLine();
                Displayoptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keypressed = keyInfo.Key;

                if (keypressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = options.Length - 1;
                    }
                }

                else if (keypressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == options.Length)
                    {
                        selectedIndex = 0;
                    }
                }

            } while (keypressed != ConsoleKey.Enter);

            return selectedIndex;
        }
    }
}
