using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopMate
{
    public static class utility
    {
        public static void mainMenu()
        {
            string[] mainmenuOptions = { "Sign Up", "Sign In", "Admin Sign In", "Quit" };

            Menu menu = new Menu(mainmenuOptions);

            int inp = menu.Run();

            switch (inp)
            {
                case 0:
                    Console.Clear(); 
                    AccountCreator accountCreator = new AccountCreator();
                    accountCreator.createAcc();
                    return;
                case 1:
                    sessionManager UserSession = new sessionManager();
                    UserSession.userLogIn();
                    break;
                case 2:
                    sessionManager AdminSession = new sessionManager();
                    AdminSession.AdminLogin();
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
        //public static void createAcc()
        //{


        //    Console.WriteLine("Enter UserName:");
        //    string username = Console.ReadLine();
        //    //if (Database.DoesUserExist(username))
        //    //{
        //    //    Console.WriteLine("Username already exist. Try a new userName.");

        //    //    createAcc();
        //    //    return;
        //    //}
        //    Console.WriteLine("Enter Name:");
        //    string Name = Console.ReadLine();

        //    //Console.WriteLine("Enter Password:");
        //    string password = utility.EncryptPassword();
        //    Console.WriteLine();
        //    password = utility.hashing(password);

        //    Console.WriteLine("Enter E-mail:");
        //    string mail = Console.ReadLine();

        //    Console.WriteLine("Enter Date of Birth(DD-MM-YYYY)");
        //    string date = Console.ReadLine();
        //    Console.Clear();

        //    //user Current_User = Database.CreateUser(username, Name, password, mail, date);
        //    //Console.WriteLine();
        //    //Database.createOrder(username);
        //    //inbox newUser = new inbox();
        //    //newUser.SendWelcomeMessage(Current_User.userName);
        //    //utility.mainMenu();
        //}

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

        public static void ExtractProductNames(string cartlist)
        {
            string[] lines = cartlist.Split('\n');

            // Extract product names and print them
            foreach (string line in lines)
            {
                string productName = GetProductName(line);
                //Database.addProductToOrders(productName);
            }
        }

        public static string GetProductName(string input)
        {
            // Split the input string by space and get the last part
            string[] parts = input.Split(' ');
            string productName = parts[parts.Length - 1];

            return productName;
        }
        public static bool DoesUserExist(string username)
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
                    if (userName == username)
                    {
                        sr.Close();
                        return true;
                    }
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public static bool isEmailUsed(string mailadress)
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
                    if (mail == mailadress)
                    {
                        sr.Close ();
                        return true;
                    }
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public static bool IsValidEmail(string email)
        {
            // Define a regular expression for a simple email validation
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            // Use Regex.IsMatch to check if the email matches the pattern
            return Regex.IsMatch(email, pattern);
        } 
        public static List<Product> getProductList(string path)
        {
            List<Product> products = new List<Product>();
            try
            {
                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    string id = userParts[0];
                    string productName = userParts[1];
                    double price = Convert.ToDouble(userParts[2]);
                    int quantity = Convert.ToInt32(userParts[3]);
                    string productDescription = userParts[4];
                    products.Add(new Product(id,productName,price,quantity,productDescription));
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
            }
            return products;
        }
    }
}
