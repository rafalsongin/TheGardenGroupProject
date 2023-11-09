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
using System.Windows.Shapes;
using Model;
using Service;

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for ViewTickets.xaml
    /// </summary>
    public partial class ViewTickets : Window
    {
        User user;
        public ViewTickets()
        {
            user = new User("testing", "kim123", "Kim", "van Schagen", UserType.Manager, "smt", "000", City.Amsterdam);
            InitializeComponent();            
            if (user.UserType == UserType.CompanyEmployee)
            {
                ResetButtons();
                allButton.Background = Brushes.Green;
                EmployeeView();
                ViewAllTicketsEmployee();
            }
            else
            {
                allButton.Background = Brushes.Green;
                everyButton.Background = Brushes.Green;
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
            if (user.UserType == UserType.CompanyEmployee || ((SolidColorBrush)ownButton.Background).Color == Colors.Green)
            {
                if (((SolidColorBrush)allButton.Background).Color == Colors.Green)
                {
                    ViewAllTicketsEmployee();
                }
                else if (((SolidColorBrush)openButton.Background).Color == Colors.Green)
                {
                    ViewOpenTicketsEmployee();
                }
                else if (((SolidColorBrush)closedButton.Background).Color == Colors.Green)
                {
                    ViewClosedTicketsEmployee();
                }
            }
            else if (((SolidColorBrush)everyButton.Background).Color == Colors.Green)
            {
                if (((SolidColorBrush)allButton.Background).Color == Colors.Green)
                {
                    ViewAllTickets();
                }
                else if (((SolidColorBrush)openButton.Background).Color == Colors.Green)
                {
                    ViewAllOpenTickets();
                }
                else if (((SolidColorBrush)closedButton.Background).Color == Colors.Green)
                {
                    ViewAllClosedTickets();
                }
            }
        }

        private void ResetButtons()
        {
            allButton.Background = Brushes.Beige;
            openButton.Background = Brushes.Beige;
            closedButton.Background = Brushes.Beige;
/*            everyButton.Background = Brushes.Beige;
            ownButton.Background = Brushes.Beige;*/
        }

        private void EmployeeView()
        {
            ownButton.Visibility = Visibility.Hidden;
            everyButton.Visibility = Visibility.Hidden;
        }

        private void AllTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            allButton.Background = Brushes.Green;
            ShowCorrectView();
        }

        private void OpenTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            openButton.Background = Brushes.Green;
            ShowCorrectView();
        }

        private void ClosedTickets_Click(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            closedButton.Background = Brushes.Green;
            ShowCorrectView();
        }

        private void OwnTickets_Click(object sender, RoutedEventArgs e)
        {
            everyButton.Background = Brushes.Beige;
            ownButton.Background = Brushes.Green;
            ShowCorrectView();
        }

        private void EveryTickets_Click(Object sender, RoutedEventArgs e)
        {
            everyButton.Background= Brushes.Beige;
            ownButton.Background= Brushes.Green;
            ShowCorrectView();
        }
    }
}
