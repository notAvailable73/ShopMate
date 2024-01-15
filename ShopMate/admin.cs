using System;
using System.IO;

namespace ShopMate
{
    public class Admin : IAccount
    {
        public void DashBoard()
        {
            string[] adminDashBoardOptions = { "Add New Product", "Add Quantity Of Existing Product", "Inbox", "Send Message", "Change Password", "Log Out" };
            Menu menu = new Menu(adminDashBoardOptions);

            int input = menu.Run();



            switch (input)
            {
                case 0: AddProduct(); break;
                case 1: manageInventory(); break;
                //case 2:
                //    Console.Clear();
                //    AdminInbox.GetAdminInbox();
                //    loadinbox();
                //    break;
                //case 3:
                //    Console.Clear();
                //    Console.WriteLine();
                //    Console.WriteLine("What Message Do You Want To Send?");
                //    string message = Console.ReadLine();
                //    sendMessage(message);
                //    Console.WriteLine();
                //    Console.WriteLine("Message Sent Successfully");
                //    Console.WriteLine("Press any key to Continue");
                //    Console.ReadKey(true);
                //    dashboard();
                //    break;
                case 4: changePassword(); break;
                case 5: logOut(); break;
                default: Console.WriteLine("Invalid Input"); break;
            }
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
            double price ;
            int quantity ;
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

        }

        //public void sendMessage(string message)
        //{
        //    foreach (user user in Database.userList)
        //    {
        //        user.inbox.MessageFromAdmin(message, user.userName);
        //    }
        //}

        public void logOut()
        {
            Console.Clear();
            Console.WriteLine("------------Logged out successfully-------------");
            utility.mainMenu();
            return;
        }
    }
}
