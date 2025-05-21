using ContactApp.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContactApp
{
    /// <summary>
    /// Interaction logic for editContactWindow.xaml
    /// </summary>
    public partial class editContactWindow : Window
    {
        Contact _contact;
        public editContactWindow(Contact contact)
        {
            InitializeComponent();

            this._contact = contact;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this._contact != null)
            {
                _contact.Name = nameTextBox.Text;
                _contact.Email = emailTextBox.Text;
                _contact.Phone = phoneNumberTextBox.Text;


                using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
                {
                    con.CreateTable<Contact>();
                    con.Update(_contact);
                }

                Close();
            }

        }
    }
}
