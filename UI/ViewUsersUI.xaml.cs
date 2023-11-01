using Model;
using Service;
using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
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

            ListViewAllUsers.Items.Clear();
            int id = 1;

            foreach (User user in usersList)
            {
                //here I create a ListViewItem with the user's username 
                //ListViewItem item = new ListViewItem();
                //item.Content = user.Username;
                
            }
            ListViewItem item = new ListViewItem();
            GridViewRowPresenter rowPresenter = new GridViewRowPresenter();
            List<ListViewItemData> dataList = new List<ListViewItemData>();


            ListViewItemData data = new ListViewItemData();
            data.Id = id;
            data.Email = usersList[0].Email;
            data.FirstName = usersList[0].FirstName;
            data.LastName = usersList[0].LastName;
            data.AmountOfTickets = 2; // hardcoded

            id++;

            dataList.Add(data);

            ListViewAllUsers.ItemsSource = dataList;
            
        }

        private void listViewAllUsers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        public class ListViewItemData
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int AmountOfTickets { get; set; }
        }
    }
}
