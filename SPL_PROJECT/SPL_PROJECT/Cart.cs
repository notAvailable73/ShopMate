using System;
using System.Collections.Generic;

namespace SPL_PROJECT
{
    public class Cart
    {
        public List<IProduct> products = new List<IProduct>();
        public void AddProductToCart(IProduct product)
        {
            AddProductToThisCart(product);
            Database.addProductToCart( product);
        }
        public void AddProductToThisCart(IProduct product)
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
        public void deleteProduct(string fullList)
        {
            Console.Clear();
            string s = "Select The Product To Remove";

            string[] options = fullList.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            //for (int i = 0; i < options.Length; i++)
            //{
            //    options[i] = options[i].Trim(); // Trim extra spaces
            //}

            Menu menu=new Menu(options);

            int inp=menu.Run(s);
            
            //Console.WriteLine(fullList);

            IProduct product;

            //Console.WriteLine("Enter ID of the Product!");

            string productinfo = options[inp];
            string productID = productinfo.Split(' ')[0];
            //productID = productID.Trim();

            int ID=Convert.ToInt32(productID);

            product = Database.getProduct(ID);
            products.Remove(product);
            Database.deleteProductFromCart( product.id.ToString());

            string message = "Product Removed Successfully.";
            string[] options1 = { "GO Back" };
            Menu menu1 = new Menu(options1);

            int inp1=menu1.Run(message);
            Session.CurrentUser.loadCart();
        }
        public void clearCart()
        {
            products.Clear();
            Database.clearCart();
        }
        public string load()
        {
            //products.Sort();
            string s=""; 
            foreach (var item in products)
            {
                s += $"{item.id} {item.name}\n";
            }
            return s;
        }

        public void confirmOrder()
        {
            foreach(IProduct product in products)
            {
                if(product.id>10000 && product.id<20000)
                {
                    Database.EditElectronicProductQuantity(product,-1);
                }
                else if(product.id>20000 && product.id<30000)
                {
                    Database.EditClothingProductQuantity(product,-1);
                }
                else
                {
                    Database.EditHomeProductQuantity(product,-1);
                }
            }
            products.Clear();
            Database.clearCart();
        }
    }
}
