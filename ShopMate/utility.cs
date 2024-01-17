using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
                    Console.ReadKey();
                    mainMenu();
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
        public static void Intro()
        {
            Console.WriteLine(GenerateShopMateArt());
            Console.WriteLine();
            Console.Write("\t\t\t\t\t\t    ");
            string Introduction1 = "Welcome to ShopMate – Your Ultimate E-Commerce Experience!\n";
            PrintLettersOneByOne(Introduction1);
            Console.Write("\t\t\t\t\t\t\t\t    ");
            Console.WriteLine("PRESS ENTER TO OPEN THE APP");
            Console.ReadKey(true);
        }
        static string GenerateShopMateArt()
        {
            string[] artLines = {
            "\t\t\t\t\t *****    *    *    ***     ****     *       *       *      *******   *******",
            "\t\t\t\t\t*         *    *   *   *    *   *    **     **      * *        *      * ",
            "\t\t\t\t\t*         *    *   *   *    *   *    * *   * *     *   *       *      * ",
            "\t\t\t\t\t ****     ******   *   *    ****     *  * *  *    *     *      *      ******* ",
            "\t\t\t\t\t     *    *    *   *   *    *        *   *   *    *******      *      * ",
            "\t\t\t\t\t     *    *    *   *   *    *        *       *    *     *      *      *  " ,
            "\t\t\t\t\t *****    *    *    ***     *        *       *    *     *      *      *******",
            "\t\t\t\t\t-------------------------------------------------------------------------------"
        };


            /*
      















    
    

     
     


             */

            return string.Join(Environment.NewLine, artLines);
        }
        static void PrintLettersOneByOne(string input)
        {
            foreach (char letter in input)
            {
                Console.Write(letter);
                Thread.Sleep(50);
            }
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
            bool contains = false;
            // if the email contains '@' symbol
            foreach (char c in email)
            {
                if (c == '@') contains = true;
            }
            if (!contains) return false;

            // if all characters are in lowercase
            foreach (char c in email)
            {
                if (c >= 'A' && c <= 'Z') return false;
            }

            // if the email ends with a valid domain suffix
            string[] validDomainSuffixes = { ".com", ".edu", ".org", ".net" };
            bool isValidDomain = false;

            int dotIndex = email.Length - 4;
            if (email[dotIndex] != '.') { return false; }

            if (email[email.Length - 1] == 'm' && email[email.Length - 2] == 'o' && email[email.Length - 3] == 'c') isValidDomain = true;
            else if (email[email.Length - 1] == 'u' && email[email.Length - 2] == 'd' && email[email.Length - 3] == 'e') isValidDomain = true;
            else if (email[email.Length - 1] == 'g' && email[email.Length - 2] == 'r' && email[email.Length - 3] == 'o') isValidDomain = true;
            else if (email[email.Length - 1] == 't' && email[email.Length - 2] == 'e' && email[email.Length - 3] == 'n') isValidDomain = true;
            else isValidDomain = false;

            return isValidDomain;
        }
        
    }
}
