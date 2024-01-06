using System;

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

        public void dashboard()
        {
            string s = $"------------------Logged In As {Session.CurrentUser.name}----------------------";
            cart = Database.getCart(userName);
            string[] userDashboardOption = { "Browse Products", "Edit Profile", "Inbox", "Cart", "Log Out" };
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
                    Console.WriteLine("--------------Did Not Implement Edit Profile------------------");
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
                    IProduct product=null;
                    bool status = checkoutStatus(ref product);
                    if (status)
                    {
                        cart.confirmOrder();
                        Console.WriteLine("Ordered Successfully!\n\nPress any key to visit dashboard.");
                        inbox.sendPurchaseMessage(userName);
                        inbox.emptyList();
                        Database.GetInbox(Session.CurrentUser);
                        Console.ReadKey();
                        dashboard();
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Product {product.id} is out of Stock");
                        loadCart();
                        return;
                    }
                case 1:
                    loadCart();
                    break; 
                default:
                    Console.WriteLine("Invalid input."); loadCart();
                    break;

            }
        }

        public bool checkoutStatus(ref IProduct CheckProduct)
        {
            foreach (IProduct product in cart.products) 
            {
                if(product.quantity == 0)
                {
                    CheckProduct=product;
                    return false;
                }
            }
           
            return true;
        }
    }
}
