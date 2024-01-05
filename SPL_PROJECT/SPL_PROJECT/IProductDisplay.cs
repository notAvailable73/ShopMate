using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;

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

            List<string> products = new List<string>();

            foreach(IProduct item in Database.ElectronicProductList) 
            {
               products.Add(item.name);
            }

            products.Add("GO Back");

            string[] ElectronicProducts = products.ToArray();

            Menu menu = new Menu(ElectronicProducts);

            int index = menu.Run();

            if (index == products.Count - 1)
            {
                Database.browseProduct();
            }
            else
            {
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
                        string s = "Product Added To Cart Successfully";
                        string[] options1 = { "GO Back" };

                        Menu menu2 = new Menu(options1);
                        int inp1 = menu2.Run(s);

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
    public class ClothingProductDisplay : IProductDisplay
    {
        public void DisplayProducts()
        {
            Console.WriteLine($"-------------------Clothing Products---------------------");
            Console.WriteLine();

            List<string> products = new List<string>();

            foreach(IProduct item in Database.clothList)
            {
                products.Add(item.name);
            }

            products.Add("GO Back");
            string[] clothingProducts=products.ToArray();

            Menu menu = new Menu(clothingProducts);


            int index = menu.Run();

            if (index == products.Count - 1)
            {
                Database.browseProduct();
            }
            else
            {
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
                        string s = "Product Added To Cart Successfully";
                        string[] options1 = { "GO Back" };

                        Menu menu2 = new Menu(options1);
                        int inp1 = menu2.Run(s);

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
    public class HomeApplienceProductDisplay : IProductDisplay
    {
        public void DisplayProducts()
        {
            Console.WriteLine($"-------------------Home Applience Product---------------------");
            Console.WriteLine();

            List<string> products=new List<string>();

            foreach (IProduct item in Database.HomeApplienceList)
            {
                products.Add(item.name);
            }

            products.Add("GO Back");

            string[] HomeAppliencesProduct = products.ToArray();

            Menu menu=new Menu(HomeAppliencesProduct);


            int index = menu.Run();

            if (index == products.Count - 1)
            {
                Database.browseProduct();
            }
            else
            {
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
                        string s = "Product Added To Cart Successfully";
                        string[] options1 = { "GO Back" };

                        Menu menu2 = new Menu(options1);
                        int inp1=menu2.Run(s);
          
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

}
