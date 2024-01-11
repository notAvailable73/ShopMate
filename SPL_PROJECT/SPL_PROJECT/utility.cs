using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SPL_PROJECT
{
    public static class utility
    {
        public static void mainMenu()
        {
            string[] mainmenuOptions = { "Sign Up", "Sign In", "Admin Sign In", "Quit" };

            Menu menu=new Menu(mainmenuOptions);

            int inp = menu.Run();

            switch (inp)
            {
                case 0: Console.Clear(); createAcc();
                    break;
                case 1:
                    ILogInManager customerlogInManager = new CustomerLogIn();
                    customerlogInManager.logIn();
                    break;
                case 2:
                    ILogInManager adminlogInManager = new AdminLogin();
                    adminlogInManager.logIn();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    mainMenu();
                    break;
            }
        }
        public static void createAcc()
        {


            Console.WriteLine("Enter UserName:");
            string username = Console.ReadLine();
            if (Database.DoesUserExist(username))
            {
                Console.WriteLine("Username already exist. Try a new userName.");

                createAcc();
                return;
            }
            Console.WriteLine("Enter Name:");
            string Name = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = utility.EncryptPassword();
            Console.WriteLine();
            password = utility.hashing(password);

            Console.WriteLine("Enter E-mail:");
            string mail = Console.ReadLine();

            Console.WriteLine("Enter Date of Birth(DD-MM-YYYY)");
            string date = Console.ReadLine();
            Console.Clear();

            user Current_User = Database.CreateUser(username, Name, password, mail, date);
            Console.WriteLine();
            Database.createOrder(username);
            inbox newUser = new inbox();
            newUser.SendWelcomeMessage(Current_User.userName);
            utility.mainMenu();
        }

        public static string EncryptPassword()
        {
            string password = "";
            char key;

            do
            {
                key = Console.ReadKey(true).KeyChar;
                if (key != '\b' && key != '\r')
                {
                    password += key;
                    Console.Write("*");
                }
                else if (key == '\b' && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key != '\r');
            return password;
        }

        public static string hashing(string password)
        {
            char[] passwordArray = password.ToCharArray();

            for (int i = 0; i < passwordArray.Length; i++)
            {
                passwordArray[i] = (char)(passwordArray[i] + 5);
            }

            string hashedPassword = new string(passwordArray);

            return hashedPassword;
        }

        
    }
}
