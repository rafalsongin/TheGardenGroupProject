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
            ticketService = new TicketService();
            TicketList = new ObservableCollection<Ticket>(ticketService.GetAllTickets());

            creatingComboBox();

            FillComboBoxesFiltering(); // used for Kim's personal functionality

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
        private void creatingComboBox()
        {  //filling the cobboboxes with the enums
            priorityCombobox.ItemsSource = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            incidentTypeCombobox.ItemsSource = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().ToList();
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


        // Kim individual Functionality: Filtering
        private void FillComboBoxesFiltering() // filling combo boxes for filtering
        {
            typeFilterComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            priorityFilterComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
            statusFilterComboBox.ItemsSource = Enum.GetValues(typeof(Status));
            var dataSource = new List<string>();
            dataSource.Add("Today");
            dataSource.Add("This Month");
            dataSource.Add("This Year");
            dateReportedFilterComboBox.ItemsSource = dataSource;
        }
        private void ClearFilter_Click(object sender, RoutedEventArgs e) // remove the filters and show normal list without filters
        {
            searchTextBox.Text = "";
            typeFilterComboBox.SelectedIndex = -1;
            priorityFilterComboBox.SelectedIndex = -1;
            dateReportedFilterComboBox.SelectedIndex = -1;
            statusFilterComboBox.SelectedIndex = -1;
            RefreshTableView();
        }

        private void FilterSelected(object sender, SelectionChangedEventArgs e) // when an item is selected from one of the filtering combo boxes show filtered list
        {
            List<Ticket> tickets = ticketService.GetAllTickets();
            ShowFilteredList(tickets);
        }

        private void SearchTyped(object sender, TextChangedEventArgs e) // when something is typed in the searched show filtered list
        {
            List<Ticket> tickets = ticketService.GetAllTickets();
            ShowFilteredList(tickets);
        }

        private void ShowFilteredList(List<Ticket> tickets) // filter list then show filtered list
        {
            List<Ticket> filteredList = FilterList(tickets);
            ticketsListView.ItemsSource = filteredList;
        }

        private List<Ticket> FilterList(List<Ticket> tickets) // filtering tickets based on type, priority, date, subject, description, status
        {
            FilteringService filteringService = new FilteringService();
            List<Ticket> filteredTickets = tickets;
            if (searchTextBox.Text.Length > 0)
            {
                filteredTickets = filteringService.FilterSearch(filteredTickets, searchTextBox.Text);
            }
            if (typeFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterType(filteredTickets, (IncidentType)typeFilterComboBox.SelectedItem);
            }
            if (priorityFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterPriority(filteredTickets, (Priority)priorityFilterComboBox.SelectedItem);
            }
            if (dateReportedFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterDate(filteredTickets, dateReportedFilterComboBox.SelectedItem.ToString());
            }
            if (statusFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterStatus(filteredTickets, (Status)statusFilterComboBox.SelectedItem);
            }
            return filteredTickets;
        }

        // Kim individual Functionality end
    }
}