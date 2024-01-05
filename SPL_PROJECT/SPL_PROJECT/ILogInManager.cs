using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

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
            Console.WriteLine("-----------------Admin Login-------------------");
            Console.WriteLine();
            Console.WriteLine("Enter password:");
            string admin_file = @"C:\ShopMate\adminpassword.txt";
            string pass = utility.EncryptPassword();
            Console.WriteLine();
            pass= utility.hashing(pass);


            if (File.Exists(admin_file))
            {
                StreamReader sr = new StreamReader(admin_file);
                string line;
                line = sr.ReadLine();

                sr.Close();
                //Console.WriteLine($"{pass} {line}");

                if (line == pass)
                {
                    Console.Clear();
                    Console.WriteLine("------------------Logged in as admin-----------------");
                    Console.WriteLine();
                    Admin admin = new Admin();
                    admin.dashboard();
                }
                else
                {
                    //Console.WriteLine("Wrong password. Back to main menu?");
                    string[] Options = { "Yes", "No" };

                    Menu menu = new Menu(Options);

                    int inp = menu.Run("Wrong password. Try Again?");

                    switch (inp)
                    {
                        case 0:
                            Console.Clear();
                            logIn();
                            break;
                        case 1:
                            Console.Clear();
                            utility.mainMenu();
                            break;
                        default:
                            Console.WriteLine("Invalid input.");

                            break;
                    }
                }
            }
        }
    }
    public class CustomerLogIn : ILogInManager
    {
        public void logIn()
        {
            Console.Clear();
            Console.WriteLine("-----------------------User Login----------------------");
            Console.WriteLine("Enter UserName:");
            string username =  Console.ReadLine().ToLower();

            Console.WriteLine("Enter Password:");
            string password = utility.EncryptPassword();
            Console.WriteLine();
            password= utility.hashing(password);

            if (Database.DoesUserExist(username))
            {
                foreach (user Temp_user in Database.userList)
                {
                    if (Temp_user.userName == username && password == Temp_user.password)
                    {
                        Console.Clear();
                        string s = $"------------------Logged In As {username}----------------------";
                        Console.WriteLine(s);
                        Console.WriteLine();
                        Session.CurrentUser = Temp_user;
                        Session.CurrentUser.dashboard();
                    }
                    else if (Temp_user.userName == username && password != Temp_user.password)
                    {
                        
                        Console.WriteLine("Wrong password. Back to main menu? (Y/N)");
                        string line = Console.ReadLine();
                        if (line == "N"||line=="n")
                        {
                            logIn();
                        }
                        else
                        {
                            Console.Clear();
                            utility.mainMenu();
                            return;
                        }
                    }
                }
            }
            
            else
            {
                Console.WriteLine("Invalid Username. Press any key to Continue...");
                Console.ReadKey();
                string[] Options = { "Yes", "No" };

                Menu menu = new Menu(Options);

                int inp = menu.Run("Try again?");

                switch (inp)
                {
                    case 0:
                        Console.Clear();
                        logIn();
                        break;
                    case 1:
                        Console.Clear();
                        utility.mainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        
                        break;
                }
            }
        }
        public void logIn(string username, string password)
        {
            password = utility.hashing(password);
            if (Database.DoesUserExist(username))
            {
                foreach (user Temp_user in Database.userList)
                {
                    if (Temp_user.userName == username && password == Temp_user.password)
                    {
                        Console.Clear();
                        Console.WriteLine($"------------------Logged In As {username}----------------------");
                        Console.WriteLine();
                        Temp_user.dashboard();
                    }
                    else if (Temp_user.userName == username && password != Temp_user.password)
                    {

                        Console.WriteLine("Wrong password. Try again? ");
                        string[] Options = { "Yes", "No" };

                        Menu menu = new Menu(Options);

                        int inp = menu.Run("Wrong password. Try again? ");

                        switch (inp)
                        {
                            case 0:
                                Console.Clear();
                                logIn();
                                break;
                            case 1:
                                Console.Clear();
                                utility.mainMenu();
                                break;
                            default:
                                Console.WriteLine("Invalid input.");

                                break;
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
