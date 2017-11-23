using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizz
{
    public class UserInfo
    {
        private int id;
        private string firstName;
        private string lastName;
        private string addresse;
        private string email;
        private int phone;
        private Groupflags groupflag;
        public UserInfo(string firstName, string lastName, string addresse, string email, int phone, Groupflags groupflag)
        {
            FirstName = firstName;
            LastName = lastName;
            Addresse = addresse;
            Email = email;
            Phone = phone;
            Groupflag = groupflag;
        }
        public UserInfo(int id, string firstName, string lastName, string addresse, string email, int phone, Groupflags groupflag)
        {
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
            Addresse = addresse;
            Email = email;
            Phone = phone;
            Groupflag = groupflag;
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
        public string Addresse
        {
            get
            {
                return addresse;
            }
            set
            {
                addresse = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public int Phone
        {
            get
            {
                return Phone;
            }
            set
            {
                Phone = value;
            }
        }
        public Groupflags Groupflag
        {
            get
            {
                return groupflag;
            }
            set
            {
                groupflag = value;
            }
        }
        public enum Groupflags { Administrator, Staff, User, Guest }

    }
}
