using System;
using System.Collections.Generic;
using System.IO;

namespace ShopMate
{
    public interface IMessegeSendable
    {
        void sendMessageToAll();
    }
    public interface IInventoryManagable
    {
        void manageInventory();
    }

    public interface ICouponGenerable
    {
        void generateCoupon();
    }

    public class Admin : IAccount, IMessegeSendable, IInventoryManagable, ICouponGenerable
    {
        public void DashBoard()
        {
            string[] adminDashBoardOptions = { "Add New Product", "Add Quantity Of Existing Product", "Watch Orders", "Inbox", "Send Message to all users", "Generate Coupon", "Change Password", "Log Out" };
            Menu menu = new Menu(adminDashBoardOptions);

            int input = menu.Run();



            switch (input)
            {
                case 0: AddProduct(); break;
                case 1: manageInventory(); break;
                case 2: watchOrders(); break;
                case 3:
                    Console.Clear();
                    loadinbox();
                    DashBoard();
                    break;
                case 4:
                    sendMessageToAll();
                    DashBoard();
                    break;
                case 5:
                    generateCoupon();
                    DashBoard();
                    break;
                case 6: changePassword(); break;
                case 7: logOut(); break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }
        private void loadinbox()
        {
            Messenger messenger = new Messenger();
            messenger.watchMessage("admin");
        }
        public void changePassword()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, "Database\\userInfos\\adminPass.txt");
            StreamReader sr = new StreamReader(path);
            string password;
            password = sr.ReadLine();
            sr.Close();
            Console.WriteLine("Enter Old PassWord: ");
            string oldPassWord = Console.ReadLine();
            if (utility.hashing(oldPassWord) == password)
            {
                Console.WriteLine("Enter New PassWord: ");
                string newPassWord = utility.EncryptPassword();

                newPassWord = utility.hashing(newPassWord);

                if (newPassWord == password)
                {
                    string s = "Your New PassWord Cannot be same as your Previous PassWord.Try again?";

                    string[] Options = { "Yes", "No" };
                    Menu menu = new Menu(Options);
                    int inp = menu.Run(s);

                    switch (inp)
                    {
                        case 0:
                            Console.Clear();
                            changePassword();
                            return;
                        case 1:
                            Console.Clear();
                            DashBoard();
                            return;
                        default:
                            Console.WriteLine("Invalid input.");
                            return;
                    }
                }
                else
                {
                    password = newPassWord;

                    try
                    {
                        File.WriteAllText(path, password);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Error updating password in the file: " + ex.Message);
                        Console.ReadLine();
                    }

                    string s = "Password Changed Successfully";
                    string[] options = { "Continue" };
                    Menu menu = new Menu(options);
                    int inp = menu.Run(s);
                    DashBoard();
                    return;
                }
            }
            else
            {

                string s = "Incorrect PassWord!Try again?";

                string[] Options = { "Yes", "No" };
                Menu menu = new Menu(Options);
                int inp = menu.Run(s);

                switch (inp)
                {
                    case 0:
                        Console.Clear();
                        changePassword();
                        return;
                    case 1:
                        Console.Clear();
                        DashBoard();
                        return;
                    default:
                        Console.WriteLine("Invalid input.");
                        return;
                }
            }

        }
        public void AddProduct()
        {
            string[] addProductOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Go to DashBoard" };
            IproductAdder productManager;

            Menu menu = new Menu(addProductOptions);

            int input = menu.Run();

            switch (input)
            {
                case 0:
                    productManager = new ElectronicProductsManager();
                    Console.WriteLine();
                    break;
                case 1:
                    productManager = new ClothingProductsManager();
                    Console.WriteLine();
                    break;
                case 2:
                    productManager = new HomeAppliencesManager();
                    Console.WriteLine();
                    break;

                case 3:
                    DashBoard();
                    return;
                default:
                    Console.WriteLine("Invalid Input");
                    AddProduct();
                    return;
            }

            productManager.addProduct(FillProductDetails());
            string[] addInventoryOptions = { "YES", "NO" };

            menu = new Menu(addInventoryOptions);

            input = menu.Run("Add another product?");

            switch (input)
            {
                case 0:
                    AddProduct();
                    return;
                case 1:
                    DashBoard(); return;
                default:
                    Console.WriteLine("Invalid Input");
                    return;
            }
        }
        public Product FillProductDetails()
        {
            string name, description;
            double price;
            int quantity;
            Console.WriteLine("Enter Name of the product:");
            name = Console.ReadLine();

            Console.WriteLine("Enter Price of the product");
            try
            {
                price = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Press Any key to try again.."); Console.ReadKey(); return FillProductDetails();
            }

            Console.WriteLine("Enter Quantity of the product");
            try
            {
                quantity = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Press Any key to try again.."); Console.ReadKey(); return FillProductDetails();
            }

            Console.WriteLine("Enter Description of the product");
            description = Console.ReadLine();
            return new Product(name, price, quantity, description);

        }
        public void manageInventory()
        {
            string[] addInventoryOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Menu" };

            IProductManager inventoryManager;

            Menu menu = new Menu(addInventoryOptions);

            int input = menu.Run();

            switch (input)
            {
                case 0:
                    inventoryManager = new ElectronicProductsManager();
                    break;
                case 1:
                    inventoryManager = new ClothingProductsManager();
                    break;
                case 2:
                    inventoryManager = new HomeAppliencesManager();
                    break;
                case 3:
                    DashBoard(); return;
                default:
                    Console.WriteLine("Invalid Input");
                    return;
            }
            int choosenProductIndex;
            do
            {
                choosenProductIndex = inventoryManager.showInventory();
                if (choosenProductIndex == -1)
                {
                    manageInventory();
                    return;
                }
                else break;
            } while (true);
            inventoryManager.addInventory(choosenProductIndex);
            Console.WriteLine("Inventory updated Successfully!");
            Console.ReadKey();
            DashBoard(); return;
        }
        public void sendMessageToAll()
        {
            Console.WriteLine("Write your message.\nNB:Submit blank to go back...\n");
            string msg = Console.ReadLine();
            if (msg != "")
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(baseDirectory, $"Database\\userInfos\\user.txt");
                string[] users = File.ReadAllLines(path);
                Messenger messenger = new Messenger();
                foreach (var userInfo in users)
                {
                    string[] userParts = userInfo.Split(',');

                    messenger.sendMessage(userParts[0], msg);

                }
            }
            Console.WriteLine("Sent Successfully!");
            Console.ReadKey(true);

        }
        public void logOut()
        {
            Console.Clear();
            Console.WriteLine("------------Logged out successfully-------------");
            Console.ReadKey();
            utility.mainMenu();
            return;
        }
        public void watchOrders()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\OrderInfo\\ordersForAdmin.txt");

            try
            {
                string[] lines = File.ReadAllLines(path);
                List<string> ordersList = new List<string>();

                foreach (var item in lines)
                {
                    string[] userParts = item.Split(',');
                    string orderID = userParts[0];
                    //string userName = userParts[1];
                    //string productId = userParts[2];
                    string productName = userParts[3];
                    //string productPrice = userParts[4];
                    string productQuantity = userParts[5];
                    string orderStatus = userParts[6];
                    ordersList.Add($"ID:{orderID}\tProduct Name :{productName}\t\tStatus : {orderStatus}");

                }
                ordersList.Add("GO Back");
                Menu menu = new Menu(ordersList.ToArray());
                int index = menu.Run();
                if (index == ordersList.Count - 1)
                {
                    DashBoard();
                    return;
                }
                changeOrderStatus(path, index);
                return;

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                DashBoard();
                return;
            }
        }
        public void changeOrderStatus(string userPath, int index)
        {
            try
            {
                string[] lines = File.ReadAllLines(userPath);
                string[] userParts = lines[index].Split(',');
                string orderID = userParts[0];
                string userName = userParts[1];
                string productId = userParts[2];
                string productName = userParts[3];
                string productPrice = userParts[4];
                string productQuantity = userParts[5];
                string orderStatus = userParts[6];
                string s = $"ID:{orderID}\tProduct Name :{productName}\t\t Customer user name:{userName}\n\nChange order status to:";
                string[] orderStatusOption = { "Delivered", "Pending", "Go Back" };
                Menu menu = new Menu(orderStatusOption);

                switch (menu.Run(s))
                {
                    case 0:
                        orderStatus = "Delivered";
                        break;
                    case 1:
                        orderStatus = "Pending";
                        break;
                    case 2:
                        watchOrders(); return;
                    default:
                        Console.WriteLine("Invalid Input"); return;


                }
                lines[index] = $"{orderID},{userName},{productId},{productName},{productPrice},{productQuantity},{orderStatus}";
                File.WriteAllLines(userPath, lines);
                Messenger messenger = new Messenger();
                string msg = $"Your order,  ID: {orderID} has some update. Status: {orderStatus}";
                messenger.sendMessage(userName, msg);
                Console.WriteLine($"You have successfully Changed the order status to \"{orderStatus}\"");
                IOrderStatusChanger orderStatuschanger = new OrderManager(userName);
                orderStatuschanger.changeOrderStatus(orderID, orderStatus);
                Console.ReadLine();
                watchOrders();
                return;

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                DashBoard();
                return;
            }
        }

        public void generateCoupon()
        {
            Console.WriteLine("Write a code that you want to generate as coupon");
            string coupon = Console.ReadLine();
            Console.WriteLine("What is the Discount in percent?");

            string discount = Console.ReadLine();
            string msg = $"Get {discount}% off on using coupon \"{coupon}\". Get your  favourite products now!";

            List<string> userNameList = new List<string> { "Go Back to dashboard", "Everyone" };
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\userInfos\\user.txt");
            string[] users = File.ReadAllLines(path);
            foreach (var userInfo in users)
            {
                string[] userParts = userInfo.Split(',');
                userNameList.Add(userParts[0]);
            }
            Messenger messenger = new Messenger();
            string[] options = userNameList.ToArray();
            couponManager coupnManager = new couponManager();
            Menu menu = new Menu(options);
            int input = menu.Run("Who can use this coupon?");
            switch (input)
            {
                case 0:
                    DashBoard();
                    return;
                case 1:
                    userNameList.Remove("Go Back to dashboard");
                    userNameList.Remove("Everyone");
                    foreach (var username in userNameList)
                    {
                        coupnManager.generateCoupon(username, coupon, discount);
                        messenger.sendMessage(username, msg);
                    }
                    break;
                default:
                    coupnManager.generateCoupon(options[input - 2], coupon, discount);
                    messenger.sendMessage(options[input - 2], msg);

                    break;
            }
            Console.WriteLine("Coupon generated Successfully.");
            Console.ReadKey();
        }
         
    }
}
