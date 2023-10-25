using System;
using System.Data;

namespace SPL_PROJECT
{
    public class user : IAccount
    {
        public string userName;
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string date_of_birth { get; set; }
        public Cart cart;
        public user(string userName, string name, string password, string email, string date)
        {
            this.userName = userName;
            this.name = name;
            this.password = password;
            this.email = email;
            this.date_of_birth = date;
            cart = Database.getCart(userName);
        }

        public void dashboard()
        {
            string[] userDashboardOption = { "Browse Products", "Edit Profile", "Cart", "Log Out" };
            Menu menu = new Menu(userDashboardOption);

            int input = menu.Run();

            //Call load products and print all in console
            //Load Cart
            //Edit Profile
            //Load orders
            //Browse Products
            switch (input)
            {
                case 0:
                    Console.Clear();
                    Database.browseProduct(userName);
                    dashboard();
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("--------------Did Not Implement Edit Profile------------------");
                    dashboard();
                    break;
                case 2:
                    //cart.showCart();
                    Console.Clear();
                    Console.WriteLine("---------------cart not implemented.--------------");
                    Console.WriteLine("------------Enter any key to go back--------------");
                    Console.ReadKey();
                    dashboard();
                    break;
                case 3:
                    Console.Clear();
                    logOut();
                    break;
                default:
                    Console.WriteLine("Invalid input."); dashboard();
                    break;

            }
        }

        public void logOut()
        {
            Console.WriteLine("logged out successfully");
            utility.mainMenu();
            return;
        }
        public void addToCart(IProduct product)
        {
            cart.AddProductToCart(userName, product);
        }
    }
}
