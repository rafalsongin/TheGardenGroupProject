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
    /// <summary>
    /// Interaction logic for CreateTicketUIxaml.xaml
    /// </summary>
    public partial class CreateTicketUIxaml : Window
    {
        private TicketService ticketService;

        public CreateTicketUIxaml()
        {
            InitializeComponent();
            ticketService = new TicketService();

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
                // Get the selected incident type as a string from the ComboBox
                string incidentType = ((ComboBoxItem)boxIncidentType.SelectedItem).Content.ToString();
                string ticketsPriority= ((ComboBoxItem)boxPriority.SelectedItem).Content.ToString();
                // Get the selected date from the DatePicker control
                DateTime deadLine = DpDeadline.SelectedDate ?? DateTime.MinValue;
                DateTime dateReported = DateTime.Now;
                IncidentType incident = (IncidentType)Enum.Parse(typeof(IncidentType), incidentType);
                Priority priority = (Priority)Enum.Parse(typeof(Priority),ticketsPriority);
                Ticket ticket = new Ticket(dateReported, txtSubjectOfIncident.Text, txtDescription.Text, incident,txtReportedBy.Text,priority, deadLine);
                
                ticketService.CreateTicket(ticket);
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
                DpTimeReported.SelectedDate = DateTime.Now;
                txtSubjectOfIncident.Text = "";
                boxIncidentType.SelectedIndex = -1; // Clear the selection
                txtReportedBy.Text = "";
                boxPriority.SelectedIndex = -1; 
                DpDeadline.SelectedDate = null;
            }
        }

        private void btnRUDTicket_Click(object sender, RoutedEventArgs e)
        {
            RUDTicket rudTicket = new RUDTicket();
            rudTicket.Show();
            this.Close();
        }
    }
}
