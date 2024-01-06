using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SPL_PROJECT
{
    public static class Database
    {
        public static List<user> userList = new List<user>();
        public static List<ElectronicProducts> ElectronicProductList = new List<ElectronicProducts>();
        public static List<Cloth> clothList = new List<Cloth>();
        public static List<HomeAppliences> HomeApplienceList = new List<HomeAppliences>();

        public static user CreateUser(string username, string name, string password, string mail, string date)
        {
            user newUser = new user(username, name, password, mail, date);
            string user_file = @"C:\ShopMate\user.txt.txt";
            string info = $"{username},{name},{password},{mail},{date}\n";
            File.AppendAllText(user_file, info);
            userList.Add(newUser);
            Console.WriteLine($"User Created Successfully with username:{username}");
            return newUser;
        }

        public static void DeleteUser(user u)
        {
            foreach(user user in userList) 
            {
                if(user.userName == u.userName)
                {
                    userList.Remove(user);
                    break;
                }
            }

            string path = $@"C:\ShopMate\user.txt.txt";
            StreamReader sr = new StreamReader(path);
            string line;
            string info = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] infoOfUser = line.Split(',');
                if (infoOfUser[0]==u.userName)
                {
                    continue;
                }
                info += $"{line}\n";
            }
            sr.Close();
            File.WriteAllText(path, info);
        }
        public static void addProduct(IProductAdder adder)
        {
            string name, description;
            double price = 0;
            int quantity = 0;

            Console.WriteLine("Enter Name of the product:");
            name = Console.ReadLine();

            Console.WriteLine("Enter Price of the product");
            try
            {
                price = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input.");
                addProduct(adder);
            }

            Console.WriteLine("Enter Quantity of the product");
            try
            {
                quantity = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input.");
                addProduct(adder);
            }

            Console.WriteLine("Enter Description of the product");
            description = Console.ReadLine();

            adder.addProduct(name, price, description, quantity);

        }
        public static IProduct getProduct(int id)
        {
            IProduct product = null;
            foreach (ElectronicProducts item in ElectronicProductList)
            {
                if (item.id == id)
                {
                    product = item;
                }
            }
            foreach (Cloth item in clothList)
            {
                if (item.id == id)
                {
                    product = item;
                }
            }
            foreach (HomeAppliences item in HomeApplienceList)
            {
                if (item.id == id)
                {
                    product = item;

                }
            }
            return product;
        }
        public static bool DoesUserExist(string username)
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
        public static void createCart(string userName)
        {
            string path = $@"C:\ShopMate\Carts\{userName}_cart.txt";
            StreamWriter sw = File.CreateText(path);
            sw.Close();
        }

        public static void createInbox(string userName)
        {
            string path = $@"C:\ShopMate\Inbox\{userName}_inbox.txt";
            StreamWriter sw = File.CreateText(path);
            sw.Close();
        }
        public static void addProductToCart(IProduct product)
        {
            string path = $@"C:\ShopMate\Carts\{Session.CurrentUser.userName}_cart.txt";
            string info = $"{product.id}\n";
            File.AppendAllText(path, info);
        }
        public static void deleteProductFromCart(string productId)
        {
            string path = $@"C:\ShopMate\Carts\{Session.CurrentUser.userName}_cart.txt";
            StreamReader sr = new StreamReader(path);
            string line;
            string info = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (productId == line)
                {
                    continue;
                }
                info += $"{line}\n";
            }
            sr.Close();
            File.WriteAllText(path, info);

        }
        public static void clearCart()
        {
            string path = $@"C:\ShopMate\Carts\{Session.CurrentUser.userName}_cart.txt";
            File.WriteAllText(path, String.Empty);

        }
        public static Cart getCart(string userName)
        {
            string path = $@"C:\ShopMate\Carts\{userName}_cart.txt";
            if (!File.Exists(path))
            {
                createCart(userName);
            }
            Cart newCart = new Cart();
            StreamReader sr = new StreamReader(path);
            string line;

            List<int> productId = new List<int>();

            while ((line = sr.ReadLine()) != null)
            {
                int id=Convert.ToInt32(line);
                productId.Add(id);
            }

            productId.Sort();

            foreach(int id in productId)
            {
                IProduct product = getProduct(id);

                newCart.AddProductToThisCart(product);
            }

            sr.Close();
            return newCart;
        }
        public static void browseProduct()
        {
            string[] browseProductOptions = { "Electronic Products", "Clothing products", "Home Appliences", "Go Back to dashboard" };
            Menu menu = new Menu(browseProductOptions);
            int inp = menu.Run();             
            IProductDisplay productDisplay;
            Console.Clear();
            switch (inp)
            {
                case 0:
                    productDisplay = new ElectronicProductDisplay(); 
                    break;

                case 1:
                    productDisplay = new ClothingProductDisplay();  
                    break;

                case 2:
                    productDisplay = new HomeApplienceProductDisplay();  
                    break;
                case 3:
                    Session.CurrentUser.dashboard();
                    return;
                default: return;
            }
            productDisplay.DisplayProducts();
        }

        public static void changePassword()
        {
            string userName = Session.CurrentUser.userName;
            string name = Session.CurrentUser.name; 
            string password = Session.CurrentUser.password;
            string email = Session.CurrentUser.email;
            string date_of_birth = Session.CurrentUser.date_of_birth;

            Console.WriteLine("Enter Old PassWord: ");
            string oldPassWord=Console.ReadLine();
            string newPassWord;

            if(utility.hashing(oldPassWord)==password)
            {
                Console.WriteLine("Enter New PassWord: ");
                newPassWord = Console.ReadLine();

                newPassWord=utility.hashing(newPassWord);

                if(newPassWord==password)
                {
                    string s = "Your New PassWord Cannot be same as your Previous PassWord";

                    string[] options = { "Continue" };
                    Menu menu = new Menu(options);
                    int inp = menu.Run(s);
                    Session.CurrentUser.dashboard();
                }
                else
                {
                    DeleteUser(Session.CurrentUser);
                    CreateUser(userName,name,newPassWord,email,date_of_birth);
                    string s = "Password Changed Successfully";

                    string[] options = { "Continue" };
                    Menu menu = new Menu(options);
                    int inp=menu.Run(s);
                    utility.mainMenu();
                }
            }
            else 
            {
                
                string s = "Incorrect PassWord!";

                string[] options = { "Continue" };
                Menu menu = new Menu(options);
                int inp = menu.Run(s);
                Session.CurrentUser.dashboard();
            }

        }

        public static void EditElectronicProductQuantity(IProduct product,int add)
        {
            ElectronicProductList[product.id - 10001].quantity += add;
            int newQuantity = ElectronicProductList[product.id - 10001].quantity;

            string productfile = @"C:\ShopMate\electronicproduct.txt";
            StreamReader sr = new StreamReader(productfile);
            string line;
            string info = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] productInfo = line.Split(',');
                int productId = Convert.ToInt32(productInfo[0]);
                if(productId==product.id) 
                {
                    string newLine= $"{product.id},{product.name},{product.price},{newQuantity},{product.description}\n";
                    info += newLine;
                    continue;
                }
                
                info += $"{line}\n";
            }
            sr.Close();
            File.WriteAllText(productfile, info);
        }

        public static void EditClothingProductQuantity(IProduct product, int add)
        {
            clothList[product.id - 20001].quantity+= add;
            int newQuantity = clothList[product.id - 20001].quantity;

            string productfile = @"C:\ShopMate\clothingproduct.txt";
            StreamReader sr = new StreamReader(productfile);
            string line;
            string info = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] productInfo = line.Split(',');
                int productId = Convert.ToInt32(productInfo[0]);
                if (productId == product.id)
                {
                    string newLine = $"{product.id},{product.name},{product.price},{newQuantity},{product.description}\n";
                    info += newLine;
                    continue;
                }

                info += $"{line}\n";
            }
            sr.Close();
            File.WriteAllText(productfile, info);
        }

        public static void EditHomeProductQuantity(IProduct product,int add)
        {
            HomeApplienceList[product.id - 30001].quantity += add;
            int newQuantity = HomeApplienceList[product.id - 30001].quantity;

            string productfile = @"C:\ShopMate\homeapplienceproduct.txt";
            StreamReader sr = new StreamReader(productfile);
            string line;
            string info = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] productInfo = line.Split(',');
                int productId = Convert.ToInt32(productInfo[0]);
                if (productId == product.id)
                {
                    string newLine = $"{product.id},{product.name},{product.price},{newQuantity},{product.description}\n";
                    info += newLine;
                    continue;
                }

                info += $"{line}\n";
            }
            sr.Close();
            File.WriteAllText(productfile, info);
        }
    }
}
