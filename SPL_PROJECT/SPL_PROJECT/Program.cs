using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    internal class Program
    {
        static void Main(string[] args)
        {
 
            Starter starter = new Starter();

            utility.mainMenu();
           /* CustomerLogIn logIn = new CustomerLogIn();
            logIn.logIn("Mainul", "1234");*/
            Console.ReadLine();
        }
    }
}
