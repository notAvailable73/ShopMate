using System;
using System.Collections.Generic;
using System.IO;

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


    }
}
