using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate
{
    public interface passwordChangable
    {
        void changePassword();
    }
    public interface InboxLoadable
    {
        void loadinbox();
    }
    public interface IAccount
    {
        void DashBoard(); 
    }
}
