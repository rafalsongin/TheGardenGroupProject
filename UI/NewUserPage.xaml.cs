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
        public NewUserPage()
        {
            InitializeComponent();
        }

        private void AddUser_btn_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName_txt.Text;
            string lastName = LastName_txt.Text;
            string emailAddress = EmailAddress_txt.Text; // TODO: add email validation (if it has @ and .) 
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

            bool doSendPassword = false;
            
            if (SendPassword_CheckBox.IsChecked == true)
            {
                doSendPassword = SendPassword_CheckBox.IsChecked == true;
            }
            else if (SendPassword_CheckBox.IsChecked == false)
            {
                doSendPassword = SendPassword_CheckBox.IsChecked == false;
            }

            UserService userService = new UserService();
            userService.CreateAndAddUser(firstName, lastName, emailAddress, phoneNumber, city, userType, doSendPassword);

            if (SendPassword_CheckBox.IsChecked == true)
            {
                   
            }
            
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: replace with "clear" or something similar
            // this.Close();
        }
    }
}