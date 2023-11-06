using System;
using Model;
using Service;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Windows;
using LiveCharts;
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
            //DisplayPieChart();
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
            TicketStatusData = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Open",
                    Values = new ChartValues<double> { 5 },
                    DataLabels = true,
                    LabelPoint = TicketStatusLabelPoint,
                    Fill = System.Windows.Media.Brushes.Green
                },
                new PieSeries
                {
                    Title = "Closed",
                    Values = new ChartValues<double> { 3 },
                    DataLabels = true,
                    LabelPoint = TicketStatusLabelPoint,
                    Fill = System.Windows.Media.Brushes.Red
                },
                new PieSeries
                {
                    Title = "In Progress",
                    Values = new ChartValues<double> { 2 },
                    DataLabels = true,
                    LabelPoint = TicketStatusLabelPoint,
                    Fill = System.Windows.Media.Brushes.Yellow
                }
            };
            
            TicketStatusLabelPoint = chartPoint => $"{chartPoint.Y} ({chartPoint.Participation})";
            
            DataContext = this;
        }
    }
}
