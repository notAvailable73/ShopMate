using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public interface IProductDisplay
    {
        void DisplayProducts(List<IProduct> productList, string category, string userName);
    }

    public class ProductDisplay : IProductDisplay
    {
        public void DisplayProducts(List<IProduct> productList, string category, string userName)
        {
            Console.WriteLine($"-------------------{category}---------------------");
            Console.WriteLine();

            foreach (IProduct item in productList)
            {
                Console.WriteLine($"{item.id} {item.name}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter Product Id To See Details");

            int input = int.Parse(Console.ReadLine());
            int index = input - (productList.First().id);
            Console.Clear();
            Console.WriteLine("---------------------Product Details------------------------");
            Console.WriteLine();

            try
            {
                if (productList[index] != null)
                {
                    IProduct product = productList[index];
                    product.DisplayDetails();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                DisplayProducts(productList, category, userName);
            }
            string[] options = { "Add to Cart", "Dashboard" };
            Menu menu1 = new Menu(options);

            int inp = menu1.Run(productList[index]);


            switch (inp)
            {
                case 0:
                    Console.Clear();
                    Database.addProductToCart(userName, productList[index]);
                    Console.WriteLine("--------------Product Added To Cart Successfully---------------");
                    Console.WriteLine();
                    return;
                case 1: Console.Clear(); return;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }
        }
    }

}
