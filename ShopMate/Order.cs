using System;
using System.IO;

namespace ShopMate
{
    public class Order
    {
        public string username { get; set; }
        public int Id { get; set; }
        public Order(string username)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\OrderInfo\\ordersForAdmin.txt");
            string[] lines = File.ReadAllLines(path);
            Id = lines.Length + 1000001;
            this.username = username;
        }
        public void complete(string id)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string userPath = Path.Combine(baseDirectory, $"Database\\OrderInfo\\{username}_Orders.txt");
            try
            {
                string[] lines = File.ReadAllLines(userPath);

                foreach (string line in lines)
                {
                    string[] userParts = line.Split(',');
                    string orderID = userParts[0];
                    string productId = userParts[1];
                    string productName = userParts[2];
                    string productPrice = userParts[3];
                    string productQuantity = userParts[4];
                    string orderStatus = userParts[5];
                    if (id == orderID)
                    {
                        string info = $"{orderID} ,{productId},{productName},{productPrice},{productQuantity}, Delivered\n";
                        File.AppendAllText(userPath, info);
                        return;
                    }


                }


            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            }

        }
        public string createOrder(string[] cartItems)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string AdminPath = Path.Combine(baseDirectory, $"Database\\OrderInfo\\ordersForAdmin.txt");
            string userPath = Path.Combine(baseDirectory, $"Database\\OrderInfo\\{username}_Orders.txt");
            try
            {
                foreach (string line in cartItems)
                {
                    string[] userParts = line.Split(',');
                    string productId = userParts[0];
                    string productName = userParts[1];
                    string productPrice = userParts[2];
                    string productQuantity = userParts[3];
                    string info = $"{Id} ,{productId},{productName},{productPrice},{productQuantity}, Pending\n";
                    File.AppendAllText(AdminPath, info);
                    File.AppendAllText(userPath, info);
                    Id++;
                    return info;
                }


            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            }
            return null;
        }


    }
}
