using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for RUDTicket.xaml
    /// </summary>
    public partial class RUDTicket : Window
    {
        public List<Ticket> TicketList { get; set; }

        private TicketService ticketService;
        public List<Priority> Priorities { get; set; }
        public List<IncidentType> IncidentTypes { get; set; }
        public List<Status> Statuses { get; set; }
        public RUDTicket()
        {
            ticketService = new TicketService();
            InitializeComponent();
            TicketList = ticketService.GetAllTickets();
            DataContext = this;

            Priorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().ToList();
            Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        }

  private void btnUpdateTicekt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the selected ticket from the list or other source
                Ticket selectedTicket = (Ticket)ticketsListView.SelectedValue;

                // Update the properties of the selected ticket with the values from the UI
                selectedTicket.Subject = subjectTxt.Text; // Replace with the actual UI elements
                selectedTicket.IncidentType = (IncidentType)incidentTypeCombobox.SelectedItem;
                selectedTicket.ReportedBy = reportedByTxt.Text;
                selectedTicket.Priority = (Priority)priorityCombobox.SelectedItem;
                selectedTicket.Deadline = DPDeadLine.SelectedDate ?? DateTime.MinValue;
                selectedTicket.Status = (Status)statusCombobox.SelectedItem;
                selectedTicket.ReportedOn=DPTimeReported.SelectedDate ?? DateTime.MinValue;

                // Call the service layer to update the ticket
                ticketService.UpdateTicket(selectedTicket);
                RefreshListView();

                MessageBox.Show("Ticket updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      

        private void btnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {
           
        }
        

        private void btnCreateIncidentTicket_Click(object sender, RoutedEventArgs e)
        {
            CreateTicketUIxaml createTicketUI = new CreateTicketUIxaml();
            createTicketUI.Show();
            this.Close();
        }

        private void ticketsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ticketsListView.SelectedItem != null)
            {
                Ticket selectedTicket = (Ticket)ticketsListView.SelectedValue;

                subjectTxt.Text = selectedTicket.Subject;
                priorityCombobox.SelectedValue = selectedTicket.Priority;
                reportedByTxt.Text = selectedTicket.ReportedBy;
                DPTimeReported.SelectedDate=selectedTicket.ReportedOn;
                DPDeadLine.SelectedDate = selectedTicket.Deadline;
                incidentTypeCombobox.SelectedValue = selectedTicket.IncidentType;
                statusCombobox.SelectedValue = selectedTicket.Status;

            }
        }
        private void RefreshListView()
        {
            using (ticketsListView.Items.DeferRefresh())
            {
                // Make your changes to the collection
                TicketList.Clear();
                foreach (var ticket in ticketService.GetAllTickets())
                {
                    TicketList.Add(ticket);
                }
            }
        }
    }
}
