using System;
using System.IO;

namespace SPL_PROJECT
{
    interface ILogInManager
    {
        void logIn();
    }
    public class AdminLogin : ILogInManager
    {
        public void logIn()
        {
            Console.WriteLine("Enter password:");
            string pass = Console.ReadLine();

            string admin_file = @"C:\ShopMate\adminpassword.txt";
            if (File.Exists(admin_file))
            {
                StreamReader sr = new StreamReader(admin_file);
                string line;
                line = sr.ReadLine();
                sr.Close();

                if (line == pass)
                {
                    Console.WriteLine("Logged in as admin");
                    Admin admin = new Admin();
                    admin.dashboard();
                }
                else
                {
                    Console.WriteLine("Wrong password. Back to main menu? (Y/N)");
                    line = Console.ReadLine();
                    if (line == "N")
                    {
                        logIn();
                    }
                    else
                    {
                        utility.mainMenu();
                        return;
                    }
                }
            }
        }
    }
    public class CustomerLogIn : ILogInManager
    {
        public void logIn()
        {
            Console.WriteLine("Enter UserName:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();
            if (Database.DoesUserExist(username))
            {
                foreach (user Temp_user in Database.userList)
                {
                    if (Temp_user.userName == username && password == Temp_user.password)
                    {
                        Temp_user.dashboard();
                    }
                    else if (Temp_user.userName == username && password != Temp_user.password)
                    {

                        Console.WriteLine("Wrong password. Back to main menu? (Y/N)");
                        string line = Console.ReadLine();
                        if (line == "N")
                        {
                            logIn();
                        }
                        else
                        {
                            utility.mainMenu();
                            return;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Username.");
            }
        }
    }
}
