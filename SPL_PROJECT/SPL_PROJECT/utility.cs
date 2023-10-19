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
                case 2: signIN(); break;
                case 3: AdminLogIn(); break;
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
        public static void signIN()
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
                            signIN();
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
        public static void AdminLogIn()
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
                        AdminLogIn();
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
}
