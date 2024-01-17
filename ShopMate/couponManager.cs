using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate
{
    public class couponManager
    {
        public void generateCoupon(string userName, string coupon, string discountPercent)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\Coupons\\{userName}_coupons.txt");
            string info = $"{discountPercent};{coupon}\n";
            File.AppendAllText(path, info);
        }
        public int getDiscount(string userName, string coupon)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\Coupons\\{userName}_coupons.txt");
            string[] coupons = File.ReadAllLines(path);

            try
            {
                foreach (string couponInfo in coupons)
                {
                    string[] part = couponInfo.Split(';');
                    string discountPercent = part[0];
                    string availableCoupons = part[1];
                    if (coupon == availableCoupons)
                    {
                        int ans = Convert.ToInt32(discountPercent);
                        return ans;
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("error: "+ e.Message);
            }
            
            return 0;
        }
    }
}
