using ContactApp.Classes;
using SQLite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDatabase();
        }

        private void addContactButton_Click(object sender, RoutedEventArgs e)
        {
            AddContactWindow addContactWindow = new AddContactWindow();
            addContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                con.CreateTable<Contact>();
                contacts = con.Table<Contact>().ToList().OrderBy(c => c.Name).ToList();
            }

            if (contacts != null)
            {
                contactsListView.ItemsSource = contacts;
            }


        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? searchTextBox = sender as TextBox;
            if (searchTextBox != null)
            {
                var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                contactsListView.ItemsSource = filteredList;
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

            Contact contact = (Contact)(sender as Button).DataContext;
            editContactWindow editWindow = new editContactWindow(contact);


            editWindow.nameTextBox.Text = contact.Name;
            editWindow.emailTextBox.Text = contact.Email;
            editWindow.phoneNumberTextBox.Text = contact.Phone;

            editWindow.ShowDialog();
            ReadDatabase();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = (Contact)(sender as Button).DataContext;
            contacts.Remove(contact);
            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                con.CreateTable<Contact>();
                con.Delete(contact);
            }

            ReadDatabase();
        }
    }
}