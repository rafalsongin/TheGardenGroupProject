using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace TheGardenGroupProject
{
    public partial class CreateTicketPage : Page // made by Ghonim
    {
        public CreateTicketPage()
        {
            InitializeComponent();
            //filling the comboBoxes
            CreatingComboBoxes();
            DataContext = this;
            
            // Disable user input for DpTimeReported to ensure it reflects the date when the ticket is assigned
            DpTimeReported.IsEnabled = false;
            DpTimeReported.Text = DateTime.Now.ToString();

            // To be sure the user will not pick a date which is already passed 
            DpDeadline.DisplayDateStart = DateTime.Now;
        }

        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!AreInputsValid())
                {
                    return;
                }

                DateTime deadLine = DpDeadline.SelectedDate ?? DateTime.MinValue;
                DateTime dateReported = DateTime.Now;
                IncidentType selectedIncidentType = (IncidentType)IncidentTypeComboBox.SelectedItem;
                Priority selectedPriority = (Priority)PriorityComboBox.SelectedItem;

                Ticket ticket = new Ticket(dateReported, SubjectOfIncidentTextBox.Text, DescriptionTextBox.Text,
                    selectedIncidentType, ReportedByTextBox.Text, selectedPriority, deadLine);
                TicketService ticketService = new TicketService();
                ticketService.CreateTicket(ticket);
                ClearUIElements();
                MessageBox.Show("Ticket created successfully!", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Display a confirmation message box
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel the ticket?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                ClearUIElements();
            }
        }

        private void ClearUIElements()
        {
            DpTimeReported.SelectedDate = DateTime.Now;
            SubjectOfIncidentTextBox.Text = "";
            IncidentTypeComboBox.SelectedIndex = -1; // Clear the selection
            ReportedByTextBox.Text = "";
            DescriptionTextBox.Text = "";
            PriorityComboBox.SelectedIndex = -1;
            DpDeadline.SelectedDate = null;
        }

        private void CreatingComboBoxes()
        {
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            IncidentTypeComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().ToList();
        }

        private bool AreInputsValid()
        {
            // Validate inputs before creating a ticket
            if (string.IsNullOrWhiteSpace(SubjectOfIncidentTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                IncidentTypeComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(ReportedByTextBox.Text) ||
                PriorityComboBox.SelectedItem == null ||
                DpDeadline.SelectedDate == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            if (!IsValidName(ReportedByTextBox.Text))
            {
                MessageBox.Show("Please enter valid values for certain fields.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool IsValidName(string input)
        {
            // Use a regular expression to check if the input contains only letters
            return Regex.IsMatch(input, "^[a-zA-Z ]+$");
        }
    }
}