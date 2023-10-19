using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public class Admin
    {
        public void AddProduct()
        {
            Console.WriteLine("Enter 1 to add Electronic Product");
            Console.WriteLine("Enter 2 to add Cloth Product");
            Console.WriteLine("Enter 3 to add HomeAppliences");
            Console.WriteLine("Enter 4 to return to menu");

            int input = 0;
            try
            {
                input = int.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //switch(input) 
            //{
            //    case 1:
            //         Database.CreateElectronicProduct(id,name,price,description);
            //        AddProduct();
            //        break;
            //    case 2:
            //         Database.CreatClothingeProduct(id,name,price,description);
            //        AddProduct();
            //        break;
            //    case 3:
            //        Database.CreateHomeAppliences(id,name,price,description);
            //        AddProduct();
            //        break;

            //    case 4:
            //        utility.adminDashboard();
            //        break;
            //    default:
            //        Console.WriteLine("Invalid Input");
            //        break;
            //}

            string name, description;
            double price;
            //Id dekhte hobe
            int id=0;
            Console.WriteLine("Enter Name of the product:");
            name= Console.ReadLine();

            Console.WriteLine("Enter Price of the product");
            price=Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter Description of the product");
            description=Console.ReadLine();

            

        }
    }
}
