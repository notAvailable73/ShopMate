using System;
using System.IO;

namespace ShopMate
{
    public class User : IAccount
    {
        public string userName { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string Adress { get; set; }
        public string date_of_birth { get; set; }
        public User(string userName, string name, string password, string email, string adress, string date)
        {
            this.userName = userName;
            this.name = name;
            this.password = password;
            this.email = email;
            this.date_of_birth = date;
            this.Adress = adress;
        }
        public void DashBoard()
        {
            string[] userDashboardOption = { "Browse Products", "Change Password", "Inbox", "Cart", "Previous Orders", "Log Out" };
            Menu menu = new Menu(userDashboardOption);
            int input = menu.Run();
            switch (input)
            {
                case 0:
                    Console.Clear();
                    browse();
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine();
                    changePassword();
                    Console.ReadKey();
                    DashBoard();
                    break;
                case 2:
                    Console.Clear();
                    //loadinbox();
                    break;
                case 3:
                    Console.Clear();
                    loadCart();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine();
                    //loadOrders();
                    break;

                case 5:
                    //Console.Clear();
                    //string cartlist = cart.load();
                    //if (!(cartlist == ""))
                    //{
                    //    inbox.sendRemindProduct(userName);
                    //}
                    logOut();
                    break;
                default:
                    Console.WriteLine("Invalid input."); DashBoard();
                    break;

            }
        }
        public void changePassword()
        {

            Console.WriteLine("Enter Old PassWord: ");
            string oldPassWord = Console.ReadLine();

            if (utility.hashing(oldPassWord) == password)
            {
                Console.WriteLine("Enter New PassWord: ");
                string newPassWord = utility.EncryptPassword();

                newPassWord = utility.hashing(newPassWord);

                if (newPassWord == password)
                {
                    string s = "Your New PassWord Cannot be same as your Previous PassWord.Try again?";

                    string[] Options = { "Yes", "No" };
                    Menu menu = new Menu(Options);
                    int inp = menu.Run(s);

                    switch (inp)
                    {
                        case 0:
                            Console.Clear();
                            changePassword();
                            return;
                        case 1:
                            Console.Clear();
                            DashBoard();
                            return;
                        default:
                            Console.WriteLine("Invalid input.");
                            return;
                    }
                }
                else
                {
                    password = newPassWord;
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string path = Path.Combine(baseDirectory, $"Database\\userInfos\\user.txt");

                    try
                    {
                        string[] lines = File.ReadAllLines(path);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] userParts = lines[i].Split(',');

                            if (userParts.Length > 0 && userParts[0] == userName)
                            {

                                lines[i] = $"{userName},{userParts[1]},{password},{userParts[3]},{userParts[4]},{userParts[5]}";
                            }
                        }

                        File.WriteAllLines(path, lines);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Error updating password in the file: " + ex.Message);
                        Console.ReadLine();
                        changePassword();
                        return;
                    }

                    string s = "Password Changed Successfully";
                    string[] options = { "Continue" };
                    Menu menu = new Menu(options);
                    int inp = menu.Run(s);
                    DashBoard();
                    return;
                }
            }
            else
            {

                string s = "Incorrect PassWord!Try again?";

                string[] Options = { "Yes", "No" };
                Menu menu = new Menu(Options);
                int inp = menu.Run(s);

                switch (inp)
                {
                    case 0:
                        Console.Clear();
                        changePassword();
                        return;
                    case 1:
                        Console.Clear();
                        DashBoard();
                        return;
                    default:
                        Console.WriteLine("Invalid input.");
                        return;
                }
            }

        }
        public void browse()
        {
            string[] browseProductOptions = { "Electronic Products", "Clothing products", "Home Appliences", "Go Back to dashboard" };
            Menu menu = new Menu(browseProductOptions);
            int inp = menu.Run();
            IProductManager productManager;
            Console.Clear();
            switch (inp)
            {
                case 0:
                    productManager = new ElectronicProductsManager();
                    break;

                case 1:
                    productManager = new ClothingProductsManager();
                    break;

                case 2:
                    productManager = new HomeAppliencesManager();
                    break;
                case 3:
                    DashBoard();
                    return;
                default: return;
            }
            int choosenProductIndex;
            do
            {
                choosenProductIndex = productManager.DisplayProducts();
                if (choosenProductIndex == -1)
                {
                    browse();
                    return;
                }
                string choosenProductDescription = productManager.getProductDescription(choosenProductIndex);
                string[] options = { "Add to Cart", "Go Back", "Dashboard" };

                Menu menu1 = new Menu(options);

                inp = menu1.Run(choosenProductDescription);
            } while (!(inp != 0 || inp != 2));
            switch (inp)
            {
                case 0:
                    AddToCart(productManager.getProduct(choosenProductIndex));
                    browse();
                    return;
                case 2:
                    Console.Clear();
                    DashBoard();
                    return;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }
        }
        void AddToCart(Product product)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\cart\\{userName}_cart.txt");
            bool alreadyInCart = false;
            try
            {
                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    string id = userParts[0];
                    string name = userParts[1];
                    string price = userParts[2];
                    string quantity = userParts[3];

                    if (id == product.id)
                    {
                        int newQuantity = Convert.ToInt32(quantity) + 1;
                        quantity = newQuantity.ToString(); alreadyInCart = true;
                        lines[i] = $"{id},{name},{price},{quantity}";
                        File.WriteAllLines(path, lines);
                        break;
                    }

                }
                if (!alreadyInCart)
                {
                    File.AppendAllText(path, $"{product.id},{product.name},{product.price},1\n");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Successfully added to cart");
            Console.ReadLine();
        }
        void logOut()
        {
            utility.mainMenu();
        }
        public void loadCart()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\cart\\{userName}_cart.txt");
            string cartItemPreview = "";

            try
            {
                string[] lines = File.ReadAllLines(path);
                if (lines.Length == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Empty Cart!\nPress any key to go to dashboard.");
                    Console.ReadKey(true);
                    DashBoard();
                    return;
                }
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    //string id = userParts[0];
                    string name = userParts[1];
                    string price = userParts[2];
                    string quantity = userParts[3];

                    cartItemPreview += $"{name}\tPrice :  {price}\tQuantity :  {quantity}\n";

                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                DashBoard();
                return;
            } 
            string[] cartOptions = { "Remove item from cart", "Clear cart", "Proceed to checkout", "Goto Dashboard" };
            Menu menu = new Menu(cartOptions);
            int input = menu.Run(cartItemPreview);
            switch (input)
            {
                case 0:
                    removeProductFromCart(cartItemPreview);
                    loadCart();
                    break;
                    //case 1:
                    //    cart.clearCart(); loadCart();
                    //    break;
                    //case 2:
                    //    checkout();

                    //    break;
                    //case 3:
                    //    DashBoard();
                    //    break;
                    //default:
                    //    Console.WriteLine("Invalid input."); loadCart();
                    //    break;

            }

        }
        void removeProductFromCart(string cartList)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\cart\\{userName}_cart.txt"); 
            //Menu menu = new Menu(cartList);
            //int input = menu.Run("Choose the product you want to remove!\n\n");

        }

    }

}
