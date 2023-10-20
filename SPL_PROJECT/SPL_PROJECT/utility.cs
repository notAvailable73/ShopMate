using System;
using System.IO;

namespace SPL_PROJECT
{
    public static class utility
    {

        public static void mainMenu()
        {
            int inp = 0;
            Console.WriteLine("Press 1 to Create New Account");
            Console.WriteLine("Press 2 to Sign In to Existing Account");
            Console.WriteLine("Press 3 to log In as admin");
            try
            {
                inp = int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            switch (inp)
            {
                case 1: createAcc(); break;
                case 2:
                    ILogInManager customerlogInManager = new CustomerLogIn();
                    customerlogInManager.logIn();
                    break;
                case 3:
                    ILogInManager adminlogInManager = new AdminLogin();
                    adminlogInManager.logIn();
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
            }
            Console.WriteLine("Enter Name:");
            string Name = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter E-mail:");
            string mail = Console.ReadLine();

            Console.WriteLine("Enter Date of Birth");
            string date_of_birth = Console.ReadLine();
            DateTime date = Convert.ToDateTime(date_of_birth);

            user Current_User = Database.CreateUser(username, Name, password, mail, date);
            Current_User.dashboard();
        }

    }
}
