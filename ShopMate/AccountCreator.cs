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
            Console.WriteLine("Enter Date of Birth(YYYY-MM-DD)");
            string date  = Console.ReadLine();
            try
            { 
                DateTime demoDate = Convert.ToDateTime(date);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message+"\nTry again from beginning.");
                Console.ReadKey(true);
                createAcc();
                return;
            } 
            Console.Clear();


            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\userInfos\\user.txt");
            string info = $"{username},{Name},{password},{mail},{adress},{date}\n";
            File.AppendAllText(path, info);
            Console.WriteLine($"User Created Successfully with username:{username}");


            path = Path.Combine(baseDirectory, $"Database\\cart\\{username}_cart.txt");
            StreamWriter sw = File.CreateText(path);
            sw.Close();

            path = Path.Combine(baseDirectory, $"Database\\inbox\\{username}inbox.txt");
            StreamWriter sw2 = File.CreateText(path);
            sw2.Close();

            Messenger welcomeMessageSender = new Messenger();
            string msg = $"Hello, {Name}.Welcome to ShopMate. Enjoy your shopping with us.";
            welcomeMessageSender.sendMessage(username, msg);

            path = Path.Combine(baseDirectory, $"Database\\OrderInfo\\{username}_Orders.txt");
            StreamWriter sw3 = File.CreateText(path);
            sw3.Close();

        }
    }
}
