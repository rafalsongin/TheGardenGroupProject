using System;
using Model;
using Service;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Windows;
using DAL;
using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Wpf;
using UI;

namespace TheGardenGroupProject
{
    public partial class DashboardUI : Window
    {
        
        
        private User ActiveUser { get; }
        public SeriesCollection TicketStatusData { get; set; }
        public Func<ChartPoint, string> TicketStatusLabelPoint { get; set; }

        public DashboardUI(string username)
        {
            InitializeComponent();
            
            // Set ActiveUser so that it can be used in other methods
            UserService userService = new UserService();
            ActiveUser = userService.GetUserByUsername(username);
            LabelDisplayLoggedInUsername.Content = "Logged in as: " + ActiveUser.Username;
            
            DataContext = this;
            
            // Display PieChart
            DisplayPieChart();
        }

        /*public DashboardUI()
        {
            InitializeComponent();
        }*/

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginUI loginWindow = new LoginUI();
            loginWindow.Show();
            this.Close();
        }
        
        private void DisplayPieChart()
        {
            var ticketDao = new TicketDao();
            var tickets = ticketDao.GetAllTickets();

            List<Ticket> openedTickets = new List<Ticket>();
            List<Ticket> resolvedTickets = new List<Ticket>();
            List<Ticket> closedTickets = new List<Ticket>();
            
            foreach (var ticket in tickets)
            {
                if (ticket.Status == Status.Opened)
                {
                    openedTickets.Add(ticket);
                }
                else if (ticket.Status == Status.Resolved)
                {
                    resolvedTickets.Add(ticket);
                }
                else if (ticket.Status == Status.Closed)
                {
                    closedTickets.Add(ticket);
                }
            }
            
            SeriesCollection pieSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Opened tickets",
                    Values = new ChartValues<double> { openedTickets.Count },
                    DataLabels = true,
                    FontSize = 20
                },
                new PieSeries
                {
                    Title = "Resolved tickets",
                    Values = new ChartValues<double> { resolvedTickets.Count },
                    DataLabels = true,
                    FontSize = 20
                },
                new PieSeries
                {
                    Title = "Closed tickets",
                    Values = new ChartValues<double> { closedTickets.Count },
                    DataLabels = true,
                    FontSize = 20
                }
            };

            PieChart pieChart = new PieChart
            {
                Series = pieSeries,
                LegendLocation = LegendLocation.Right,
                Width = 400,
                Height = 400,
                Margin = new Thickness(10, 10, 10, 10),
                Hoverable = true,
                DisableAnimations = true,
                DataTooltip = null
            };
            
            ChartContainer.Children.Add(pieChart);
            
        }
    }
}
