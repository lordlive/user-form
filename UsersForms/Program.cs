using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace UsersForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Declare a list of UserClass objects
            List<UserClass> Users = new List<UserClass>();
            // Check for the existence of the data file
            if (File.Exists("user.xml"))
            {
                Users = DeSerialize(); // if the file exists, read the data
            }
            else // if the file does not exist, create a default one
            {
                Users.Add(new UserClass("admin", "admin1234", true, true));
                Users.Add(new UserClass("user1", "qwerty123", true, false));
                Serialize(Users); // and save it
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(Users));
        }
        public static void Serialize(List<UserClass> temp)
        {
            // we pass the class type to the constructor
            // we get a stream where we will write the serialized object           
            FileStream fs = new FileStream("user.xml", FileMode.Create);
            // create an XmlSerializer object
            XmlSerializer formatter = new XmlSerializer(typeof(List<UserClass>));
            try
            {
                formatter.Serialize(fs, temp);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Object not serialized reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        static List<UserClass> DeSerialize()
        {
            // create a temporary object for deserialization
            List<UserClass> lists = new List<UserClass>();
            // open a stream from the file
            FileStream fs = new FileStream("user.xml", FileMode.OpenOrCreate);
            try
            {
                // create an XmlSerializer object
                XmlSerializer formatter = new XmlSerializer(typeof(List<UserClass>));
                lists = (List<UserClass>)formatter.Deserialize(fs);
                return lists;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Object not deserialized reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close(); // closing the stream
            }
        }
    }
}
