using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public interface IProduct
    {
         int id { get; set; }
         string name { get; set; }
         double price { get; set; }
         string description { get; set; }
    }

    public class ElectronicProducts:IProduct
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
    }

    public class Cloth: IProduct
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
    }

    public class HomeAppliences: IProduct
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
    }
}
