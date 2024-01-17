using System;
using System.Diagnostics;
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
                    loadInbox();
                    DashBoard();
                    break;
                case 3:
                    Console.Clear();
                    loadCart();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine();
                    watchPreviousOrderList();
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
            while (true)
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
                if (inp != 1)
                {
                    break;
                }
            }
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
                    Console.WriteLine("invalid input!!!!");
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
                    int newQuantity = Convert.ToInt32(quantity) + 1;
                    if (id == product.id)
                    {
                        if (newQuantity <= product.qty)
                        {
                            quantity = newQuantity.ToString(); alreadyInCart = true;
                            lines[i] = $"{id},{name},{price},{quantity}";
                            File.WriteAllLines(path, lines);

                        }
                        else
                        {
                            Console.WriteLine($"Sorry,  This product has low inventory. You have already added {quantity} unit of this product.");
                            Console.ReadKey();
                            return;
                        }
                        break;
                    }

                }
                if (!alreadyInCart)
                {
                    if (product.qty <= 0)
                    {
                        Console.WriteLine("Stock out.");
                        Console.ReadKey();
                        return;
                    }
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
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\cart\\{userName}_cart.txt");
            string[] lines = File.ReadAllLines(path);
            if (lines.Length != 0)
            {
                string message = $"Hey {name}! Maybe you forgot something in your cart.";
                Messenger messenger = new Messenger();
                messenger.sendMessage(userName, message);
            }
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

                    cartItemPreview += $"{name}\tPrice : {price}\tQuantity :  {quantity}\n";

                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                DashBoard();
                return;
            }
            string[] cartOptions = { "Proceed to checkout", "Remove item from cart", "Clear cart",  "Goto Dashboard" };
            Menu menu = new Menu(cartOptions);
            int input = menu.Run(cartItemPreview);
            switch (input)
            {
                case 0:
                    checkout(path, cartItemPreview);
                    DashBoard(); 
                    break;
                case 1:
                    removeProductFromCart(cartItemPreview);
                    loadCart();
                    return;
                case 2:
                    clearCart(path);
                    Console.WriteLine("Successfully Cleared your cart.");
                    Console.ReadKey(); 
                    DashBoard();
                    return;
                case 3:
                    DashBoard();
                    return;
                default:
                    Console.WriteLine("Invalid input."); loadCart();
                    return;

            }

        }
        void checkout(string path, string cartList)
        {
            productInventoryUpdater updater = new productInventoryUpdater(path);
            string[] lines = File.ReadAllLines(path);

            if (!updater.isAllProductAvailable())
            {
                Console.WriteLine("\nYou may have to remove some item from stock.\n");
                Console.ReadKey();
                return;
            }
            double totalPrice = 0;
            try
            {

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userParts = lines[i].Split(',');
                    string id = userParts[0];
                    string name = userParts[1];
                    string price = userParts[2];
                    string quantity = userParts[3];

                    double Price = Convert.ToDouble(price) * Convert.ToInt32(quantity);
                    totalPrice += Price;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                return;
            }
            string[] checkoutOptions = { "Confirm Order", "Go Back" };
            Menu menu = new Menu(checkoutOptions);
            int input = menu.Run(cartList + "\n\nTotal Price : " + totalPrice);
            switch (input)
            {
                case 0:
                    updater.update();
                    IOrderCreator orderCreator = new OrderManager(userName);
                    orderCreator.createOrder(lines);
                    Console.WriteLine("Order Complete!");
                    Console.ReadKey();
                    clearCart(path);
                    return;
                case 1:
                    loadCart();
                    return;
                default:
                    Console.WriteLine("Invalid input."); loadCart();
                    return;
            }
        }
        void removeProductFromCart(string cartList)
        {
            if (cartList == "")
            {
                Console.WriteLine("Empty Cart!\nPress any key to go to dashboard.");
                Console.ReadKey();
                DashBoard();
                return;
            }
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\cart\\{userName}_cart.txt");
            cartList += "Go back\n";
            string[] options = cartList.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Menu menu = new Menu(options);
            int index = menu.Run("Which Product do you want to remove from the cart?\n\n");
            if (index == options.Length - 1)
            {
                Console.Clear();
                loadCart();
                return;
            }
            try
            {
                string[] lines = File.ReadAllLines(path);

                string[] userParts = lines[index].Split(',');
                string id = userParts[0];
                string name = userParts[1];
                string price = userParts[2];
                string quantity = userParts[3];
                int newQuantity = Convert.ToInt32(quantity) - 1;
                quantity = newQuantity.ToString();
                if (newQuantity == 0)
                {
                    StreamReader sr = new StreamReader(path);
                    string line;
                    string info = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] productInfos = line.Split(',');
                        string Productid = productInfos[0];

                        if (Productid == id)
                        {
                            continue;
                        }
                        info += $"{line}\n";
                    }
                    sr.Close();
                    File.WriteAllText(path, info);
                }
                else
                {
                    lines[index] = $"{id},{name},{price},{quantity}";
                    File.WriteAllLines(path, lines);
                }
                Console.WriteLine($"Successfully Removed a unit of {name} from your cart.");
                Console.ReadKey();
                loadCart();
                return;

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                loadCart();
                return;
            }

        }
        void clearCart(string path)
        {
            File.WriteAllText(path, String.Empty);
        }
        void watchPreviousOrderList()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\OrderInfo\\{userName}_Orders.txt");

            try
            { 
                string[] lines = File.ReadAllLines(path);
                foreach (var item in lines)
                {
                    string[] userParts = item.Split(','); 
                    string orderID = userParts[0];
                    //string userName = userParts[1];
                    //string productId = userParts[2];
                    string productName = userParts[3];
                    //string productPrice = userParts[4];
                    //string productQuantity = userParts[5];
                    string orderStatus = userParts[6];
                    Console.WriteLine($"ID:{orderID}\tProduct Name :{productName}\t\tStatus : {orderStatus}");  

                }
                Console.WriteLine("Press any key to go back to dashboard.");
                Console.ReadKey();
                DashBoard();
                return;

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.ReadLine();
                DashBoard();
                return;
            }
        }
        void loadInbox()
        {
            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string path = Path.Combine(baseDirectory, $"Database\\cart\\{userName}_cart.txt");
            Messenger messenger = new Messenger();
            messenger.watchMessage(userName);
        }
    }

}
