using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizz
{
    public class LoginInfo
    {
        private string username;
        private string password;
        public LoginInfo()
        {
        }
        public LoginInfo(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Password {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

    }

}
