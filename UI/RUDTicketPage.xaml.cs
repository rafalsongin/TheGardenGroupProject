using System;
using System.Windows;
using System.Windows.Controls;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using Service;

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for RUDTicket.xaml
    /// </summary>
    public partial class RudTicketPage : Page
    {
        private FilterDefinition<Ticket> filter;

        public RudTicketPage()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string serachCriteria = txtFilterByEmail.Text; // i will change it filter by id or email
            TicketService ticketService = new TicketService();

            // Create a filter based on the search criteria (e.g., by subject)
            filter = Builders<Ticket>.Filter.Eq("Subject", serachCriteria);

            // Call the service method to get the filtered tickets
            Ticket matchingTicket = ticketService.GetTicketByFilter(filter);

            IDTextBox.Text = matchingTicket.TicketId.ToString();
            EmailTextBox.Text = matchingTicket.Email;
            PriorityComboBox.Text = matchingTicket.Priority.ToString();
            AssignedByTextBox.Text = matchingTicket.Assignedby;
            DeadlineDatePicker.SelectedDate = matchingTicket.Deadline;
        }

        private void btnUpdateTicekt_Click(object sender, RoutedEventArgs e)
        {
            TicketService ticketService = new TicketService();
            if (filter != null)
            {
                DateTime selectedDeadline = DeadlineDatePicker.SelectedDate ?? DateTime.MinValue;
                string Priority = ((ComboBoxItem)PriorityComboBox.SelectedItem).Content.ToString();


                Ticket upgradedTicket = new Ticket()
                {
                    Subject = txtFilterByEmail.Text,
                    TicketId = ObjectId.Parse(IDTextBox.Text),
                    Email = EmailTextBox.Text,
                    Priority = (Priority)Enum.Parse(typeof(Priority), Priority),
                    Assignedby = AssignedByTextBox.Text,
                    Deadline = selectedDeadline,
                };
                ticketService.UpdateTicket(filter, upgradedTicket);
                MessageBox.Show("Ticket updated successfully !");
            }
            else
            {
                MessageBox.Show("No matching ticket found for updating.");
            }
        }

        private void btnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            if (filter != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this ticket?",
                    "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // User confirmed the deletion, so proceed with deleting the ticket
                    TicketService ticketService = new TicketService();
                    ticketService.DeleteTicket(filter);
                    MessageBox.Show("Ticket deleted successfully!");
                    ClearUIElements();
                }
                else
                {
                    // User chose not to delete the ticket
                    MessageBox.Show("Ticket deletion canceled.");
                }
            }
            else
            {
                MessageBox.Show("Nothing selected");
            }
        }

        private void ClearUIElements()
        {
            // Clear the content of text boxes, combo boxes, date pickers, etc.
            IDTextBox.Text = "";
            EmailTextBox.Text = "";
            PriorityComboBox.SelectedIndex = -1;
            AssignedByTextBox.Text = "";
            DeadlineDatePicker.SelectedDate = null;
        }

        /*private void btnCreateIncidentTicket_Click(object sender, RoutedEventArgs e)
        {
            CreateTicketUIxaml createTicketUI = new CreateTicketUIxaml();
            createTicketUI.Show();
            this.Close();
        }*/
    }
}