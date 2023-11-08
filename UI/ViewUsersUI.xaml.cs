using Model;
using MongoDB.Bson;
using Service;
using System;
using System.Collections.Generic;
using System.Net.Mail;
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

        //dana
        private void DisplayAllUsers()
        {
            UserService userService = new UserService();
            List<User> usersList = userService.GetAllUsers();

            ListViewAllUsers.Items.Clear();
            int id = 1;

            List<ListViewItemData> dataList = new List<ListViewItemData>();

            foreach (User user in usersList)
            {
                ListViewItemData data = new ListViewItemData();
                data.Id = id;
                data.Email = user.Email;
                data.FirstName = user.FirstName;
                data.LastName = user.LastName;
                data.AmountOfTickets = 2;
                //int.Parse(user.AmountOfTickets); 
                // hardcoded

                if (IsValidEmail(user.Email))
                {
                    data.Email = user.Email;
                }
                else
                {
                    // Handle invalid email address (e.g., mark as invalid or display an error)
                    data.Email = "Invalid Email";
                }

                id++;

                dataList.Add(data);
                
                //here I create a ListViewItem with the user's username 
                //ListViewItem item = new ListViewItem();
                //item.Content = user.Username;
                
            }



            ListViewAllUsers.ItemsSource = dataList;
            
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
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
            public int? AmountOfTickets { get; set; }
        }
    }
}
