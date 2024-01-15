using ShopMate;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShopMate
{
    public interface IproductAdder
    {
        void addProduct(Product newProduct);
    }
    public interface IProductFinder
    {
        Product getProduct(int index);
    }
    public interface IProductDisplay
    {
        int DisplayProducts();
    }
    public interface IProductDesicription
    {
        string getProductDescription(int index);
    }
    public interface IInventoryManager
    {
        void addInventory(int index);
        int showInventory();
    }
    public interface IProductManager : IproductAdder, IProductFinder, IProductDisplay, IProductDesicription, IInventoryManager
    {
        //void addProduct(Product newProduct);
        //int DisplayProducts();
        //string getProductDescription(int index);
        //Product getProduct(int index);
    }
    public class ElectronicProductsManager : IProductManager
    {
        public void addInventory(int index)
        {
             
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt");
            Console.WriteLine();
            string[] lines;
            try
            {
                lines = File.ReadAllLines(path);
                string[] userParts = lines[index].Split(',');
                string id = userParts[1];
                string productName = userParts[1];
                decimal price = Convert.ToDecimal(userParts[2]);
                int quantity = Convert.ToInt32(userParts[3]);
                string productDescription = userParts[4];
                Console.WriteLine($"How Many More {productName} Do You Want To Add?");
                int newQuantity ;
                try
                {
                    newQuantity  = Convert.ToInt32(Console.ReadLine());
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);  
                    Console.ReadLine();
                    addInventory(index); return ;
                }
                lines[index] = $"{id},{productName},{price},{newQuantity + quantity},{productDescription}";

                File.WriteAllLines(path, lines);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error updating inventory: " + ex.Message);
                Console.ReadLine();
                addInventory(index); return;

            }
        }

        public void addProduct(Product newProduct)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt");
            int id = 10001 + File.ReadAllLines(path).Length;
            string info = $"{id},{newProduct.name},{newProduct.price},{newProduct.qty},{newProduct.description}\n";
            File.AppendAllText(path, info);
            Console.WriteLine($"Product added Successfully.");
        }

        public int DisplayProducts()
        {
            List<string> Options = new List<string>();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt");
            try
            {
                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    //string id = userParts[0];
                    string productName = userParts[1];
                    //decimal price = Convert.ToDecimal(userParts[2]);
                    //int quantity =Convert.ToInt32(userParts[3]);
                    //string productDescription = userParts[4];
                    Options.Add(productName);
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            }

            Options.Add("GO Back");

            string[] ElectronicProductOptions = Options.ToArray();

            Menu menu = new Menu(ElectronicProductOptions);
            int index = menu.Run($"-------------------Electronic Products---------------------");
            if (index == Options.Count - 1)
            {
                return -1;
            }
            return index;


        }

        public Product getProduct(int index)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt");
            List<Product> products = utility.getProductList(path);
            return products[index];
        }

        public string getProductDescription(int index)
        {
            Console.Clear();
            Console.WriteLine("---------------------Product Details------------------------");
            Console.WriteLine();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt");
            List<Product> products = utility.getProductList(path);
            string description = $"Name: {products[index].name}\n"
                              + $"Price: {products[index].price}\n"
                              + $"Quantity: {products[index].qty}\n"
                              + $"Description: {products[index].description}";

            return description;
        }

        public int showInventory()
        {
            List<string> Options = new List<string>();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt");
            try
            {
                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    string productName = userParts[1];
                    //decimal price = Convert.ToDecimal(userParts[2]);
                    int quantity = Convert.ToInt32(userParts[3]);
                    //string productDescription = userParts[4];
                    Options.Add($"{productName}\t\tAvailable: {quantity}");
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            }

            Options.Add("GO Back");

            string[] ElectronicProductOptions = Options.ToArray();

            Menu menu = new Menu(ElectronicProductOptions);
            int index = menu.Run($"-------------------Electronic Products---------------------");
            if (index == Options.Count - 1)
            {
                return -1;
            }
            return index;
        }
    }
}
public class ClothingProductsManager : IProductManager
{
    public void addProduct(Product newProduct)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt");
        int id = 20001 + File.ReadAllLines(path).Length;
        string info = $"{id},{newProduct.name},{newProduct.price},{newProduct.qty},{newProduct.description}\n";

        File.AppendAllText(path, info);
        Console.WriteLine($"Product added Successfully.");
    }
    public int DisplayProducts()
    {
        List<string> Options = new List<string>();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt");
        try
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] userParts = lines[i].Split(',');
                string productName = userParts[1];
                //decimal price = Convert.ToDecimal(userParts[2]);
                //int quantity =Convert.ToInt32(userParts[4]);
                //string productDescription = userParts[4];
                Options.Add(productName);
            }

        }
        catch (IOException ex)
        {
            Console.WriteLine("Error : " + ex.Message);
            Console.ReadLine();
        }

        Options.Add("GO Back");

        string[] ClothingProductOptions = Options.ToArray();

        Menu menu = new Menu(ClothingProductOptions);

        int index = menu.Run($"-------------------clothing Products---------------------");
        if (index == Options.Count - 1)
        {
            return -1;
        }
        return index;
    }

    public string getProductDescription(int index)
    {
        Console.Clear();
        Console.WriteLine("---------------------Product Details------------------------");
        Console.WriteLine();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt");
        List<Product> products = utility.getProductList(path);
        string description = $"Name: {products[index].name}\n"
                          + $"Price: {products[index].price}\n"
                          + $"Quantity: {products[index].qty}\n"
                          + $"Description: {products[index].description}";

        return description;
    }
    public Product getProduct(int index)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt");
        List<Product> products = utility.getProductList(path);
        return products[index];
    }

    public void addInventory(int index)
    {

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt");
        Console.WriteLine();
        string[] lines;
        try
        {
            lines = File.ReadAllLines(path);
            string[] userParts = lines[index].Split(',');
            string id = userParts[1];
            string productName = userParts[1];
            decimal price = Convert.ToDecimal(userParts[2]);
            int quantity = Convert.ToInt32(userParts[3]);
            string productDescription = userParts[4];
            Console.WriteLine($"How Many More {productName} Do You Want To Add?");
            int newQuantity ;
            try
            {
                newQuantity = Convert.ToInt32(Console.ReadLine());
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
                addInventory(index); return;
            }
            lines[index] = $"{id},{productName},{price},{newQuantity + quantity},{productDescription}";

            File.WriteAllLines(path, lines);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error updating inventory: " + ex.Message);
            Console.ReadLine();
            addInventory(index); return;

        }
    }


    public int showInventory()
    {
        List<string> Options = new List<string>();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt");
        try
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] userParts = lines[i].Split(',');
                string productName = userParts[1];
                //decimal price = Convert.ToDecimal(userParts[2]);
                int quantity = Convert.ToInt32(userParts[3]);
                //string productDescription = userParts[4];
                Options.Add($"{productName}\t\tAvailable: {quantity}");
            }

        }
        catch (IOException ex)
        {
            Console.WriteLine("Error : " + ex.Message);
            Console.ReadLine();
        }

        Options.Add("GO Back");

        string[] ClothingProductOptions = Options.ToArray();

        Menu menu = new Menu(ClothingProductOptions);
        
        int index = menu.Run($"-------------------Clothing Products---------------------");
        if (index == Options.Count - 1)
        {
            return -1;
        }
        return index;
    }
}
public class HomeAppliencesManager : IProductManager
{
    public void addProduct(Product newProduct)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt");
        int id = 30001 + File.ReadAllLines(path).Length;
        string info = $"{id},{newProduct.name},{newProduct.price},{newProduct.qty},{newProduct.description}\n";
        File.AppendAllText(path, info);
        Console.WriteLine($"Product added Successfully.");
    }
    public int DisplayProducts()
    {
        List<string> Options = new List<string>();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt");
        try
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] userParts = lines[i].Split(',');
                string productName = userParts[1];
                //decimal price = Convert.ToDecimal(userParts[2]);
                //int quantity =Convert.ToInt32(userParts[4]);
                //string productDescription = userParts[4];
                Options.Add(productName);
            }

        }
        catch (IOException ex)
        {
            Console.WriteLine("Error : " + ex.Message);
            Console.ReadLine();
        }

        Options.Add("GO Back");

        string[] homeappliencesProductOptions = Options.ToArray();

        Menu menu = new Menu(homeappliencesProductOptions );

        int index = menu.Run($"-------------------homeappliences  Products---------------------");
        if (index == Options.Count - 1)
        {
            return -1;
        }
        return index;

    }

    public string getProductDescription(int index)
    {
        Console.Clear();
        Console.WriteLine("---------------------Product Details------------------------");
        Console.WriteLine();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt");
        List<Product> products = utility.getProductList(path);
        string description = $"Name: {products[index].name}\n"
                          + $"Price: {products[index].price}\n"
                          + $"Quantity: {products[index].qty}\n"
                          + $"Description: {products[index].description}";

        return description;
    }
    public Product getProduct(int index)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt");
        List<Product> products = utility.getProductList(path);
        return products[index];
    }

    public void addInventory(int index)
    {

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt");
        Console.WriteLine();
        string[] lines;
        try
        {
            lines = File.ReadAllLines(path);
            string[] userParts = lines[index].Split(',');
            string id = userParts[1];
            string productName = userParts[1];
            decimal price = Convert.ToDecimal(userParts[2]);
            int quantity = Convert.ToInt32(userParts[3]);
            string productDescription = userParts[4];
            Console.WriteLine($"How Many More {productName} Do You Want To Add?");
            int newQuantity ;
            try
            {
                newQuantity  = Convert.ToInt32(Console.ReadLine());
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
                addInventory(index); return;
            }
            lines[index] = $"{id},{productName},{price},{newQuantity + quantity},{productDescription}";

            File.WriteAllLines(path, lines);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error updating inventory: " + ex.Message);
            Console.ReadLine();
            addInventory(index); return;

        }
    }


    public int showInventory()
    {
        List<string> Options = new List<string>();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt");
        try
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] userParts = lines[i].Split(',');
                string productName = userParts[1];
                //decimal price = Convert.ToDecimal(userParts[2]);
                int quantity = Convert.ToInt32(userParts[3]);
                //string productDescription = userParts[4];
                Options.Add($"{productName}\t\tAvailable: {quantity}");
            }

        }
        catch (IOException ex)
        {
            Console.WriteLine("Error : " + ex.Message);
            Console.ReadLine();
        }

        Options.Add("GO Back");

        string[] homeappliencesProductOptions = Options.ToArray();

        Menu menu = new Menu(homeappliencesProductOptions);
        int index = menu.Run($"-------------------homeappliences Products---------------------");
        if (index == Options.Count - 1)
        {
            return -1;
        }
        return index;
    }
}

