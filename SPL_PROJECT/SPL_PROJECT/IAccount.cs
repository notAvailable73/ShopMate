using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public interface IAccount
    {
        void dashboard();
        void logOut();
        void loadinbox();
        void changePassword();
        //void editProfile();
    }
}
