using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Model;
using Service;

namespace TheGardenGroupProject
{
    public partial class ViewTicketsForEmployeePage : Page
    {
        private readonly User _user;
        private readonly Brush _selectedColour = new SolidColorBrush(Color.FromRgb(60, 115, 96));
        private readonly Brush _notSelectedColour = new SolidColorBrush(Color.FromRgb(162, 222, 202));

        public ViewTicketsForEmployeePage(User user)
        {
            _user = user;
            InitializeComponent();
            LoadComboBoxes();
            ResetButtons();

            if (user.UserType == UserType.CompanyEmployee) // if the user is a company employee dont show the buttons for the manager and set colours correctly
            {
                AllButton.Background = _selectedColour;
                EmployeeView();
                ViewAllTicketsEmployee();
            }
            else if (user.UserType == UserType.Manager) // if the user is a manager show all buttons and set colours correctly
            {
                AllButton.Background = _selectedColour;
                EveryButton.Background = _selectedColour;
                ViewAllTickets();
            }
        }

        private void ResetButtons()
        {
            AllButton.Background = _notSelectedColour;
            OpenButton.Background = _notSelectedColour;
            ClosedButton.Background = _notSelectedColour;
        }

        private void LoadComboBoxes() // filling one combo box with all the values of the enum 'IncidentType' and the other combo box with all the values of the enum 'Priority'
        {
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
            var dataSource = new List<string>();
            dataSource.Add("Today");
            dataSource.Add("This Month");
            dataSource.Add("This Year");
            DateAddedComboBox.ItemsSource = dataSource;
        }

        private void EmployeeView() // hide manager buttons
        {
            OwnButton.Visibility = Visibility.Hidden;
            EveryButton.Visibility = Visibility.Hidden;
        }

        private void ViewAllTicketsEmployee() // view all tickets from the user
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetAllTicketsFromUser(_user);
            tickets = FilterList(tickets);
            ViewTicketsListView.ItemsSource = tickets;
        }

        private void ViewOpenTicketsEmployee() // view all pending and open tickets from the user
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetOpenTicketsFromUser(_user);
            tickets = FilterList(tickets);
            ViewTicketsListView.ItemsSource = tickets;
        }

        private void ViewClosedTicketsEmployee() // view all closed and resolved tickets from the user
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetClosedTicketsFromUser(_user);
            tickets = FilterList(tickets);
            ViewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllTickets() // view all tickets
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetAllTickets();
            tickets = FilterList(tickets);
            ViewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllOpenTickets() // view all pending and open tickets
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetAllOpenTickets();
            tickets = FilterList(tickets);
            ViewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllClosedTickets() // view all closed and resolved tickets
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAllClosedTickets();
            tickets = FilterList(tickets);
            ViewTicketsListView.ItemsSource = tickets;
        }

        private void ShowCorrectView() // fill the list view with the correct data according to what buttons are active
        {
            if (_user.UserType == UserType.CompanyEmployee || ((SolidColorBrush)OwnButton.Background) == _selectedColour)
            {
                if (((SolidColorBrush)AllButton.Background) == _selectedColour)
                {
                    ViewAllTicketsEmployee();
                }
                else if (((SolidColorBrush)OpenButton.Background) == _selectedColour)
                {
                    ViewOpenTicketsEmployee();
                }
                else if (((SolidColorBrush)ClosedButton.Background) == _selectedColour)
                {
                    ViewClosedTicketsEmployee();
                }
            }
            else if (((SolidColorBrush)EveryButton.Background) == _selectedColour)
            {
                if (((SolidColorBrush)AllButton.Background) == _selectedColour)
                {
                    ViewAllTickets();
                }
                else if (((SolidColorBrush)OpenButton.Background) == _selectedColour)
                {
                    ViewAllOpenTickets();
                }
                else if (((SolidColorBrush)ClosedButton.Background) == _selectedColour)
                {
                    ViewAllClosedTickets();
                }
            }
        }

        private List<Ticket> FilterList(List<Ticket> tickets) // filtering tickets based on type, priority and date
        {
            FilteringService filteringService = new FilteringService();
            List<Ticket> filteredTickets = tickets;
            if (SearchTextBox.Text.Length > 0)
            {
                filteredTickets = filteringService.FilterSearch(filteredTickets, SearchTextBox.Text);
            }
            if (TypeComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterType(filteredTickets, (IncidentType)TypeComboBox.SelectedItem);
            }
            if (PriorityComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterPriority(filteredTickets, (Priority)PriorityComboBox.SelectedItem);
            }
            if (DateAddedComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterDate(filteredTickets, DateAddedComboBox.SelectedItem.ToString());
            }
            return filteredTickets;
        }

        private void AllTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            AllButton.Background = _selectedColour;
            ShowCorrectView();
        }

        private void OpenTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            OpenButton.Background = _selectedColour;
            ShowCorrectView();
        }

        private void ClosedTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            ClosedButton.Background = _selectedColour;
            ShowCorrectView();
        }

        private void OwnTickets_Click(object sender, RoutedEventArgs e)
        {
            EveryButton.Background = _notSelectedColour;
            OwnButton.Background = _selectedColour;
            ShowCorrectView();
        }

        private void EveryTickets_Click(object sender, RoutedEventArgs e)
        {
            EveryButton.Background = _selectedColour;
            OwnButton.Background = _notSelectedColour;
            ShowCorrectView();
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            TypeComboBox.SelectedIndex = -1;
            PriorityComboBox.SelectedIndex = -1;
            DateAddedComboBox.SelectedIndex = -1;
            SearchTextBox.Text = "";
            ShowCorrectView();
        }

        private void FilterSelected(object sender, SelectionChangedEventArgs e)
        {
            ShowCorrectView();
        }

        private void SearchTyped(object sender, TextChangedEventArgs e) // when something is typed in the searched show filtered list
        {
            ShowCorrectView();
        }
    }
}
