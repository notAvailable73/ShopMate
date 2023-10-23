using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace SPL_PROJECT
{
    public static class Database
    {
        public static List<user> userList = new List<user>();
        public static List<ElectronicProducts> ElectronicProductList = new List<ElectronicProducts>();
        public static List<Cloth> clothList = new List<Cloth>();
        public static List<HomeAppliences> HomeApplienceList = new List<HomeAppliences>();

        public static user CreateUser(string username, string name, string password, string mail, DateTime date)
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

    }
}
