using System;
using System.IO;

namespace ShopMate
{
    public class AccountCreator
    {
        public void createAcc()
        {
            Console.Clear();

            Console.WriteLine("Enter UserName:");
            string username = Console.ReadLine();
            if (utility.DoesUserExist(username))
            {
                Console.WriteLine("Username already exist. Try a new userName.");
                Console.WriteLine();
                Console.ReadKey();
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
            if (!utility.IsValidEmail(mail))
            {
                Console.WriteLine("This mail is not valid. Try again!.");
                Console.WriteLine();
                Console.ReadKey();
                createAcc();
                return;
            }
            if (utility.isEmailUsed(mail))
            {
                Console.WriteLine("This mail is already used. Try again!.");
                Console.WriteLine();
                Console.ReadKey();
                createAcc();
                return;
            }
            Console.WriteLine("Enter your present adress");
            string adress = Console.ReadLine();
            Console.WriteLine("Enter Date of Birth(DD-MM-YYYY)");
            string date = Console.ReadLine();
            Console.Clear();

            // Insert new user info into user file
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\userInfos\\user.txt");
            string info = $"{username},{Name},{password},{mail},{adress},{date}\n";
            File.AppendAllText(path, info);
            Console.WriteLine($"User Created Successfully with username:{username}");

            // Create new cart for him
            path = Path.Combine(baseDirectory, $"Database\\cart\\{username}_cart.txt");
            StreamWriter sw = File.CreateText(path);
            sw.Close();
            // Create new inbox for him
            path = Path.Combine(baseDirectory, $"Database\\inbox\\{username}inbox.txt");
            StreamWriter sw2 = File.CreateText(path);
            sw2.Close();
            // Create new orderList for him
            path = Path.Combine(baseDirectory, $"Database\\inbox\\{username}_Orders.txt");
            StreamWriter sw3 = File.CreateText(path);
            sw2.Close();

        }
    }
}
