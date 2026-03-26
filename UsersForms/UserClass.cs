using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace UsersForms
{
    // A class that describes users with rights (admin/user) of type (user by default or not) and their passwords
    [Serializable]
    public class UserClass
    {
        public String Name;
        bool defaultUser, adminUser; // properties - default user / administrator rights
        public String Password;

        // default constructor
        public UserClass() { }
        // full constructor
        public UserClass(String name, string pass, bool def, bool adm)
        {
            Name = name;
            Password = pass;
            defaultUser = def;
            adminUser = adm;
            //count++;
        }
        // partial constructor
        public UserClass(String name, string pass)
        {
            Name = name;
            Password = pass;
            defaultUser = adminUser = false;
        }

        // access to private variables of the UserClass class
        public bool dUser
        {
            get
            {
                return defaultUser;
            }
            set
            {
                defaultUser = value;
            }

        }
        public bool aUser
        {
            get
            {
                return adminUser;
            }
            set
            {
                adminUser = value;
            }
        }
    }
}
