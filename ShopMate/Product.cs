using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate
{
    public class Product
    {
        public string id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; } 
        public int quantiTy { get; set; }
        public Product(string name, double price, string description)
        {
            
            this.name = name;
            this.price = price;
            this.description = description;
        }
        public Product(string name, double price,int qntity, string description)
        {

            this.name = name;
            this.price = price;
            this.description = description;
            this.quantiTy = qntity;
        }
        public Product(string id,string name, double price, int qntity, string description)
        {
            this.id = id;

            this.name = name;
            this.price = price;
            this.description = description;
            this.quantiTy = qntity;
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
