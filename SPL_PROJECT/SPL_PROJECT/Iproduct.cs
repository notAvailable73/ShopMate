using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SPL_PROJECT
{
    public interface IProduct
    {
        int id { get; set; }
        string name { get; set; }
        double price { get; set; }
        string description { get; set; }
        void DisplayDetails();
    }

    public class ElectronicProducts : IProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public ElectronicProducts(int id, string name, double price, string description)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine();
        }
    }

    public class Cloth : IProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public Cloth(int id, string name, double price, string description)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
        }
        public void DisplayDetails()
        {
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine();
        }
    }

    public class HomeAppliences : IProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public HomeAppliences(int id, string name, double price, string description)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
        }
        public void DisplayDetails()
        {
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine();
        }
    }
}
