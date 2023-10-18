using System;
using System.IO;

namespace SPL_PROJECT
{
    public static class utility
    {
        public static Database db = new Database();
        static utility()
        {
            loadDatabase();
        }
        public static void loadDatabase()
        {
            loadUser();
        }
        public static void mainMenu()
        {
            int inp = 0;
            Console.WriteLine("Press 1 to Create New Account");
            Console.WriteLine("Press 2 to Sign In to Existing Account");
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
                default:
                    Console.WriteLine("Invalid input.");
                    mainMenu();
                    break;
            }
        }
        public static void loadUser()
        {
            string user_file = @"C:\ShopMate\user.txt.txt";
            if (File.Exists(user_file))
            {
                StreamReader sr = new StreamReader(user_file);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = null;
                    s = line.Split(',');

                    string userName = s[0];
                    string name = s[1];
                    string pass = s[2];
                    string mail = s[3];
                    DateTime date = Convert.ToDateTime(s[4]);
                    user tempUser = new user(userName, name, pass, mail, date);
                    db.userList.Add(tempUser);
                }

                sr.Close();
            }
        }
        public static void createAcc()
        {
            Console.WriteLine("Enter UserName:");
            string username = Console.ReadLine();
            if (userExist(username))
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

            db.CreateUser(username, Name, password, mail, date);
        }
        public static void signIN()
        {
            Console.WriteLine("Enter UserName:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();
            if (userExist(username))
            {
                foreach (user Temp_user in db.userList)
                {
                    if (Temp_user.userName == username && password == Temp_user.password)
                    {
                        //login
                    }
                    else if (Temp_user.userName == username && password != Temp_user.password)
                    {

                        Console.WriteLine("Invalid Password.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Username.");
            }

        }
        public static bool userExist(string username)
        {
            foreach (user Temp_user in db.userList)
            {
                if (Temp_user.userName == username)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
