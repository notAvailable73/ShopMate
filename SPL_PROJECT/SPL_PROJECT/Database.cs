using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace SPL_PROJECT
{
    public static class Database
    {
        public static List<user> userList = new List<user>();
        public static List<ElectronicProducts> ElectronicProductList = new List<ElectronicProducts>();
        public static List<Cloth> clothList = new List<Cloth>();
        public static List<HomeAppliences> HomeApplienceList = new List<HomeAppliences>();

        public static user CreateUser(string username, string name, string password, string mail, string date)
        {
            user newUser = new user(username, name, password, mail, date);
            string user_file = @"C:\ShopMate\user.txt.txt";
            string info = $"{username},{name},{password},{mail},{date}\n";
            File.AppendAllText(user_file, info);
            userList.Add(newUser);
            Console.WriteLine($"User Created Successfully with username:{username}");
            return newUser;
        }
        public static void addProduct(IAdder adder)
        {
            string name, description;
            double price = 0;

            Console.WriteLine("Enter Name of the product:");
            name = Console.ReadLine();

            Console.WriteLine("Enter Price of the product");
            try
            {
                price = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input.");
                addProduct(adder);
            }
            Console.WriteLine("Enter Description of the product");
            description = Console.ReadLine();
            adder.addProduct(name, price, description);

        }
        public static IProduct getProduct(int id)
        {
            foreach (ElectronicProducts item in ElectronicProductList)
            {
                if (item.id==id)
                {
                    return item;
                }
            }
            foreach (Cloth item in clothList)
            {
                if (item.id==id)
                {
                    return item;
                }
            }
            foreach (HomeAppliences item in HomeApplienceList)
            {
                if (item.id==id)
                {
                    return item;
                }
            }
            return null;
        }
        public static bool DoesUserExist(string username)
        {
            foreach (user Temp_user in Database.userList)
            {
                if (Temp_user.userName == username)
                {
                    return true;
                }
            }
            return false;
        }
        public static void createCart(string userName)
        {
            string path = $@"C:\ShopMate\Carts\{userName}_cart.txt";
            StreamWriter sw = File.CreateText(path);
            sw.Close();
        }
        public static void addProductToCart(string userName, IProduct product)
        {
            string path = $@"C:\ShopMate\Carts\{userName}_cart.txt";
            string info = product.id.ToString();
            File.AppendAllText(path, info);
        }
        public static void deleteProductFromCart(string userName, string productId)
        {
            string path = $@"C:\ShopMate\Carts\{userName}_cart.txt";
            StreamReader sr = new StreamReader(path);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (productId == line)
                {
                    line = "removed";
                }
            }
            sr.Close();

        }
        public static void clearCart(string userName)
        {
            string path = $@"C:\ShopMate\Carts\{userName}_cart.txt";
            File.WriteAllText(path, String.Empty);

        }
        public static Cart getCart(string userName)
        {
            string path = $@"C:\ShopMate\Carts\{userName}_cart.txt";
            if (!File.Exists(path))
            {
                createCart(userName);
            }
            Cart newCart = new Cart();
            StreamReader sr = new StreamReader(path);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                int id = int.Parse(line);
                IProduct product = getProduct(id);
                newCart.AddProductToCart(product);
            }

            sr.Close();
            return newCart;
        }
        public static void loadProducts()
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to browse Electronic Products");
            Console.WriteLine("Enter 2 to browse Cloth");
            Console.WriteLine("Enter 3 to browse Home Appliences");
        
            int inp = 0;

            try
            {
                inp= int.Parse(Console.ReadLine());
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            int input, index;
            switch (inp)
            {
                

                case 1:
                    Console.Clear();
                    Console.WriteLine("-------------------------Electronic Products--------------------------");
                    Console.WriteLine();
                    foreach (ElectronicProducts Item in ElectronicProductList)
                    {
                        Console.WriteLine(Item.id + " " + Item.name);
                    }

                    Console.WriteLine("Enter Product Id To See Details");

                     input = int.Parse(Console.ReadLine());
                    index = input - 10001;
                    Console.Clear();
                    Console.WriteLine("------------------Product Details------------------");
                    if (ElectronicProductList[index] !=null )
                    {
                        Console.WriteLine("Name: " + ElectronicProductList[index].name);
                        Console.WriteLine("Price: " + ElectronicProductList[index].price);
                        Console.WriteLine("Description: " + ElectronicProductList[index].description);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
            break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("-------------------------Clothing Products--------------------------");
                    Console.WriteLine();
                    foreach (Cloth Item in clothList)
                    {
                        Console.WriteLine(Item.id + " " + Item.name);
                    }
                    Console.WriteLine("Enter Product Id To See Details");

                    input = int.Parse(Console.ReadLine());
                    index = input - 20001;
                    Console.Clear();
                    Console.WriteLine("------------------Product Details------------------");
                    if (clothList[index] != null)
                    {
                        Console.WriteLine("Name: " + clothList[index].name);
                        Console.WriteLine("Price: " + clothList[index].price);
                        Console.WriteLine("Description: " + clothList[index].description);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("-------------------------Home Appliences--------------------------");
                    Console.WriteLine();
                    foreach (HomeAppliences Item in HomeApplienceList)
                    {
                        Console.WriteLine(Item.id + " " + Item.name);
                    }
                    Console.WriteLine("Enter Product Id To See Details");

                    input = int.Parse(Console.ReadLine());
                    index = input - 30001;
                    Console.Clear();
                    Console.WriteLine("------------------Product Details------------------");
                    if (HomeApplienceList[index] != null)
                    {
                        Console.WriteLine("Name: " + HomeApplienceList[index].name);
                        Console.WriteLine("Price: " + HomeApplienceList[index].price);
                        Console.WriteLine("Description: " + HomeApplienceList[index].description);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                    break;

            }
            Console.WriteLine("Press 1 to go to MainMenu");

            try
            {
                inp = int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

          /*  switch(inp)
            {
                case 1:
                    
            }*/


        }

    }
}
