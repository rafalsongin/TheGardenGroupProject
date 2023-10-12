using Model;
using Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for ViewUsersUI.xaml
    /// </summary>
    public partial class ViewUsersUI : Window
    {
        public ViewUsersUI()
        {
            InitializeComponent();
            DisplayAllUsers();
        }

        private void DisplayAllUsers()
        {
            UserService userService = new UserService();
            List<User> usersList = userService.GetAllUsers();

            for (int i = 0; i < usersList.Count; i++)
            {
                listViewAllUsers.Items.Add(usersList[i].Username);
                 // add it to the listview
            }
        }

        private void listViewAllUsers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
