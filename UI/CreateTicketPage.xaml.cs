using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TheGardenGroupProject
{
    public partial class CreateTicketPage : Page
    {
        public List<Priority> Priorities { get; set; }
        public List<IncidentType> IncidentTypes { get; set; }
     

        public CreateTicketPage()
        {
            InitializeComponent();
            //filling the comboBoxes
            Priorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().ToList();
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
                IncidentType selectedIncidentType = (IncidentType)incidentTypeCombobox.SelectedItem;
                Priority selectedPriority = (Priority)priorityCombobox.SelectedItem;

                Ticket ticket = new Ticket(dateReported, subjectOfIncidenttxt.Text, txtDescription.Text, selectedIncidentType, reportedByTxt.Text, selectedPriority, deadLine);
                TicketService ticketService = new TicketService();
                ticketService.CreateTicket(ticket);
                ClearUIElements();
                MessageBox.Show("Ticket created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Display a confirmation message box
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel the ticket?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                ClearUIElements();
            }
        }
        private void ClearUIElements()
        {
            DpTimeReported.SelectedDate = DateTime.Now;
            subjectOfIncidenttxt.Text = "";
            incidentTypeCombobox.SelectedIndex = -1; // Clear the selection
            reportedByTxt.Text = "";
            txtDescription.Text = "";
            priorityCombobox.SelectedIndex = -1;
            DpDeadline.SelectedDate = null;

        }
        private bool AreInputsValid()
        {
            // Validate inputs before creating a ticket
            if (string.IsNullOrWhiteSpace(subjectOfIncidenttxt.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                incidentTypeCombobox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(reportedByTxt.Text) ||
                priorityCombobox.SelectedItem == null ||
                DpDeadline.SelectedDate == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!IsValidName(subjectOfIncidenttxt.Text) || !IsValidName(reportedByTxt.Text) || !IsValidName(txtDescription.Text))
            {
                MessageBox.Show("Please enter valid values for certain fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        private bool IsValidName(string input)
        {
            // Use a regular expression to check if the input contains only letters
            return System.Text.RegularExpressions.Regex.IsMatch(input, "^[a-zA-Z]+$");
        }

    }
}
