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
            //utility.mainMenu();
            user ask = new user("ria", "ra", "123", "asd", DateTime.Today);
            ElectronicProducts ep = new ElectronicProducts(1001, "a", 1234, "a");
            ask.addToCart(ep);
            ask.cart.showCart();
            Console.ReadLine();
        }
    }
}
