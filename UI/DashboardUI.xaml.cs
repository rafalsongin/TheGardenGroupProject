using Model;
using Service;
using System.Collections.Generic;
using System.Windows;
using UI;

namespace TheGardenGroupProject
{
    public partial class DashboardUI : Window
    {
        private User ActiveUser { get; }

        public DashboardUI(string username)
        {
            InitializeComponent();
            
            // Set ActiveUser so that it can be used in other methods
            UserService userService = new UserService();
            ActiveUser = userService.GetUserByUsername(username);
            LabelDisplayLoggedInUsername.Content = "Logged in as: " + ActiveUser.Username;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginUI loginWindow = new LoginUI();
            loginWindow.Show();
            this.Close();
        }

        /*// Working, created for testing
        private void DisplayAllUsersUsernames()
        {
            UserService userService = new UserService();
            List<User> userList = userService.GetAllUsers();
            
            foreach (var user in userList)
            {
                ListViewTest.Items.Add(user.Username);
            }
        }

        // Working, created for testing
        private void DisplayUserByUsername(string username)
        {
            UserService userService = new UserService();
            User user = userService.GetUserByUsername(username);
            
            ListViewTest.Items.Add(user.Username);
        }*/
    }
}
