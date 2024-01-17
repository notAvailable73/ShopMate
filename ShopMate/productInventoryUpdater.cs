using System;
using System.IO;

namespace ShopMate
{
    public interface inventoryUpdatable
    {
        void update();
    }
    public class productInventoryUpdater : inventoryUpdatable
    {
        string cartPath;
        public productInventoryUpdater(string cartPath)
        {
            this.cartPath = cartPath;
        }
        public void update()
        {
            try
            {
                string[] lines = File.ReadAllLines(cartPath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    string id = userParts[0];
                    string name = userParts[1];
                    string price = userParts[2];
                    string quantity = userParts[3];
                    //10001,eProduct1,138.59,1

                    updateInventory(id, Convert.ToInt32(quantity));
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR!  " + ex.Message);
                Console.ReadLine();
            }

        }
        private void updateInventory(string id, int quantity)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] paths = { Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt"), Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt"), Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt") };
            foreach (var path in paths)
            {
                if (findAndUpdate(id, quantity, path))
                {
                    return;
                }
            }

        }
        public bool isAllProductAvailable()
        {
            string[] lines = File.ReadAllLines(cartPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] userParts = lines[i].Split(',');
                string id = userParts[0];
                string quantity = userParts[3];
                if (!isAvailable(id, Convert.ToInt32(quantity)))
                {
                    return false;
                }
            }
            return true;
        }
        private bool findAndUpdate(string id, int quantity, string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    string productId = userParts[0];
                    string productName = userParts[1];
                    decimal price = Convert.ToDecimal(userParts[2]);
                    int productQuantity = Convert.ToInt32(userParts[3]);
                    string productDescription = userParts[4];
                    if (id == productId)
                    {
                        int newquantiTy = productQuantity - quantity;
                        lines[i] = $"{productId},{productName},{price},{newquantiTy},{productDescription}";
                        File.WriteAllLines(path, lines);
                        if (newquantiTy < 5)
                        {
                            stockAlert(productId, newquantiTy);
                        }
                        return true;
                    }
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            }
            return false;
        }
        private bool isAvailable(string id, int quantity)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] paths = { Path.Combine(baseDirectory, $"Database\\productList\\electronicproduct.txt"), Path.Combine(baseDirectory, $"Database\\productList\\clothingproduct.txt"), Path.Combine(baseDirectory, $"Database\\productList\\homeapplienceproduct.txt") };

            try
            {
                foreach (var path in paths)
                {
                    string[] lines = File.ReadAllLines(path);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] userParts = lines[i].Split(',');                       
                        string name = userParts[1];                      
                        int availableQuantity = Convert.ToInt32(userParts[3]);
                        if (id == userParts[0])
                        {
                            if (quantity <= availableQuantity)
                            {
                                return true;
                            }
                            else
                            {
                                Console.WriteLine($"Sorry, we don't have enough {name} according to your needs.");
                                return false;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR! DASV " + ex.Message);
                Console.ReadLine();
            }
            return false;
        }
        private void stockAlert(string ProductID, int newquantiTy)
        {
            string msg = $"Product  ID:{ProductID} has low inventory. Available unit: {newquantiTy}";
            Messenger messenger = new Messenger();
            messenger.sendMessage("admin", msg);
        }
    }
}
