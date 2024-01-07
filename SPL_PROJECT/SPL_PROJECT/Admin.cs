using System;
using System.IO;
namespace SPL_PROJECT
{
    public class Admin : IAccount
    {

        public void AddProduct()
        {
            string[] addProductOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Menu" };
            IProductAdder productAdder;

            Menu menu = new Menu(addProductOptions);

            int input = menu.Run();
              
            switch (input)
            {
                case 0:
                    productAdder = new ElectronicProductsAdder();
                    Database.addProduct(productAdder);
                    Console.WriteLine();
                    dashboard();
                    break;
                case 1:
                    productAdder = new ClothingProductsAdder();
                    Database.addProduct(productAdder);
                    Console.WriteLine();
                    dashboard();
                    break;
                case 2:
                    productAdder = new HomeAppliencesAdder();
                    Database.addProduct(productAdder);
                    Console.WriteLine();
                    dashboard();
                    break;

                case 3:
                    dashboard();
                    return;
                default:
                    Console.WriteLine("Invalid Input");
                    AddProduct();
                    return;
            }
        }

        public void InventoryAdder()
        {
            string[] addInventoryOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Menu" };

            IInventoryAdder inventoryAdder;

            Menu menu = new Menu(addInventoryOptions);
       
            int input = menu.Run();

            switch (input)
            {
                case 0:
                    inventoryAdder = new ElectronicInventoryAdder();
                    inventoryAdder.AddToInventory();
                    Console.WriteLine();
                    dashboard();
                    break;
                case 1:
                    inventoryAdder = new ClothingInventoryAdder();
                    inventoryAdder.AddToInventory();
                    Console.WriteLine();
                    dashboard();
                    break;
                case 2:
                    inventoryAdder = new HomeApplienceInventoryAdder();
                    inventoryAdder.AddToInventory();
                    Console.WriteLine();
                    dashboard();
                    break;                  
                default:
                    Console.WriteLine("Invalid Input");
                    dashboard();
                    return;
            }
        }
        public void changePassword()
        {
            string path = @"C:\ShopMate\adminpassword.txt";
            string[] pass = File.ReadAllLines(path);
            string password = pass[0];

            Console.WriteLine("Enter Old PassWord: ");
            string oldPassWord = Console.ReadLine();
            string newPassWord;

            if (utility.hashing(oldPassWord) == password)
            {
                Console.WriteLine("Enter New PassWord: ");
                newPassWord = Console.ReadLine();

                newPassWord = utility.hashing(newPassWord);

                if (newPassWord == password)
                {
                    string s = "Your New PassWord Cannot be same as your Previous PassWord";

                    string[] options = { "Continue" };
                    Menu menu = new Menu(options);
                    int inp = menu.Run(s);
                    dashboard();
                }
                else
                    
                {
                    File.WriteAllText(path, newPassWord);
                    string s = "Password Changed Successfully";
                    string[] options = { "Continue" };
                    Menu menu = new Menu(options);
                    int inp = menu.Run(s);
                    utility.mainMenu();
                }
            }
            else
            {

                string s = "Incorrect PassWord!";
                Console.WriteLine(password);
                string[] options = { "Continue" };
                Menu menu = new Menu(options);   
                int inp = menu.Run(s);
                dashboard();
            }
            
        }
        public void loadinbox() {
            string messagetlist = AdminInbox.load();
            if (messagetlist == "")
            {
                Console.Clear();
                Console.WriteLine("Inbox Empty!\nPress any key to go to dashboard.");
                Console.ReadKey(true);
                Admin admin = new Admin();
                AdminInbox.emptyList();
                admin.dashboard();
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine(messagetlist);
                Console.WriteLine("\nPress any key to go to dashboard.");
                Console.ReadKey(true);
                Admin admin = new Admin();
                AdminInbox.emptyList();
                admin.dashboard();
            }
        }

        public void dashboard()
        {
            string[] adminDashBoardOptions = { "Add New Product","Add Quantity Of Existing Product", "Inbox", "Change Password", "Log Out" };
            Menu menu=new Menu(adminDashBoardOptions);          

            int input = menu.Run();

            

            switch (input)
            {
                case 0: AddProduct(); break;
                case 1: InventoryAdder();break;
                case 2:Console.Clear();                    
                       AdminInbox.GetAdminInbox();
                       loadinbox();
                       break;
                case 3: changePassword();  break;
                case 4: logOut(); break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }

        public void logOut()
        {
            Console.Clear();
            Console.WriteLine("------------Logged out successfully-------------");
            utility.mainMenu();
            return;
        }

    }
}
