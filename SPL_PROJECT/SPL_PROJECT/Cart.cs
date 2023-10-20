using System.Collections.Generic;

namespace SPL_PROJECT
{
    public class Cart
    {
        List<IProduct> products = new List<IProduct>();
        public Cart(string userName) {
            Database.createCart(userName);
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
        public void deleteProduct(IProduct product)
        {
            products.Remove(product);
        }
        public void clearCart()
        {
            products.Clear();
        }

    }
}
