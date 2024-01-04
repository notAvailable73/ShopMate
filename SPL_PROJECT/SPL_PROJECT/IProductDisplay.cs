using System;
using System.Linq;

namespace SPL_PROJECT
{
    public interface IProductDisplay
    {
        void DisplayProducts();
    }

    public class ElectronicProductDisplay : IProductDisplay
    {
        public void DisplayProducts()
        {
            Console.WriteLine($"-------------------Electronic Products---------------------");
            Console.WriteLine();

            foreach (IProduct item in Database.ElectronicProductList)
            {
                Console.WriteLine($"{item.id} {item.name}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter Product Id To See Details");

            int input = int.Parse(Console.ReadLine());
            int index = input - (Database.ElectronicProductList.First().id);
            Console.Clear();
            Console.WriteLine("---------------------Product Details------------------------");
            Console.WriteLine();

            try
            {
                if (Database.ElectronicProductList[index] == null)
                {
                    throw new Exception();
                }
                IProduct product = Database.ElectronicProductList[index];
                product.DisplayDetails();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                DisplayProducts();
            }
            string[] options = { "Add to Cart", "Go Back", "Dashboard" };

            Menu menu1 = new Menu(options);

            int inp = menu1.Run(Database.ElectronicProductList[index]);


            switch (inp)
            {
                case 0:
                    Console.Clear();
                    Database.addProductToCart(Database.ElectronicProductList[index]);
                    Console.WriteLine("Product Added To Cart Successfully");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to go back.");
                    Console.ReadKey();
                    DisplayProducts();
                    return;
                case 1:
                    Console.Clear();
                    DisplayProducts();
                     
                    return;
                case 2:
                    Console.Clear();
                    Session.CurrentUser.dashboard();
                    return;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }
        }
    }
    public class ClothingProductDisplay : IProductDisplay
    {
        public void DisplayProducts()
        {
            Console.WriteLine($"-------------------Clothing Products---------------------");
            Console.WriteLine();

            foreach (IProduct item in Database.clothList)
            {
                Console.WriteLine($"{item.id} {item.name}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter Product Id To See Details");

            int input = int.Parse(Console.ReadLine());
            int index = input - (Database.clothList.First().id);
            Console.Clear();
            Console.WriteLine("---------------------Product Details------------------------");
            Console.WriteLine();

            try
            {
                if (Database.clothList[index] != null)
                {
                    IProduct product = Database.clothList[index];
                    product.DisplayDetails();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                DisplayProducts();
            }
            string[] options = { "Add to Cart", "Go Back", "Dashboard" };

            Menu menu1 = new Menu(options);

            int inp = menu1.Run(Database.clothList[index]);


            switch (inp)
            {
                case 0:
                    Console.Clear();
                    Database.addProductToCart(Database.clothList[index]);
                    Console.WriteLine("Product Added To Cart Successfully");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to go back.");
                    Console.ReadKey();
                    DisplayProducts();
                    return;
                case 1:
                    Console.Clear();
                    DisplayProducts();

                    return;
                case 2:
                    Console.Clear();
                    Session.CurrentUser.dashboard();

                    return;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }
        }
    }
    public class HomeApplienceProductDisplay : IProductDisplay
    {
        public void DisplayProducts()
        {
            Console.WriteLine($"-------------------Home Applience Product---------------------");
            Console.WriteLine();

            foreach (IProduct item in Database.HomeApplienceList)
            {
                Console.WriteLine($"{item.id} {item.name}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter Product Id To See Details");

            int input = int.Parse(Console.ReadLine());
            int index = input - (Database.HomeApplienceList.First().id);
            Console.Clear();
            Console.WriteLine("---------------------Product Details------------------------");
            Console.WriteLine();

            try
            {
                if (Database.HomeApplienceList[index] != null)
                {
                    IProduct product = Database.HomeApplienceList[index];
                    product.DisplayDetails();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                DisplayProducts();
            }
            string[] options = { "Add to Cart", "Go Back", "Dashboard" };
            Menu menu1 = new Menu(options);

            int inp = menu1.Run(Database.HomeApplienceList[index]);


            switch (inp)
            {
                case 0:
                    Console.Clear();
                    Database.addProductToCart(Database.HomeApplienceList[index]);
                    Console.WriteLine("Product Added To Cart Successfully");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to go back.");
                    Console.ReadKey();
                    DisplayProducts();
                    return;
                case 1:
                    Console.Clear();
                    DisplayProducts();
                    return;
                case 2:
                    Console.Clear();
                    Session.CurrentUser.dashboard();
                    return;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }
        }
    }

}
