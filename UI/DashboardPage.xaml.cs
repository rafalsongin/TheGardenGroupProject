using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using Model;
using Service;

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            InitializeComponent();
            
            DataContext = this;
            
            // TODO: check why it's not working (something with the Model.Ticket)
            try
            {
                //DisplayPieChart();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
