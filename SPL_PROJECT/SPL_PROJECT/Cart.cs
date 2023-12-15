using System;
using System.Collections.Generic;

namespace SPL_PROJECT
{
    public class Cart
    {
        List<IProduct> products = new List<IProduct>();
        public void AddProductToCart(string userName, IProduct product)
        {
            products.Add(product);
            Database.addProductToCart(userName, product);
        }
        public void AddProductToCart(IProduct product)
        {
            products.Add(product);
        }
        public double calculatePrice()
        {
            double price = 0;
            foreach (IProduct product in products)
            {
                price += product.price;
            }
            return price;
        }
        public void deleteProduct(string userName,string fullList)
        {
            Console.Clear();
            Console.WriteLine(fullList);
            IProduct product;
            Console.WriteLine("Enter ID of the Product!");
            product = Database.getProduct(int.Parse(Console.ReadLine()));
            products.Remove(product);
            Database.deleteProductFromCart(userName, product.id.ToString());
            Console.WriteLine("Removed Successfully.\n Press any button to go back.");
            Console.ReadKey();
        }
        public void clearCart(string userName)
        {
            products.Clear();
            Database.clearCart(userName);
        }
        public string load()
        {
            string s=""; 
            foreach (var item in products)
            {
                s += $"{item.id} {item.name}\n";
            }
            return s;
        }

    }
}
