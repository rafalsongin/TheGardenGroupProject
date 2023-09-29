using Model;
using MongoDB.Driver;
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

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for NewUserUI.xaml
    /// </summary>
    public partial class NewUserUI : Window

    {
        private IMongoCollection<User> _userCollection;

        public NewUserUI()
        {
            InitializeComponent();

            var client = new MongoClient("mongodb+srv://dbUser:test123@thegardengroupserverles.vovxxor.mongodb.net/");
            var database = client.GetDatabase("TheGardenGroupProject"); 
            _userCollection = database.GetCollection<User>("User");
        }

        private void AddUser_btn_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName_txt.Text;
            string lastName = LastName_txt.Text;
            string emailAddress = EmailAddress_txt.Text;
            string phoneNumber = PhoneNumber_txt.Text;
            string userType = ((ComboBoxItem)TypeOfUse_combo.SelectedItem)?.Content.ToString();
            string location = ((ComboBoxItem)Location_combo.SelectedItem)?.Content.ToString();
            bool sendPassword = SendPassword_CheckBox.IsChecked ?? false;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(userType)
                || string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            City newCity = City.Amsterdam;
            UserType newUserType = UserType.ServiceDeskEmployee;

            if (userType == "Service desk employee") 
            {
                newUserType = UserType.ServiceDeskEmployee;
            }
            if (location == "Amsterdam") 
            {
                newCity = City.Amsterdam;
            }

            string newUsername = firstName + lastName;
            string password = firstName + lastName + "login";


            User newUser2 = new User(newUsername, password, firstName, lastName, UserType.ServiceDeskEmployee, emailAddress, phoneNumber, City.Amsterdam);


            /*User newUser = new User
            {
                Username = firstName + lastName,
                Password = firstName + lastName + "login",
                FirstName = firstName,
                LastName = lastName,
                UserType = UserType.ServiceDeskEmployee,
                Email = emailAddress,
                PhoneNumber = phoneNumber,
                City = newCity
            };*/

            
            AddUserToDatabase(newUser2);

            // Optional
            MessageBox.Show("User added successfully!");
            this.Close();
        }

        private void AddUserToDatabase(User user)
        {
            try
            {
                _userCollection.InsertOne(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}