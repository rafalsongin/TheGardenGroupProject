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
    public partial class NewUserPage : Page

    {
        private IMongoCollection<User> _userCollection;

        public NewUserPage()
        {
            InitializeComponent();
        }

        private void AddUser_btn_Click(object sender, RoutedEventArgs e)
        {

            string firstName = FirstNameTxt.Text;
            string lastName = LastNameTxt.Text;
            string emailAddress = EmailAddressTxt.Text;
            string phoneNumber = PhoneNumberTxt.Text;
            string userType = ((ComboBoxItem)TypeOfUseCombo.SelectedItem)?.Content.ToString();
            string city = ((ComboBoxItem)LocationCombo.SelectedItem)?.Content.ToString();
            
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(userType)
                || string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            UserService userService = new UserService();
            bool isCreated = userService.IsUserCreatedAndAddedSuccessfully(firstName, lastName, emailAddress, phoneNumber, city, userType);


            if (isCreated)
            {
                UserSuccessfullyAddedMessage.Visibility = Visibility.Visible;
                UserNotAddedMessage.Visibility = Visibility.Hidden;
            }
            else
            {
                UserSuccessfullyAddedMessage.Visibility = Visibility.Hidden;
                UserNotAddedMessage.Visibility = Visibility.Visible;
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTxt.Clear();
            LastNameTxt.Clear();
            EmailAddressTxt.Clear();
            PhoneNumberTxt.Clear();

            // Reset the ComboBox selections to the first item
            TypeOfUseCombo.SelectedIndex = 0;
            LocationCombo.SelectedIndex = 0;
        }
    }
}