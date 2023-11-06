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

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginUI loginWindow = new LoginUI();
            loginWindow.Show();
            this.Close();
        }
        
        private void DisplayPieChart()
        {
            ChartContainer.Children.Add(GetPieChart());
        }

        private static PieChart GetPieChart()
        {
            TicketService ticketService = new TicketService();

            List<Ticket> openedTickets = ticketService.GetOpenedTickets();
            List<Ticket> resolvedTickets = ticketService.GetResolvedTickets();
            List<Ticket> closedTickets = ticketService.GetClosedTickets();
            
            SeriesCollection pieSeries = GetPieSeriesCollection(openedTickets, resolvedTickets, closedTickets);
            
            PieChart pieChart = new PieChart
            {
                Series = pieSeries,
                LegendLocation = LegendLocation.Right,
                Width = 400,
                Height = 400,
                Margin = new Thickness(350, 10, 10, 10),
                Hoverable = true,
                DisableAnimations = true,
                DataTooltip = null
            };
            return pieChart;
        }

        private static SeriesCollection GetPieSeriesCollection(List<Ticket> openedTickets, List<Ticket> resolvedTickets, List<Ticket> closedTickets)
        {
            SeriesCollection pieSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Opened tickets",
                    Values = new ChartValues<double> { openedTickets.Count },
                    DataLabels = true,
                    FontSize = 22
                },
                new PieSeries
                {
                    Title = "Resolved tickets",
                    Values = new ChartValues<double> { resolvedTickets.Count },
                    DataLabels = true,
                    FontSize = 22
                },
                new PieSeries
                {
                    Title = "Closed tickets",
                    Values = new ChartValues<double> { closedTickets.Count },
                    DataLabels = true,
                    FontSize = 22
                }
            };
            return pieSeries;
        }
    }
}
