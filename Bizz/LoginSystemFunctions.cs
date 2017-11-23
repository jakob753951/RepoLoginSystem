using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizz
{
    public class LoginSystemFunctions
    {
        ObservableCollection<LoginInfo> CollectionOfLogins = new ObservableCollection<LoginInfo>();
        ObservableCollection<UserInfo> CollectionOfUsers = new ObservableCollection<UserInfo>();
        public LoginSystemFunctions()
        {

        }
        public bool CheckCredentials(string Username, string Password)
        {
            return true;
        }
    }
}
