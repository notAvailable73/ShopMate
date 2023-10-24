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
        public double price()
        {
            double price = 0;
            foreach (IProduct product in products)
            {
                price += product.price;
            }
            return price;
        }
        public void deleteProduct(string userName, IProduct product)
        {
            products.Remove(product);
            Database.deleteProductFromCart(userName, product.id.ToString());
        }
        public void clearCart(string userName)
        {
            products.Clear();
            Database.clearCart(userName);
        }
        public void showCart()
        {
            int index = 1;
            foreach (IProduct item in products)
            {
                Console.WriteLine($"{index}. {item.name}");
                index++;
            }
        }

    }
}
