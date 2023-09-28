using Model;
using Service;
using System.Collections.Generic;
using System.Windows;
using UI;

namespace TheGardenGroupProject
{
    public partial class DashboardUI : Window
    {

        public DashboardUI(string username)
        {
            InitializeComponent();
            
            DisplayUserByUsername(username);
        }
        
        // TODO: add logout button later
        /*private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginUI loginWindow = new LoginUI();
            loginWindow.Show();
            this.Close();
        }*/

        // Working, created for testing
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
            // string? username = "jstatham";
            UserService userService = new UserService();
            User user = userService.GetUserByUsername(username);
            
            ListViewTest.Items.Add(user.Username);
        }
    }
}
