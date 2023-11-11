using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace TheGardenGroupProject
{
    public partial class NewUserPage : Page

    {
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
                                                     || string.IsNullOrWhiteSpace(emailAddress) ||
                                                     string.IsNullOrWhiteSpace(userType)
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

            bool doSendPassword = SendPasswordCheckBox.IsChecked == true;

            UserService userService = new UserService();
            bool isCreated = userService.IsUserCreatedAndAddedSuccessfully(firstName, lastName, emailAddress,
                phoneNumber, city, userType, doSendPassword);

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
            MessageBoxResult confirm = MessageBox.Show("Are you sure you want to cancel?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);


            //only clear the form controls if the user confirms the cancellation
            if (confirm == MessageBoxResult.Yes)
            {
                //call the method to clear form controls
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

            //reset the ComboBox selections
            TypeOfUseCombo.SelectedIndex = -1;
            LocationCombo.SelectedIndex = -1;
        }


        // the IsValidEmail method is defined here
        private bool IsValidEmail(string email)
        {
            // regular expression pattern for email validation
            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*(\.[a-z]{2,4})$";

            // Regex.IsMatch is used to check if the email matches the pattern
            return Regex.IsMatch(email, pattern);
        }
    }
}