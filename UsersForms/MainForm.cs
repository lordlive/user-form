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
    public partial class MainForm : Form
    {
        // static class variable into which data from the login and password form is written
        public static UserClass login = new UserClass();
        // static variable defining the current position in the list of users
        public static int CountList = 0;
        public static List<UserClass> Work; // list of users
        public MainForm(List<UserClass> Users)

        {
            // the first form should be the login and password verification form and only after successful authentication
            // main form starts!!!!
            //List<UserClass> Base = Users;
            Work = Users;  // we assign the list of users received from the startup program
            bool accsesLog = false; // variable indicating the correctness of the login
            bool accsesPas = false; // variable indicating the correctness of the password

            InitializeComponent();
            // create a login and password entry form
            LoginForms pForm = new LoginForms();
            // Attaching an event handler to a child form
            pForm.sendDataFormEvent += new EventHandler<UserEventArgs>(pForm_sendDataFormEvent);
            // Display it for filling the text fields
            pForm.ShowDialog();
            // check the correctness of the user input and password
            foreach (UserClass temp in Work)
                if (login.Name == temp.Name) // check if such a login exists
                {
                    accsesLog = true;
                    if (login.Password == temp.Password) // check if the password matches
                    {
                        accsesPas = true;
                        login.aUser = temp.aUser;
                    }
                }
            if (accsesLog != accsesPas || !accsesLog)
            {
                // we display a message about an incorrect login and password
                MessageBox.Show("Login or password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // After clicking the (OK) button, close the application
                Environment.Exit(0); // close the application
            }
            UserClass workUser = login; // current user
            if (!workUser.aUser)
            {
                // if the user is not an administrator, deny access to the buttons
                buttonAdd.Enabled = false;
                buttonSave.Enabled = false;
                buttonDel.Enabled = false;
            }
            login = null; // reset the link

            // Displaying information about the current user
            //Display the user's name
            textBoxName.Text = Work[CountList].Name.ToString();
            // Display the user type
            if (Work[CountList].aUser)
                textBoxType.Text = "Administrator";
            else
                textBoxType.Text = "User";
        }
        // Handler for the event of receiving data from the child form
        void pForm_sendDataFormEvent(object sender, UserEventArgs e)
        {
            // receive the object from the class
            login = e.SendingUser;
            //MessageBox.Show(login.Name);
            //textBoxLog.Text = login.Name;
            //textBoxPass.Text = login.Password;
        }
        // Handler for the event of viewing the next element in the user list
        private void buttonNext_Click(object sender, EventArgs e)
        {
            // check that we are not at the end of the list and increment the counter by 1
            if (CountList < Work.Count - 1)
                CountList++;
            // Display the user's name
            textBoxName.Text = Work[CountList].Name.ToString();
            // Display the user type
            if (Work[CountList].aUser)
                textBoxType.Text = "Administrator";
            else
                textBoxType.Text = "User";
        }
        // Handler for the event of viewing the previous element in the user list
        private void buttonPrevius_Click(object sender, EventArgs e)
        {
            // check that we are not at the beginning of the list and decrement the counter by 1
            if (CountList > 0)
                CountList--;
            //Display the user's name
            textBoxName.Text = Work[CountList].Name.ToString();
            // Display the user type
            if (Work[CountList].aUser)
                textBoxType.Text = "Administrator";
            else
                textBoxType.Text = "User";
        }
        // Handler for the event of adding a new user to the list
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddForms pForm = new AddForms();
            // Connect the event handler to the child form
            pForm.sendDataFormEvent += new EventHandler<UserEventArgs>(pForm_sendDataFormEvent);
            // We display it to fill text fields
            pForm.ShowDialog();
            Work.Add(login);
        }
        // Handler for the event of saving the current user database
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Program.Serialize(Work);
        }
        // Handler for the event of deleting the selected user
        private void buttonDel_Click(object sender, EventArgs e)
        {
            // check that the counter is within the list bounds and remove the current record
            if (CountList >= 0 && CountList < Work.Count - 1)
                if (Work[CountList].dUser != true) // check that the selected user for deletion is not the default user
                    Work.RemoveAt(CountList);

        }
    }
}
