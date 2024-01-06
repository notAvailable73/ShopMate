using System;

namespace SPL_PROJECT
{
    public class Admin : IAccount
    {

        public void AddProduct()
        {
            string[] addProductOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Menu" };
            IProductAdder productAdder;

            Menu menu = new Menu(addProductOptions);

            int input = menu.Run();

            switch (input)
            {
                case 0:
                    productAdder = new ElectronicProductsAdder();
                    Database.addProduct(productAdder);
                    Console.WriteLine();
                    dashboard();
                    break;
                case 1:
                    productAdder = new ClothingProductsAdder();
                    Database.addProduct(productAdder);
                    Console.WriteLine();
                    dashboard();
                    break;
                case 2:
                    productAdder = new HomeAppliencesAdder();
                    Database.addProduct(productAdder);
                    Console.WriteLine();
                    dashboard();
                    break;

                case 3:
                    dashboard();
                    return;
                default:
                    Console.WriteLine("Invalid Input");
                    AddProduct();
                    return;
            }
        }

        public void InventoryAdder()
        {
            string[] addInventoryOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Menu" };

            IInventoryAdder inventoryAdder;

            Menu menu = new Menu(addInventoryOptions);
       
            int input = menu.Run();

            switch (input)
            {
                case 0:
                    inventoryAdder = new ElectronicInventoryAdder();
                    inventoryAdder.AddToInventory();
                    Console.WriteLine();
                    dashboard();
                    break;
                case 1:
                    inventoryAdder = new ClothingInventoryAdder();
                    inventoryAdder.AddToInventory();
                    Console.WriteLine();
                    dashboard();
                    break;
                case 2:
                    inventoryAdder = new HomeApplienceInventoryAdder();
                    inventoryAdder.AddToInventory();
                    Console.WriteLine();
                    dashboard();
                    break;                  
                default:
                    Console.WriteLine("Invalid Input");
                    dashboard();
                    return;
            }
        }

        public void dashboard()
        {
            string[] adminDashBoardOptions = { "Add New Product","Add Quantity Of Existing Product", "Inbox", "Log Out" };
            Menu menu=new Menu(adminDashBoardOptions);          

            int input = menu.Run();

            

            switch (input)
            {
                case 0: AddProduct(); break;
                case 1: InventoryAdder();break;
                case 2:Console.Clear();                    
                       AdminInbox.GetAdminInbox();
                       AdminInbox.loadinbox();
                       break;
                case 3: logOut(); break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }

        public void logOut()
        {
            Console.Clear();
            Console.WriteLine("------------Logged out successfully-------------");
            utility.mainMenu();
            return;
        }

    }
}
