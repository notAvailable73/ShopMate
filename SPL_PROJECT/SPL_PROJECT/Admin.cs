using System;

namespace SPL_PROJECT
{
    public class Admin : IAccount
    {

        public void AddProduct()
        {
            string[] addProductOptions = { "Electronic Product", "Clothing Product", "Home Appliences", "Menu" };
            IProductAdder productAdder;

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
            string[] adminDashBoardOptions = { "Add New Product","Add Quantity Of Existing Product", "Log Out" };
            Menu menu=new Menu(adminDashBoardOptions);

            //Console.WriteLine("Enter 1 to add product");
            //Console.WriteLine("Enter 2 to Log out");

            int input = menu.Run();

            

            switch (input)
            {
                case 0: AddProduct(); break;
                case 1:
                    Console.WriteLine("Enter Product Id: ");
                    int id= Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("How Many More Product Do You Want To Add?");
                    int add= Convert.ToInt32(Console.ReadLine());
                    IProduct product= Database.getProduct(id);

                    if (product.id > 10000 && product.id < 20000)
                    {
                        Database.EditElectronicProductQuantity(product, add);
                    }
                    else if (product.id > 20000 && product.id < 30000)
                    {
                        Database.EditClothingProductQuantity(product, add);
                    }
                    else
                    {
                        Database.EditHomeProductQuantity(product, add);
                    }
                    Console.WriteLine("Quantity Increased Successfully\nPress Any Key To Contimue");
                    Console.ReadLine();
                    dashboard();

                    break;
                case 2: logOut(); break;
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
