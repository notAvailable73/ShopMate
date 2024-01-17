using System;
using System.IO;

namespace ShopMate
{
    public interface IOrderStatusChanger
    {
        void changeOrderStatus(string id, string status);
    }

    public interface IOrderCreator
    {
        void createOrder(string[] cartItems);
    }

    public class OrderManager : IOrderStatusChanger, IOrderCreator
    {
        public string username { get; set; }
        public int Id { get; set; }
        public OrderManager(string username)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\OrderInfo\\ordersForAdmin.txt");
            string[] lines = File.ReadAllLines(path);
            Id = lines.Length + 3101210;
            this.username = username;
        }
        public void changeOrderStatus(string id, string status)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string userPath = Path.Combine(baseDirectory, $"Database\\OrderInfo\\{username}_Orders.txt");
            try
            {
                string[] lines = File.ReadAllLines(userPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    //foreach (string line in lines)
                
                    string[] userParts = lines[i].Split(',');
                    string orderID = userParts[0];
                    string userName = userParts[1];
                    string productId = userParts[2];
                    string productName = userParts[3];
                    string productPrice = userParts[4];
                    string productQuantity = userParts[5];
                    string orderStatus = userParts[6];
                    if (id == orderID)
                    {
                        lines[i] = $"{orderID},{userName},{productId},{productName},{productPrice},{productQuantity},{status}";
                        File.WriteAllLines(userPath, lines);
                        
                    }


                }


            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            }

        }
        public void createOrder(string[] cartItems)
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
                    string info = $"{Id},{username},{productId},{productName},{productPrice},{productQuantity},Pending\n";
                    File.AppendAllText(AdminPath, info);
                    File.AppendAllText(userPath, info);

                    string message = $"Order Completed! ID:{Id} Your order’s been processed";
                    Messenger confirmOrderSender = new Messenger();
                    confirmOrderSender.sendMessage(username, message);
                    message = $"You have recieved a new order from user name {username}. Check your orders to see details.";
                    confirmOrderSender.sendMessage("admin", message);
                    Id++; 
                }


            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            } 
        }


    }
}
