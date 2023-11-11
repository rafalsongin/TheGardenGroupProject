using Model;
using MongoDB.Driver.Linq;
using Service;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for ViewTicketsForEmployeePage.xaml
    /// </summary>
    public partial class ViewTicketsForEmployeePage : Page
    {
        User user;
        Brush selectedColour = new SolidColorBrush(Color.FromRgb(60, 115, 96));
        Brush notSelectedColour = new SolidColorBrush(Color.FromRgb(162, 222, 202));

        public ViewTicketsForEmployeePage(User user)
        {
            this.user = user;
            InitializeComponent();
            LoadComboBoxes();
            ResetButtons();

            if (user.UserType == UserType.CompanyEmployee) // if the user is a company employee dont show the buttons for the manager and set colours correctly
            {
                allButton.Background = selectedColour;
                EmployeeView();
                ViewAllTicketsEmployee();
            }
            else if (user.UserType == UserType.Manager) // if the user is a manager show all buttons and set colours correctly
            {
                allButton.Background = selectedColour;
                everyButton.Background = selectedColour;
                ViewAllTickets();
            }
        }

        private void ResetButtons()
        {
            allButton.Background = notSelectedColour;
            openButton.Background = notSelectedColour;
            closedButton.Background = notSelectedColour;
        }

        private void LoadComboBoxes() // filling one combo box with all the values of the enum 'IncidentType' and the other combo box with all the values of the enum 'Priority'
        {
            typeComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
            var dataSource = new List<string>();
            dataSource.Add("Today");
            dataSource.Add("This Month");
            dataSource.Add("This Year");
            dateAddedComboBox.ItemsSource = dataSource;
        }

        private void EmployeeView() // hide manager buttons
        {
            ownButton.Visibility = Visibility.Hidden;
            everyButton.Visibility = Visibility.Hidden;
        }

        private void ViewAllTicketsEmployee() // view all tickets from the user
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetAllTicketsFromUser(user);
            tickets = FilterList(tickets);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewOpenTicketsEmployee() // view all pending and open tickets from the user
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetOpenTicketsFromUser(user);
            tickets = FilterList(tickets);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewClosedTicketsEmployee() // view all closed and resolved tickets from the user
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetClosedTicketsFromUser(user);
            tickets = FilterList(tickets);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllTickets() // view all tickets
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetAllTickets();
            tickets = FilterList(tickets);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllOpenTickets() // view all pending and open tickets
        {
            TicketService ticketService = new();
            List<Ticket> tickets = ticketService.GetAllOpenTickets();
            tickets = FilterList(tickets);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllClosedTickets() // view all closed and resolved tickets
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAllClosedTickets();
            tickets = FilterList(tickets);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ShowCorrectView() // fill the list view with the correct data according to what buttons are active
        {
            if (user.UserType == UserType.CompanyEmployee || ((SolidColorBrush)ownButton.Background) == selectedColour)
            {
                if (((SolidColorBrush)allButton.Background) == selectedColour)
                {
                    ViewAllTicketsEmployee();
                }
                else if (((SolidColorBrush)openButton.Background) == selectedColour)
                {
                    ViewOpenTicketsEmployee();
                }
                else if (((SolidColorBrush)closedButton.Background) == selectedColour)
                {
                    ViewClosedTicketsEmployee();
                }
            }
            else if (((SolidColorBrush)everyButton.Background) == selectedColour)
            {
                if (((SolidColorBrush)allButton.Background) == selectedColour)
                {
                    ViewAllTickets();
                }
                else if (((SolidColorBrush)openButton.Background) == selectedColour)
                {
                    ViewAllOpenTickets();
                }
                else if (((SolidColorBrush)closedButton.Background) == selectedColour)
                {
                    ViewAllClosedTickets();
                }
            }
        }

        private List<Ticket> FilterList(List<Ticket> tickets) // filtering tickets based on type, priority and date
        {
            FilteringService filteringService = new FilteringService();
            List<Ticket> filteredTickets = tickets;
            if (typeComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterType(filteredTickets, (IncidentType)typeComboBox.SelectedItem);
            }
            if (priorityComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterPriority(filteredTickets, (Priority)priorityComboBox.SelectedItem);
            }
            if (dateAddedComboBox.SelectedIndex != -1)
            {
                filteredTickets = filteringService.FilterDate(filteredTickets, dateAddedComboBox.SelectedItem.ToString());
            }
            return filteredTickets;
        }

        private void AllTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            allButton.Background = selectedColour;
            ShowCorrectView();
        }

        private void OpenTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            openButton.Background = selectedColour;
            ShowCorrectView();
        }

        private void ClosedTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            closedButton.Background = selectedColour;
            ShowCorrectView();
        }

        private void OwnTickets_Click(object sender, RoutedEventArgs e)
        {
            everyButton.Background = notSelectedColour;
            ownButton.Background = selectedColour;
            ShowCorrectView();
        }

        private void EveryTickets_Click(object sender, RoutedEventArgs e)
        {
            everyButton.Background = selectedColour;
            ownButton.Background = notSelectedColour;
            ShowCorrectView();
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            typeComboBox.SelectedIndex = -1;
            priorityComboBox.SelectedIndex = -1;
            dateAddedComboBox.SelectedIndex = -1;
            ShowCorrectView();
        }

        private void FilterSelected(object sender, SelectionChangedEventArgs e)
        {
            ShowCorrectView();
        }
    }
}
