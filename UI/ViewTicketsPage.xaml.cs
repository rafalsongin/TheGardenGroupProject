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
        private ObservableCollection<Ticket> TicketList { get; }

        private readonly TicketService _ticketService;


        public ViewTicketsPage()
        {
            InitializeComponent();
            _ticketService = new TicketService();


            // TicketList = new ObservableCollection<Ticket>(_ticketService.GetAllTickets()); - default list
            TicketList =
                new ObservableCollection<Ticket>(SortingTickets()); // sorted list (Dana's individual functionality)


            CreatingComboBox();
            FillComboBoxesFiltering(); // used for Kim's personal functionality

            DataContext = this;
            TicketsListView.ItemsSource = TicketList;
        }

        private void UpdateTicketButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsTicketSelected())
                {
                    return;
                }

                // Get the selected ticket from the list
                Ticket selectedTicket = (Ticket)TicketsListView.SelectedItem;

                // Check if any changes are made in the UI elements
                if (!AreChangesMade(selectedTicket))
                {
                    MessageBox.Show("No changes made to the ticket.", "Information", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(SubjectOfIncidentTextBox.Text) ||
                         string.IsNullOrWhiteSpace(ReportedByTxt.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                // Update the properties of the selected ticket with the values from the UI
                UpdateTicketProperties(selectedTicket);

                _ticketService.UpdateTicket(selectedTicket);
                RefreshTableView();
                ClearUiElements();

                MessageBox.Show("Ticket updated successfully!", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void DeleteTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTicketSelected())
            {
                return;
            }

            Ticket selectedTicket = (Ticket)TicketsListView.SelectedItem;
            _ticketService.DeleteTicket(selectedTicket);
            MessageBox.Show("Ticket deleted successfully!", "Success", MessageBoxButton.OK,
                MessageBoxImage.Information);
            RefreshTableView();

            ClearUiElements();
        }


        private void ClearUiElements()
        {
            ReportedByTxt.Text = "";
            SubjectOfIncidentTextBox.Text = "";

            PriorityCombobox.SelectedIndex = -1;
            IncidentTypeCombobox.SelectedIndex = -1;
            StatusCombobox.SelectedIndex = -1;

            DpDeadLine.SelectedDate = null;
            DpTimeReported.SelectedDate = null;
        }

        private void CreatingComboBox()
        {
            //filling the combo boxes with the enums
            PriorityCombobox.ItemsSource = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            IncidentTypeCombobox.ItemsSource = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().ToList();
            var statusValues = Enum.GetValues(typeof(Status)).Cast<Status>().Where(s => s != Status.Pending).ToList();
            StatusCombobox.ItemsSource = statusValues;
        }

        private void RefreshTableView()
        {
            TicketList.Clear();
            foreach (Ticket ticket in _ticketService.GetAllTickets())
            {
                TicketList.Add(ticket);
            }
        }

        private bool AreChangesMade(Ticket selectedTicket)
        {
            // Compare the properties of the selected ticket with the values from the UI
            return selectedTicket.Subject != SubjectOfIncidentTextBox.Text
                   || selectedTicket.IncidentType != (IncidentType)IncidentTypeCombobox.SelectedItem
                   || selectedTicket.ReportedBy != ReportedByTxt.Text
                   || selectedTicket.Priority != (Priority)PriorityCombobox.SelectedItem
                   || selectedTicket.Deadline != (DpDeadLine.SelectedDate ?? DateTime.MinValue)
                   || selectedTicket.Status != (Status)StatusCombobox.SelectedItem
                   || selectedTicket.ReportedOn != (DpTimeReported.SelectedDate ?? DateTime.MinValue);
        }

        private void UpdateTicketProperties(Ticket ticket)
        {
            ticket.Subject = SubjectOfIncidentTextBox.Text; // Replace with the actual UI elements
            ticket.IncidentType = (IncidentType)IncidentTypeCombobox.SelectedItem;
            ticket.ReportedBy = ReportedByTxt.Text;
            ticket.Priority = (Priority)PriorityCombobox.SelectedItem;
            ticket.Deadline = DpDeadLine.SelectedDate ?? DateTime.MinValue;
            ticket.Status = (Status)StatusCombobox.SelectedItem;
            ticket.ReportedOn = DpTimeReported.SelectedDate ?? DateTime.MinValue;
        }

        private void ticketsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TicketsListView.SelectedItem != null)
            {
                Ticket selectedTicket = (Ticket)TicketsListView.SelectedValue;

                SubjectOfIncidentTextBox.Text = selectedTicket.Subject;
                PriorityCombobox.SelectedValue = selectedTicket.Priority;
                ReportedByTxt.Text = selectedTicket.ReportedBy;
                DpTimeReported.SelectedDate = selectedTicket.ReportedOn;
                DpDeadLine.SelectedDate = selectedTicket.Deadline;
                IncidentTypeCombobox.SelectedValue = selectedTicket.IncidentType;
                StatusCombobox.SelectedValue = selectedTicket.Status;
            }
        }

        private bool IsTicketSelected()
        {
            if (TicketsListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a ticket.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        //Ghonim individual Functionality
        private void ArchiveTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTicketSelected())
            {
                return;
            }

            Ticket selectedTicket = (Ticket)TicketsListView.SelectedValue;

            if (selectedTicket.Status != Status.Closed)
            {
                MessageBox.Show("Only tickets with a closed status can be archived.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            ArchivingService archivingTicket = new ArchivingService();
            archivingTicket.ArchiveTicket(selectedTicket);
            _ticketService.DeleteTicket(selectedTicket);
            RefreshTableView();
            ClearUiElements();
            MessageBox.Show("Ticket archived successfully!", "Success", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        // Ghonim individual Functionality end


        private List<Ticket> SortingTickets()
        {
            SortingTicketsService sortingTicketsService = new SortingTicketsService();
            return sortingTicketsService.GetAllTicketsSortedByPriorityDescending();
        }

        // Kim individual Functionality: Filtering
        private void FillComboBoxesFiltering() // filling combo boxes for filtering
        {
            TypeFilterComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            PriorityFilterComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
            StatusFilterComboBox.ItemsSource = Enum.GetValues(typeof(Status));
            var dataSource = new List<string>();
            dataSource.Add("Today");
            dataSource.Add("This Month");
            dataSource.Add("This Year");
            DateReportedFilterComboBox.ItemsSource = dataSource;
        }

        private void ClearFilter_Click(object sender,
            RoutedEventArgs e) // remove the filters and show normal list without filters
        {
            SearchTextBox.Text = "";
            TypeFilterComboBox.SelectedIndex = -1;
            PriorityFilterComboBox.SelectedIndex = -1;
            DateReportedFilterComboBox.SelectedIndex = -1;
            StatusFilterComboBox.SelectedIndex = -1;
            RefreshTableView();
        }

        private void FilterSelected(object sender,
            SelectionChangedEventArgs e) // when an item is selected from one of the filtering combo boxes show filtered list
        {
            List<Ticket> tickets = _ticketService.GetAllTickets();
            ShowFilteredList(tickets);
        }

        private void SearchTyped(object sender,
            TextChangedEventArgs e) // when something is typed in the searched show filtered list
        {
            List<Ticket> tickets = _ticketService.GetAllTickets();
            ShowFilteredList(tickets);
        }

        private void ShowFilteredList(List<Ticket> tickets) // filter list then show filtered list
        {
            List<Ticket> filteredList = FilterList(tickets);
            TicketsListView.ItemsSource = filteredList;
        }

        private List<Ticket>
            FilterList(List<Ticket> tickets) // filtering tickets based on type, priority, date, subject, description, status
        {
            FilteringService filteringService = new FilteringService();
            List<Ticket> filteredTickets = tickets;
            if (SearchTextBox.Text.Length > 0)
            {
                filteredTickets = filteringService.FilterSearch(filteredTickets, SearchTextBox.Text);
            }

            if (TypeFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets =
                    filteringService.FilterType(filteredTickets, (IncidentType)TypeFilterComboBox.SelectedItem);
            }

            if (PriorityFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets =
                    filteringService.FilterPriority(filteredTickets, (Priority)PriorityFilterComboBox.SelectedItem);
            }

            if (DateReportedFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets =
                    filteringService.FilterDate(filteredTickets, DateReportedFilterComboBox.SelectedItem.ToString());
            }

            if (StatusFilterComboBox.SelectedIndex != -1)
            {
                filteredTickets =
                    filteringService.FilterStatus(filteredTickets, (Status)StatusFilterComboBox.SelectedItem);
            }

            return filteredTickets;
        }
        // Kim individual Functionality end
    }
}