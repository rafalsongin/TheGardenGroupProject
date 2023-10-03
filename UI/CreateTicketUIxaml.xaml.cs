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
        }

        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            string dateFormat ="yyyy-MM-dd";
            try
            {
                Ticket ticket = new Ticket
                {

                    DateReported = DateTime.ParseExact(txtTimeReported.Text, dateFormat, CultureInfo.InvariantCulture),
                    Subject = txtSubjectOfIncident.Text,
                    IncidentType =(IncidentType)boxIncidentType.SelectedValue,
                    Assignedby = txtReportedBy.Text,
                    Priority = (Priority)boxPriority.SelectedValue,
                    Deadline = DateTime.ParseExact(txtDeadLine.Text,dateFormat,CultureInfo.InvariantCulture),

                };

                

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
                txtTimeReported.Text = "";
                txtSubjectOfIncident.Text = "";
                boxIncidentType.SelectedIndex = -1; // Clear the selection
                txtReportedBy.Text = "";
                boxPriority.SelectedIndex = -1; // Clear the selection
                txtDeadLine.Text = "";
            }
        }
    }
}
