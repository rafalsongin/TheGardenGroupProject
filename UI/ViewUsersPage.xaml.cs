using System.Collections.Generic;
using System.Windows.Controls;
using Model;
using Service;

namespace TheGardenGroupProject
{
    public partial class ViewUsersPage : Page
    {
        public ViewUsersPage()
        {
            InitializeComponent();
            DisplayAllUsers();
        }

        // dana
        private void DisplayAllUsers()
        {
            UserService userService = new UserService();
            List<User> usersList = userService.GetAllUsers();

            ListViewAllUsers.Items.Clear();
            int id = 1;

            List<ListViewItemData> dataList = new List<ListViewItemData>();

            foreach (User user in usersList)
            {
                ListViewItemData data = new ListViewItemData
                {
                    Id = id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AmountOfTickets = GetUserTicketCount(user)
                };

                id++;

                dataList.Add(data);
            }

            ListViewAllUsers.ItemsSource = dataList;
        }

        private int GetUserTicketCount(User user)
        {
            UserService userService = new UserService();
            long ticketCount = userService.GetTicketCountForUser(user.Username);

            return (int)ticketCount;
        }

        public class ListViewItemData
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? AmountOfTickets { get; set; }
        }
    }
}