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
    public partial class AddForms : Form
    {
        // Data transfer event
        public bool admChange = false;
        public event EventHandler<UserEventArgs> sendDataFormEvent;
        public AddForms()

        {
            InitializeComponent();

        }

        private void buttonAddOk_Click(object sender, EventArgs e)
        {
            // Create an object of type UserClass
            UserClass temp = new UserClass(textBoxName.Text, textBoxPass.Text);
            if (checkBoxAdmin.Checked)
                temp.aUser = true;
            else
                temp.aUser = false;
            temp.dUser = false;
            // Generate event with named argument
            // Pass the created object to the argument class
            if (sendDataFormEvent != null)
                sendDataFormEvent(this, new UserEventArgs(temp));

            // Close the form
            this.Close();
        }

        private void checkBoxAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (admChange) admChange = false;
            else admChange = true;
        }
    }
}
