using System;

namespace SPL_PROJECT
{
    public class Admin : IAccount
    {

        public void AddProduct()
        {
            IAdder productAdder;
            Console.WriteLine("Enter 1 to add Electronic Product");
            Console.WriteLine("Enter 2 to add Cloth Product");
            Console.WriteLine("Enter 3 to add HomeAppliences");
            Console.WriteLine("Enter 4 to return to menu");

            int input = 0;
            try
            {
                input = int.Parse(Console.ReadLine());

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input.");
                AddProduct();
                return;
            }
            switch (input)
            {
                case 1:
                    productAdder = new ElectronicProductsAdder();
                    Database.addProduct(productAdder);
                    Console.Clear();
                    Console.WriteLine("--------------Product Added Successfully-------------");
                    Console.WriteLine();
                    dashboard();
                    break;
                case 2:
                    productAdder = new ClothingProductsAdder();
                    Database.addProduct(productAdder);
                    Console.Clear();
                    Console.WriteLine("--------------Product Added Successfully-------------");
                    Console.WriteLine();
                    dashboard();
                    break;
                case 3:
                    productAdder = new HomeAppliencesAdder();
                    Database.addProduct(productAdder);
                    Console.Clear();
                    Console.WriteLine("--------------Product Added Successfully-------------");
                    Console.WriteLine();
                    dashboard();
                    break;

                case 4:
                    dashboard();
                    return;
                default:
                    Console.WriteLine("Invalid Input");
                    AddProduct();
                    return;
            }
        }

        public void dashboard()
        {
           
            Console.WriteLine("Enter 1 to add product");
            Console.WriteLine("Enter 2 to Log out");

            int input = 0;

            try
            {
                input = int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            switch (input)
            {
                case 1:
                    Console.Clear(); AddProduct(); break;
                case 2:
                    Console.Clear(); logOut(); break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }

        public void logOut()
        {
            Console.WriteLine("logged out successfully");
            Console.WriteLine();
            utility.mainMenu();
            return;
        }

    }
}
