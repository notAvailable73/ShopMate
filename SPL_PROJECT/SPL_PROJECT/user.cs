using Microsoft.SqlServer.Server;
using System;
using System.IO;

namespace SPL_PROJECT
{
    public class user : IAccount
    {
        public string userName { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string date_of_birth { get; set; }
        public Cart cart;
        public inbox inbox;
        public user(string userName, string name, string password, string email, string date)
        {
            this.userName = userName;
            this.name = name;
            this.password = password;
            this.email = email;
            this.date_of_birth = date;
            cart = Database.getCart(userName);
            inbox = new inbox();
        }
        public user(string userName)
        {
            inbox = new inbox();
            this.userName =userName;
        }

        public void dashboard()
        {
            string s = $"------------------Logged In As {Session.CurrentUser.name}----------------------";
            cart = Database.getCart(userName);
            string[] userDashboardOption = { "Browse Products", "Change Password", "Inbox", "Cart","Previous Orders", "Log Out" };
            Menu menu = new Menu(userDashboardOption);

            int input = menu.Run(s); 
            switch (input)
            {
                case 0:
                    Console.Clear();
                    Database.browseProduct();
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine();
                    changePassword();
                    Console.ReadKey();
                    dashboard();
                    break;
                case 2:
                    Console.Clear();
                    loadinbox();
                    break;
                case 3:
                    Console.Clear();
                    loadCart();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine();
                    loadOrders();
                    break;                   

                case 5:
                    Console.Clear();
                    string cartlist = cart.load();
                    if (!(cartlist == ""))
                    {
                        inbox.sendRemindProduct(userName);
                    }
                    logOut();
                    break;
                default:
                    Console.WriteLine("Invalid input."); dashboard();
                    break;

            }
        }
        public void loadCart()
        {
            string cartlist= cart.load();
            if (cartlist == "")
            {
                Console.Clear();
                Console.WriteLine("Empty Cart!\nPress any key to go to dashboard.");
                Console.ReadKey(true);
                dashboard();
                return;
            } 
            string[] cartOptions = { "Remove item from cart", "Clear cart", "Proceed to checkout", "Goto Dashboard" };
            Menu menu = new Menu(cartOptions);

            int input = menu.Run(cartlist);
            switch (input)
            {
                case 0:
                    cart.deleteProduct(cartlist); 
                    loadCart();
                    break;
                case 1:
                    cart.clearCart(); loadCart();
                    break;
                case 2:
                    checkout();

                    break;
                case 3:
                    dashboard();
                    break;
                default:
                    Console.WriteLine("Invalid input.") ; loadCart();
                    break;

            }
            
        }
        public void loadinbox()
        {
            string messagetlist = inbox.load();           
            if (messagetlist == "")
            {
                Console.Clear();
                Console.WriteLine("Inbox Empty!\nPress any key to go to dashboard.");
                Console.ReadKey(true);
                dashboard();
                return;
            }
            Console.Clear();
            Console.WriteLine(messagetlist);
            Console.WriteLine("\nPress any key to go to dashboard.");
            Console.ReadKey(true);
            dashboard();
        }
        public void loadOrders()
        {
            string messagetlist = Database.GetOrders(Session.CurrentUser);
            if (messagetlist == "")
            {
                Console.Clear();
                Console.WriteLine("No Orders Yet.Get Your First order Now\nPress any key to go to dashboard.");
                Console.WriteLine();
                Console.ReadKey(true);
                dashboard();
                return;
            }
            Console.Clear();
            Console.WriteLine(messagetlist);
            Console.WriteLine("\nPress any key to go to dashboard.");
            Console.ReadKey(true);
            dashboard();
        }
        public  void changePassword()
        {
            string userName = Session.CurrentUser.userName;           
            string password = Session.CurrentUser.password;           

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
                    Session.CurrentUser.dashboard();
                }
                else
                {
                    Session.CurrentUser.password = newPassWord;
                    UpdatePasswordInFile(userName, newPassWord);
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

                string[] options = { "Continue" };
                Menu menu = new Menu(options);
                int inp = menu.Run(s);
                Session.CurrentUser.dashboard();
            }

        }
        private void UpdatePasswordInFile(string userName, string newPassword)
        {
            string path = @"C:\ShopMate\user.txt.txt";

            try
            {                
                string[] lines = File.ReadAllLines(path);
                                
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                                        
                    if (userParts.Length > 0 && userParts[0] == userName)
                    {
                        
                        lines[i] = $"{userName},{userParts[1]},{newPassword},{userParts[3]},{userParts[4]}";
                        break; 
                    }
                }
                
                File.WriteAllLines(path, lines);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error updating password in the file: " + ex.Message);
            }
        }

        public void logOut()
        {
            inbox.emptyList();
            Console.WriteLine("logged out successfully");
            utility.mainMenu();
            return;
        }
        public void addToCart(IProduct product)
        {          
         cart.AddProductToCart(product);             
         }
        public void checkout()
        {
            string cartlist = cart.load();
            double price = cart.calculatePrice();
            string[] checkoutOptions = { "Confirm Order","Go Back" };
            Menu menu = new Menu(checkoutOptions);
            string s= cartlist + "\n\nTotal Price: $" + price+"\n\n";
            int input = menu.Run(s);
            switch (input)
            {
                case 0:                                          
                        cart.confirmOrder();
                        Console.WriteLine("Ordered Successfully!\n\nPress any key to visit dashboard.");
                        ExtractProductNames(cartlist);
                        inbox.sendPurchaseMessage(userName);
                        inbox.emptyList();
                        Database.GetInbox(Session.CurrentUser); 
                        Console.ReadKey();
                        dashboard();
                    break;
                                     
                case 1:
                    loadCart();
                    break; 
                default:
                    Console.WriteLine("Invalid input."); loadCart();
                    break;

            }
        }
        
        public void MessageToAdmin(IProduct product)
        {
           
                if (product.quantity == 5)
                {
                    AdminInbox.sendQuantityWarning(product);
                }
                if (product.quantity == 1)
                {
                    AdminInbox.sendEmptyInventoryWarning(product);
                }            
           
        }
        static void ExtractProductNames(string cartlist)
        {
            // Split the input string into lines
            string[] lines = cartlist.Split('\n');

            // Extract product names and print them
            foreach (string line in lines)
            {
                string productName = GetProductName(line);
                Database.addProductToOrders(productName);
            }
        }

        static string GetProductName(string input)
        {
            // Split the input string by space and get the last part
            string[] parts = input.Split(' ');
            string productName = parts[parts.Length - 1];

            return productName;
        }

    }
}
