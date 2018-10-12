using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces
{
    interface SecurityUserInterface
    {
        string getPassword();
        string getLogin();
        bool isAdmin();
        void setAdmin(bool isAdmin);
        SecurityUserInterface copy();
        void setPassword(string password);
    }
}
