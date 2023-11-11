using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace TheGardenGroupProject
{

    public partial class ViewTicketsPage : Page // made by Ghonim
    {
        public ObservableCollection<Ticket> TicketList { get; set; }

        private TicketService ticketService;

     

        public ViewTicketsPage()
        {
            InitializeComponent();
       ticketService=new TicketService();
     TicketList = new ObservableCollection<Ticket>(ticketService.GetAllTickets());
           
            creatingComboBox();
            DataContext = this;

          
 

        }

        private void btnUpdateTicekt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsTicketSelected())
                {
                    return;
                }

                // Get the selected ticket from the list
                Ticket selectedTicket = (Ticket)ticketsListView.SelectedItem;

                // Check if any changes are made in the UI elements
                if (!AreChangesMade(selectedTicket))
                {
                    MessageBox.Show("No changes made to the ticket.", "Information", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(subjectOfIncidenttxt.Text) ||
                         string.IsNullOrWhiteSpace(reportedByTxt.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                // Update the properties of the selected ticket with the values from the UI
                UpdateTicketProperties(selectedTicket);

                ticketService.UpdateTicket(selectedTicket);
                RefreshTableView();
                ClearUIElements();

                MessageBox.Show("Ticket updated successfully!", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTicketSelected())
            {
                return;
            }

            Ticket selectedTicket = (Ticket)ticketsListView.SelectedItem;
            ticketService.DeleteTicket(selectedTicket);
            MessageBox.Show("Ticket deleted successfully!", "Success", MessageBoxButton.OK,
                MessageBoxImage.Information);
            RefreshTableView();

            ClearUIElements(); 
         }



        private void ClearUIElements()
        {
            reportedByTxt.Text = "";
            subjectOfIncidenttxt.Text = "";

            priorityCombobox.SelectedIndex = -1;
            incidentTypeCombobox.SelectedIndex = -1;
            statusCombobox.SelectedIndex = -1;

            DPDeadLine.SelectedDate = null;
            DPTimeReported.SelectedDate = null;
        }
        private void creatingComboBox ()
        {  //filling the cobboboxes with the enums
            priorityCombobox.ItemsSource= Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            incidentTypeCombobox.ItemsSource=Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().ToList();
            var statusValues = Enum.GetValues(typeof(Status)).Cast<Status>().Where(s => s != Status.Pending).ToList();
            statusCombobox.ItemsSource = statusValues;
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
            return selectedTicket.Subject != subjectOfIncidenttxt.Text
                   || selectedTicket.IncidentType != (IncidentType)incidentTypeCombobox.SelectedItem
                   || selectedTicket.ReportedBy != reportedByTxt.Text
                   || selectedTicket.Priority != (Priority)priorityCombobox.SelectedItem
                   || selectedTicket.Deadline != (DPDeadLine.SelectedDate ?? DateTime.MinValue)
                   || selectedTicket.Status != (Status)statusCombobox.SelectedItem
                   || selectedTicket.ReportedOn != (DPTimeReported.SelectedDate ?? DateTime.MinValue);
        }

        private void UpdateTicketProperties(Ticket ticket)
        {
            ticket.Subject = subjectOfIncidenttxt.Text; // Replace with the actual UI elements
            ticket.IncidentType = (IncidentType)incidentTypeCombobox.SelectedItem;
            ticket.ReportedBy = reportedByTxt.Text;
            ticket.Priority = (Priority)priorityCombobox.SelectedItem;
            ticket.Deadline = DPDeadLine.SelectedDate ?? DateTime.MinValue;
            ticket.Status = (Status)statusCombobox.SelectedItem;
            ticket.ReportedOn = DPTimeReported.SelectedDate ?? DateTime.MinValue;
        }

        private void ticketsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ticketsListView.SelectedItem != null)
            {
                Ticket selectedTicket = (Ticket)ticketsListView.SelectedValue;

                subjectOfIncidenttxt.Text = selectedTicket.Subject;
                priorityCombobox.SelectedValue = selectedTicket.Priority;
                reportedByTxt.Text = selectedTicket.ReportedBy;
                DPTimeReported.SelectedDate = selectedTicket.ReportedOn;
                DPDeadLine.SelectedDate = selectedTicket.Deadline;
                incidentTypeCombobox.SelectedValue = selectedTicket.IncidentType;
                statusCombobox.SelectedValue = selectedTicket.Status;
            }
        }

        private bool IsTicketSelected()
        {
            if (ticketsListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a ticket.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        //Ghonim individual Functionality
        private void btnArchiveTicekt_Click(object sender, RoutedEventArgs e) 
        {
            if (!IsTicketSelected())
            {
                return;
            }
            Ticket selectedTicket = (Ticket)ticketsListView.SelectedValue;

            if (selectedTicket.Status != Status.Closed)
            {
                MessageBox.Show("Only tickets with a closed status can be archived.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ArchivingService archivingTicket = new ArchivingService();
            archivingTicket.ArchiveTicket(selectedTicket);
            ticketService.DeleteTicket(selectedTicket);
            RefreshTableView();
            ClearUIElements();
            MessageBox.Show("Ticket archived successfully!", "Success", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        //Ghonim individual Functionality end


        private void sortingTickets ()
        {
            SortingTicketsService sortingTicketsService = new SortingTicketsService();
            List<Ticket> tickets = sortingTicketsService.GetAllTicketsSortedByPriorityDescending();

        }

    }


}