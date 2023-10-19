using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public static class Database
    {
        public static List<user> userList = new List<user>();
        public static List<ElectronicProducts> ElectronicProducts = new List<ElectronicProducts>();
        public static List<Cloth> cloths = new List<Cloth>();
        public static List<HomeAppliences> HomeAppliences = new List<HomeAppliences>();
        public static user CreateUser(string username,string name,string password,string mail, DateTime date)
        {
            user newUser=new user(username,name, password, mail, date);
            string user_file = @"C:\ShopMate\user.txt.txt";
            string info = $"{username},{name},{password},{mail},{date}\n";
            File.AppendAllText(user_file, info);
            userList.Add(newUser);
            Console.WriteLine($"User Created Successfully with username:{username}");
            return newUser;
        }

        public static void CreateElectronicProduct(int id,string name,double price,string description) 
        {
            ElectronicProducts ep=new ElectronicProducts(id,name,price,description);
            string productfile = @"C:\ShopMate\electronicproduct.txt";
            string info = $"{id},{name},{price},{description}\n";
            File.AppendAllText(productfile, info);
            ElectronicProducts.Add(ep);
            Console.WriteLine($"Product added Successfully.");
        }
        public static void CreatClothingeProduct(int id,string name,double price,string description) 
        {
            Cloth cloth=new Cloth(id,name,price,description);
            string productfile = @"C:\ShopMate\clothingproduct.txt";
            string info = $"{id},{name},{price},{description}\n";
            File.AppendAllText(productfile, info);
            cloths.Add(cloth);
            Console.WriteLine($"Product added Successfully.");
        }
        public static void CreateHomeAppliences(int id,string name,double price,string description) 
        {
            HomeAppliences ha= new HomeAppliences(id,name,price,description);
            string productfile = @"C:\ShopMate\homeapplienceproduct.txt";
            string info = $"{id},{name},{price},{description}\n";
            File.AppendAllText(productfile, info);
            HomeAppliences.Add(ha);
            Console.WriteLine($"Product added Successfully.");
        }
    }
}
