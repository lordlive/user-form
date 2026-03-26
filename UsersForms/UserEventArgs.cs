using System;

namespace UsersForms
{
    // public class, descendant from EventArgs
    // Used as an argument for passing data in events
    public class UserEventArgs : EventArgs
    {
        // public (readonly) field of the class
        public UserClass SendingUser;
        // Constructor of the class with a parameter
        public UserEventArgs(UserClass User)
        {
            // passes an object of type UserClass
            SendingUser = User;
        }
    }
}
