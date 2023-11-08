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

        public NewUserUI()
        {
            InitializeComponent();
        }

        //dana

        private void AddUser_btn_Click(object sender, RoutedEventArgs e)
        {

            string firstName = FirstName_txt.Text;
            string lastName = LastName_txt.Text;
            string emailAddress = EmailAddress_txt.Text;
            string phoneNumber = PhoneNumber_txt.Text;
            string userType = ((ComboBoxItem)TypeOfUse_combo.SelectedItem)?.Content.ToString();
            string city = ((ComboBoxItem)Location_combo.SelectedItem)?.Content.ToString();
            
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
            FirstName_txt.Clear();
            LastName_txt.Clear();
            EmailAddress_txt.Clear();
            PhoneNumber_txt.Clear();

            // Reset the ComboBox selections to the first item
            TypeOfUse_combo.SelectedIndex = 0;
            Location_combo.SelectedIndex = 0;

            // Close the window
            this.Close();
        }
    }
}