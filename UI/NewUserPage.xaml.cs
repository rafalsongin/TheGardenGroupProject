using Model;
using MongoDB.Driver;
using Service;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
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

        //dana
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

            //email validation using regular expression
            if (!IsValidEmail(emailAddress))
            {
                MessageBox.Show("Please enter a valid email address.");
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

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

            // Display a confirmation message box
            MessageBoxResult confirm = MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Only clear the form controls if the user confirms the cancellation
            if (confirm == MessageBoxResult.Yes)
            {
                // Call the method to clear form controls
                ClearFormControls();
            }
        }

        private void ClearFormControls()
        {
            // Clear the text boxes
            FirstNameTxt.Text = "";
            LastNameTxt.Text = "";
            EmailAddressTxt.Text = "";
            PhoneNumberTxt.Text = "";

            // Reset the ComboBox selections to the first item
            TypeOfUseCombo.SelectedIndex = -1;
            LocationCombo.SelectedIndex = -1;
        }


        //the IsValidEmail method is defined here
        private bool IsValidEmail(string email)
        {
            //regular expression pattern for email validation
            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*(\.[a-z]{2,4})$";

            //Regex.IsMatch is used to check if the email matches the pattern
            return Regex.IsMatch(email, pattern);
        }
        
    }
}