using System;

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
            int input = 0;
            Console.WriteLine("Enter 1 to Browse Products");
            //Call load products and print all in console
            Console.WriteLine("Enter 2 to Edit Profile");
            Console.WriteLine("Enter 3 to see your Cart");
            Console.WriteLine("Enter 4 to log out");
            //Load Cart
            //Edit Profile
            //Load orders
            //Browse Products
            try
            {
                input = int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            switch (input)
            {
                case 1:
                    Database.loadProducts();
                    break;
                case 2:
                    Console.WriteLine("edit profile didn't implemented");
                    break;
                case 3:
                    cart.showCart();
                    Console.WriteLine("Enter any key to go back");
                    Console.ReadKey();
                    dashboard();
                    break;
                case 4:
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
