using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class ViewTicketsPage : Page
    {
        public ObservableCollection<Ticket> TicketList { get; set; }
        private TicketService ticketService;
        public List<Priority> Priorities { get; set; }
        public List<IncidentType> IncidentTypes { get; set; }
        public List<Status> Statuses { get; set; }

        public ViewTicketsPage()
        {
            ticketService = new TicketService();
            TicketList = new ObservableCollection<Ticket>(ticketService.GetAllTickets());

            Priorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().ToList();
            Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();

            DataContext = this;
            InitializeComponent();
        }

      

        private void btnUpdateTicekt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ticketsListView.SelectedItem == null)
                {
                    MessageBox.Show("Please select a ticket to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Get the selected ticket from the list
                Ticket selectedTicket = (Ticket)ticketsListView.SelectedItem;

                // Check if any changes are made in the UI elements
                if (!AreChangesMade(selectedTicket))
                {
                    MessageBox.Show("No changes made to the ticket.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Update the properties of the selected ticket with the values from the UI
                selectedTicket.Subject = subjectTxt.Text; // Replace with the actual UI elements
                selectedTicket.IncidentType = (IncidentType)incidentTypeCombobox.SelectedItem;
                selectedTicket.ReportedBy = reportedByTxt.Text;
                selectedTicket.Priority = (Priority)priorityCombobox.SelectedItem;
                selectedTicket.Deadline = DPDeadLine.SelectedDate ?? DateTime.MinValue;
                selectedTicket.Status = (Status)statusCombobox.SelectedItem;
                selectedTicket.ReportedOn = DPTimeReported.SelectedDate ?? DateTime.MinValue;

                // Call the service layer to update the ticket
                ticketService.UpdateTicket(selectedTicket);
                RefreshTableView();
                ClearUIElements();

                MessageBox.Show("Ticket updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            if (ticketsListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a ticket to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        
            
                Ticket selectedTicket = (Ticket)ticketsListView.SelectedItem;
                ticketService.DeleteTicket(selectedTicket);
            MessageBox.Show("Ticket deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshTableView();


        }

        private void ClearUIElements()
        {
            reportedByTxt.Text = "";
            subjectTxt.Text = "";
               
            priorityCombobox.SelectedIndex = -1;
            incidentTypeCombobox.SelectedIndex = -1;
            statusCombobox.SelectedIndex = -1;

            DPDeadLine.SelectedDate = null;
            DPTimeReported.SelectedDate = null;
        }
        private void RefreshTableView()
        {
            TicketList.Clear();
            foreach (Ticket ticket in ticketService.GetAllTickets())
            {
                TicketList.Add(ticket);
            }
        }

        private bool AreChangesMade(Ticket selectedTicket)
        {
            // Compare the properties of the selected ticket with the values from the UI
            return selectedTicket.Subject != subjectTxt.Text
                || selectedTicket.IncidentType != (IncidentType)incidentTypeCombobox.SelectedItem
                || selectedTicket.ReportedBy != reportedByTxt.Text
                || selectedTicket.Priority != (Priority)priorityCombobox.SelectedItem
                || selectedTicket.Deadline != (DPDeadLine.SelectedDate ?? DateTime.MinValue)
                || selectedTicket.Status != (Status)statusCombobox.SelectedItem
                || selectedTicket.ReportedOn != (DPTimeReported.SelectedDate ?? DateTime.MinValue);
        }

        private void ticketsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ticketsListView.SelectedItem != null)
            {
                Ticket selectedTicket = (Ticket)ticketsListView.SelectedValue;

                subjectTxt.Text = selectedTicket.Subject;
                priorityCombobox.SelectedValue = selectedTicket.Priority;
                reportedByTxt.Text = selectedTicket.ReportedBy;
                DPTimeReported.SelectedDate = selectedTicket.ReportedOn;
                DPDeadLine.SelectedDate = selectedTicket.Deadline;
                incidentTypeCombobox.SelectedValue = selectedTicket.IncidentType;
                statusCombobox.SelectedValue = selectedTicket.Status;

            }
        }
    }
}