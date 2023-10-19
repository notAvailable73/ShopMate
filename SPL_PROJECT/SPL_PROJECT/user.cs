using System;

namespace SPL_PROJECT
{
    public class user : IAccount
    {
        public string userName;
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime date_of_birth { get; set; }
        public Cart cart;
        public user(string userName,string name, string password, string email, DateTime date)
        {
            this.userName = userName;
            this.name = name;
            this.password = password;
            this.email = email;
            this.date_of_birth = date;
            cart = new Cart();
        }

        public void signIN()
        {
            
        }

        public void dashboard()
        {
            int input = 0;
            Console.WriteLine("Enter 1 to Browse Products");
            //Call load products and print all in console
            Console.WriteLine("Enter 2 to Edit Profile");
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

            //switch (input)
            //{
            //    case 1:
            //        loadproducts();
            //        break;
            //    default:
            //        Console.WriteLine("Invalid Input");
            //        Dashboard(Current_User);
            //        break;
            //}
        }

        public void logOut()
        {
            Console.WriteLine("logged out successfully");
            utility.mainMenu();
            return;
        }
    }
}
