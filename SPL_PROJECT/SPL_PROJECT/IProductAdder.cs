using System;
using System.IO;

namespace SPL_PROJECT
{
    public interface IProductAdder
    {
        void addProduct(string name, double price, string description, int quantity);
    }
    public class ElectronicProductsAdder : IProductAdder
    {
        public void addProduct(string name, double price, string description, int quantity)
        {
            int id = (Database.ElectronicProductList.Count + 1)  + 10000;
            ElectronicProducts product = new ElectronicProducts(id, name, price, description, quantity);
            string productfile = @"C:\ShopMate\electronicproduct.txt";
            string info = $"{product.id},{product.name},{product.price},{product.quantity},{product.description}\n";
            File.AppendAllText(productfile, info);
            Database.ElectronicProductList.Add(product);
            Console.WriteLine($"Product added Successfully.");
        }
    }
    public class ClothingProductsAdder : IProductAdder
    {
        public void addProduct(string name, double price, string description, int quantity)
        {
            int id = (Database.clothList.Count + 1)  + 20000;

            string productfile = @"C:\ShopMate\clothingproduct.txt";
            Cloth product = new Cloth(id, name, price, description, quantity);
            string info = $"{product.id},{product.name},{product.price},{product.quantity},{product.description}\n";
            File.AppendAllText(productfile, info);
            Database.clothList.Add(product);
            Console.WriteLine($"Product added Successfully.");
        }
    }
    public class HomeAppliencesAdder : IProductAdder
    {
        public void addProduct(string name, double price, string description, int quantity)
        {
            int id = (Database.HomeApplienceList.Count + 1)  + 30000;

            HomeAppliences product = new HomeAppliences(id, name, price, description, quantity);
            string productfile = @"C:\ShopMate\homeapplienceproduct.txt";
            string info = $"{product.id},{product.name},{product.price},{product.quantity},{product.description}\n";
            File.AppendAllText(productfile, info);
            Database.HomeApplienceList.Add(product);
            Console.WriteLine($"Product added Successfully.");
        }
    }
}
