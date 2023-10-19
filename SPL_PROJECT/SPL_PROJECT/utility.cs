using System;
using System.Collections.Concurrent;
using System.IO;

namespace SPL_PROJECT
{
    public static class utility
    {
        
        static utility()
        {
            loadDatabase();
        }
        public static void loadDatabase()
        {
            loadUser();
            loadClothingProduct();
            loadElectronicProduct();
            loadHomeApplienceProduct();
        }
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

        public static void AdminLogIn()
        {
            Console.WriteLine("Enter password:");
            string pass=Console.ReadLine();

            string admin_file = @"C:\ShopMate\adminpassword.txt";
            if(File.Exists(admin_file))
            {
                StreamReader sr = new StreamReader(admin_file);
                string line;
                line= sr.ReadLine();
                sr.Close();

                if(line==pass)
                {
                    Database.current_admin=new Admin();
                    Console.WriteLine("Logged in as admin");
                    adminDashboard();
                }
            }
        }

        public static void adminDashboard()
        {
            Console.WriteLine("Enter 1 to add product");
            Console.WriteLine("Enter 2 to Log out");

            int input = 0;

            try
            {
                input = int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            switch(input)
            {
                case 1: Database.current_admin.AddProduct(); break;
                case 2: Database.current_admin=null; mainMenu(); break;
                default : Console.WriteLine("Invalid Input"); break;
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
                    Database.userList.Add(tempUser);
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

            user Current_User= Database.CreateUser(username, Name, password, mail, date);

            Dashboard(Current_User);

        }
        public static void signIN()
        {
            Console.WriteLine("Enter UserName:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();
            if (userExist(username))
            {
                foreach (user Temp_user in Database.userList)
                {
                    if (Temp_user.userName == username && password == Temp_user.password)
                    {
                        Dashboard(Temp_user);
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
            foreach (user Temp_user in Database.userList)
            {
                if (Temp_user.userName == username)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Dashboard(user Current_User)
        {
            int input=0;
            Console.WriteLine("Enter 1 to Browse Products");
            //Call load products and print all in console
            Console.WriteLine("Enter 2 to Edit Profile");
            //Load Cart
            //Edit Profile
            //Load orders
            //Browse Products
            try
            {
                input = int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            switch(input) 
            {
                case 1:
                    loadproducts();
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    Dashboard(Current_User);
                    break;
            }

        }

        public static void loadElectronicProduct()
        {
            string product_file = @"C:\ShopMate\electonicproduct.txt";
            if (File.Exists(product_file))
            {
                StreamReader sr = new StreamReader(product_file);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = null;
                    s = line.Split(',');

                    int id = Convert.ToInt32(s[0]);
                    string name = s[1];
                    double price = Convert.ToDouble(s[2]);
                    string description = s[3];
                    
                    ElectronicProducts temp = new ElectronicProducts(id, name, price, description);
                    Database.ElectronicProducts.Add(temp);
                }

                sr.Close();
            }
        }

        public static void loadClothingProduct()
        {
            string product_file = @"C:\ShopMate\clothingproduct.txt";
            if (File.Exists(product_file))
            {
                StreamReader sr = new StreamReader(product_file);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = null;
                    s = line.Split(',');

                    int id = Convert.ToInt32(s[0]);
                    string name = s[1];
                    double price = Convert.ToDouble(s[2]);
                    string description = s[3];

                    Cloth temp = new Cloth(id, name, price, description);
                    Database.cloths.Add(temp);
                }

                sr.Close();
            }
        }

        public static void loadHomeApplienceProduct()
        {
            string product_file = @"C:\ShopMate\homeapplienceproduct.txt";
            if (File.Exists(product_file))
            {
                StreamReader sr = new StreamReader(product_file);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = null;
                    s = line.Split(',');

                    int id = Convert.ToInt32(s[0]);
                    string name = s[1];
                    double price = Convert.ToDouble(s[2]);
                    string description = s[3];

                    HomeAppliences temp = new HomeAppliences(id, name, price, description);
                    Database.HomeAppliences.Add(temp);
                }

                sr.Close();
            }
        }

        public static void loadproducts()
        {
            Console.WriteLine("Electronic Products:");

            foreach(ElectronicProducts ep in Database.ElectronicProducts) 
            {
                Console.WriteLine($"{ep.id}\t{ep.name}\t{ep.price}\t{ep.description}");
            }

            Console.WriteLine("Clothing Products:");

            foreach (Cloth cloth in Database.cloths)
            {
                Console.WriteLine($"{cloth.id}\t{cloth.name}\t{cloth.price}\t{cloth.description}");
            }

            Console.WriteLine("HomeApplience Products:");

            foreach (HomeAppliences ha in Database.HomeAppliences)
            {
                Console.WriteLine($"{ha.id}\t{ha.name}\t{ha.price}\t{ha.description}");
            }
        }

    }
}
