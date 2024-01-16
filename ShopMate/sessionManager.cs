using System;
using System.IO;

namespace ShopMate
{
    public class sessionManager
    {
        public static IAccount CurrentAccount;
        public void AdminLogin()
        {
            Console.WriteLine("-----------------Admin Login-------------------");
            Console.WriteLine();
            Console.WriteLine("Enter password:");
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, "Database\\userInfos\\adminPass.txt");
            string pass = utility.EncryptPassword();
            Console.WriteLine();
            pass = utility.hashing(pass);


            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                string line;
                line = sr.ReadLine();
                sr.Close();

                if (line == pass)
                {
                    Console.Clear();
                    Console.WriteLine("------------------Logged in as admin-----------------");
                    Console.WriteLine();
                    CurrentAccount = new Admin();
                    CurrentAccount.DashBoard();
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
                            AdminLogin();
                            return;
                        case 1:
                            Console.Clear();
                            utility.mainMenu();
                            return;
                        default:
                            Console.WriteLine("Invalid input.");

                            return;
                    }
                }
            }
        }
        public void userLogIn()
        {
            Console.Clear();
            Console.WriteLine("-----------------------User Login----------------------");
            Console.WriteLine("Enter UserName:");
            string username = Console.ReadLine().ToLower();
            Console.WriteLine("Enter Password:");
            string password = utility.EncryptPassword();
            Console.WriteLine();
            password = utility.hashing(password);
            if (utility.DoesUserExist(username))
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(baseDirectory, $"Database\\userInfos\\user.txt");

                try
                {
                    StreamReader sr = new StreamReader(path);
                    string line;


                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] s = null;
                        s = line.Split(',');

                        string userName = s[0];
                        string name = s[1];
                        string pass = s[2];
                        string mail = s[3];
                        string adress = s[4];
                        string date = s[5];
                        User Temp_user = new User(userName, name, pass, mail, adress, date);
                        if (Temp_user.userName == username && password == Temp_user.password)
                        {
                            sr.Close();
                            CurrentAccount = Temp_user;
                            Console.Clear();
                            Console.WriteLine($"------------------Logged In As {username}----------------------");
                            CurrentAccount.DashBoard();
                            return; 
                        }
                        /// password is wrong

                        else if (Temp_user.userName == username && password != Temp_user.password)
                        {
                            sr.Close();
                            string[] Options = { "Yes", "No" };
                            Menu menu = new Menu(Options);
                            int inp = menu.Run("Wrong password. Try again?");
                            switch (inp)
                            {
                                case 0:
                                    Console.Clear();
                                    userLogIn();
                                    return;
                                case 1:
                                    Console.Clear();
                                    utility.mainMenu();
                                    return;
                                default:
                                    Console.WriteLine("Invalid input.");
                                    return;
                            }
                        }

                    }


                    sr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            /// username is wrong

            else
            {
                string[] Options = { "Yes", "No" };
                Menu menu = new Menu(Options);
                int inp = menu.Run("Invalid Username. Press any key to Continue...");

                switch (inp)
                {
                    case 0:
                        Console.Clear();
                        userLogIn();
                        return;
                    case 1:
                        Console.Clear();
                        utility.mainMenu();
                        return;
                    default:
                        Console.WriteLine("Invalid input.");
                        return;
                }
            }
        }
    }
}
