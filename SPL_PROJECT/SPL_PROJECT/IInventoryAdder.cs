using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public interface IInventoryAdder
    {
        void AddToInventory();
    }
    public class ElectronicInventoryAdder : IInventoryAdder
    {

        public void AddToInventory()
        {

            Console.WriteLine($"-------------------Electronic Products---------------------");
            Console.WriteLine();

            List<string> products = new List<string>();

            foreach (IProduct item in Database.ElectronicProductList)
            {
                products.Add(item.name);
            }

            products.Add("GO Back");

            string[] ElectronicProducts = products.ToArray();

            Menu menu = new Menu(ElectronicProducts);

            int index = menu.Run();

            if (index == products.Count - 1)
            {
                Admin admin = new Admin();
                admin.InventoryAdder();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("---------------------Product Details------------------------");
                Console.WriteLine();

                try
                {
                    if (Database.ElectronicProductList[index] == null)
                    {
                        throw new Exception();
                    }
                    IProduct product = Database.ElectronicProductList[index];
                    Console.WriteLine($"How Many More {product.name} Do You Want To Add?");
                    int add = Convert.ToInt32(Console.ReadLine());
                    Database.EditElectronicProductQuantity(product, add);
                    Console.WriteLine();
                    Console.WriteLine($"{add} {product.name} Added Successfully  ");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey(true);
                    AddToInventory();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    Admin admin = new Admin();
                    admin.InventoryAdder();
                    return;
                }
            }

        }
    }

    public class ClothingInventoryAdder : IInventoryAdder
    {
        public void AddToInventory()
        {

            Console.WriteLine($"-------------------Clothing Products---------------------");
            Console.WriteLine();

            List<string> products = new List<string>();

            foreach (IProduct item in Database.clothList)
            {
                products.Add(item.name);
            }

            products.Add("GO Back");

            string[] ClothProducts = products.ToArray();

            Menu menu = new Menu(ClothProducts);

            int index = menu.Run();

            if (index == products.Count - 1)
            {
                Admin admin = new Admin();
                admin.InventoryAdder();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("---------------------Product Details------------------------");
                Console.WriteLine();

                try
                {
                    if (Database.clothList[index] == null)
                    {
                        throw new Exception();
                    }
                    IProduct product = Database.clothList[index];
                    Console.WriteLine($"How Many More {product.name} Do You Want To Add?");
                    int add = Convert.ToInt32(Console.ReadLine());
                    Database.EditClothingProductQuantity(product, add);
                    Console.WriteLine();
                    Console.WriteLine($"{add} {product.name} Added Successfully  ");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey(true);
                    AddToInventory();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    Admin admin = new Admin();
                    admin.InventoryAdder();
                    return;
                }
            }

        }
    }

    public class HomeApplienceInventoryAdder : IInventoryAdder
    {
        public void AddToInventory()
        {

            Console.WriteLine($"-------------------HomeApplience Products---------------------");
            Console.WriteLine();

            List<string> products = new List<string>();

            foreach (IProduct item in Database.HomeApplienceList)
            {
                products.Add(item.name);
            }

            products.Add("GO Back");

            string[] HomeApplienceProducts = products.ToArray();

            Menu menu = new Menu(HomeApplienceProducts);

            int index = menu.Run();

            if (index == products.Count - 1)
            {
                Admin admin = new Admin();
                admin.InventoryAdder();    
            }
            else
            {
                Console.Clear();
                Console.WriteLine("---------------------Product Details------------------------");
                Console.WriteLine();

                try
                {
                    if (Database.clothList[index] == null)
                    {
                        throw new Exception();
                    }
                    IProduct product = Database.HomeApplienceList[index];
                    Console.WriteLine($"How Many More {product.name} Do You Want To Add?");
                    int add = Convert.ToInt32(Console.ReadLine());
                    Database.EditHomeProductQuantity(product, add);
                    Console.WriteLine();
                    Console.WriteLine($"{add} {product.name} Added Successfully  ");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey(true);
                    AddToInventory();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    Admin admin = new Admin();
                    admin.InventoryAdder();
                    return;
                }
            }
        }
    }
}
