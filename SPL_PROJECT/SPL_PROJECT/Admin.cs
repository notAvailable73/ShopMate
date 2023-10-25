using System;

namespace SPL_PROJECT
{
    public class Admin : IAccount
    {

        public void AddProduct()
        {
            string[] addProductOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Menu" };
            IAdder productAdder;

            Menu menu=new Menu(addProductOptions);

            //Console.WriteLine("Enter 1 to add Electronic Product");
            //Console.WriteLine("Enter 2 to add Cloth Product");
            //Console.WriteLine("Enter 3 to add HomeAppliences");
            //Console.WriteLine("Enter 4 to return to menu");

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

        public void dashboard()
        {
            string[] adminDashBoardOptions = { "Add Product", "Log Out" };
            Menu menu=new Menu(adminDashBoardOptions);

            //Console.WriteLine("Enter 1 to add product");
            //Console.WriteLine("Enter 2 to Log out");

            int input = menu.Run();

            

            switch (input)
            {
                case 0: AddProduct(); break;
                case 1: logOut(); break;
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
