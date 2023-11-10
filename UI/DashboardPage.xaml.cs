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
    // Rafal
    public partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            InitializeComponent();

            DataContext = this;

            try
            {
                DisplayPieChart();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void DisplayPieChart()
        {
            PieChart pieChart = GetPieChart();

            // create PieChart in the middle of the screen
            pieChart.Margin = new Thickness(80, 40, 80, 40);

            ChartContainer.Children.Add(pieChart);
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
                LegendLocation = LegendLocation.Bottom,
                FontSize = 18,
                Width = 500,
                Height = 500,
                Hoverable = true,
                DisableAnimations = true,
                DataTooltip = null,
            };

            return pieChart;
        }

        private static SeriesCollection GetPieSeriesCollection(List<Ticket> openedTickets, List<Ticket> resolvedTickets,
            List<Ticket> closedTickets)
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