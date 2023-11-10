using Model;
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
            if (user.UserType == UserType.CompanyEmployee)
            {
                ResetButtons();
                allButton.Background = selectedColour;
                EmployeeView();
                ViewAllTicketsEmployee();
            }
            else if (user.UserType == UserType.Manager)
            {
                allButton.Background = selectedColour;
                everyButton.Background = selectedColour;
                ViewAllTickets();
            }
        }

        private void ViewAllTicketsEmployee()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAllTicketsFromUser(user);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewOpenTicketsEmployee()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetOpenTicketsFromUser(user);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewClosedTicketsEmployee()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetClosedTicketsFromUser(user);
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllTickets()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAllTickets();
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllOpenTickets()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAllOpenTickets();
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ViewAllClosedTickets()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAllClosedTickets();
            viewTicketsListView.ItemsSource = tickets;
        }

        private void ShowCorrectView()
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

        private void ResetButtons()
        {
            allButton.Background = notSelectedColour;
            openButton.Background = notSelectedColour;
            closedButton.Background = notSelectedColour;
        }

        private void EmployeeView()
        {
            ownButton.Visibility = Visibility.Hidden;
            everyButton.Visibility = Visibility.Hidden;
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

        private void EveryTickets_Click(Object sender, RoutedEventArgs e)
        {
            everyButton.Background = selectedColour;
            ownButton.Background = notSelectedColour;
            ShowCorrectView();
        }
    }
}
