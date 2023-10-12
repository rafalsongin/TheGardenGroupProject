using Model;
using MongoDB.Driver;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void AddUser_btn_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName_txt.Text;
            string lastName = LastName_txt.Text;
            string emailAddress = EmailAddress_txt.Text;
            string phoneNumber = PhoneNumber_txt.Text;
            string userType = ((ComboBoxItem)TypeOfUse_combo.SelectedItem)?.Content.ToString();
            string city = ((ComboBoxItem)Location_combo.SelectedItem)?.Content.ToString();

            // declaring variable, otherwise doesn't work
            City newCity = City.Haarlem;
            if (city == "Haarlem") {
                newCity = City.Haarlem;
            }

            // declaring variable, otherwise doesn't work
            UserType newUserType = UserType.ServiceDeskEmployee;
            if (userType == "Service desk employee")
            {
                newUserType = UserType.ServiceDeskEmployee;
            }

            // TODO: check if this is needed in the requirements
            //bool sendPassword = SendPassword_CheckBox.IsChecked ?? false;

            string username = firstName + lastName;
            string password = firstName + lastName + "login";
            User newUser = new User(username, password, firstName, lastName, newUserType, emailAddress, phoneNumber, newCity);
            
           
            UserService userService = new UserService();
            userService.AddUser(newUser);

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(userType)
                || string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}