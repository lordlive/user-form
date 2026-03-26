using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsersForms
{
    public partial class LoginForms : Form
    {
        // Data transfer event
        public event EventHandler<UserEventArgs> sendDataFormEvent;
        // Form designer
        public LoginForms()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Create an object of type UserClass
            UserClass temp = new UserClass(textBoxLogin.Text, textBoxPassword.Text);
            // Generate event with named argument
            // Pass the created object to the argument class
            if (sendDataFormEvent != null)
                sendDataFormEvent(this, new UserEventArgs(temp));

            // Close the form
            this.Close();
        }
    }
}
