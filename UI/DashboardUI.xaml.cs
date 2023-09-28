using Model;
using Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace TheGardenGroupProject
{
    public partial class DashboardUI : Window
    {

        public DashboardUI()
        {
            InitializeComponent();
        }

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
    }
}
