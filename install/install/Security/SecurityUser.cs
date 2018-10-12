using install.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Security
{
    class SecurityUser : SecurityUserInterface
    {
        private string login;
        private string password;
        private bool admin;
        private bool enterIntoSystem;

        public SecurityUser(string login, string password)
        {
            this.login = login;
            this.password = password;
            admin = false;
            enterIntoSystem = false;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public void setAdmin(bool isAdmin)
        {
            admin = isAdmin;
        }

        public string getLogin()
        {
            return login;
        }

        public string getPassword()
        {
            return password;
        }

        public bool isAdmin()
        {
            return admin;
        }

        public SecurityUserInterface copy()
        {
            SecurityUserInterface copyUser = new SecurityUser(login, password);
            copyUser.setAdmin(isAdmin());

            return copyUser;
        }
    }
}
