using Model;
using MongoDB.Driver;
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
using System.Windows.Shapes;

namespace TheGardenGroupProject
{
    /// <summary>
    /// Interaction logic for RUDTicket.xaml
    /// </summary>
    public partial class RUDTicket : Window
    {
        public RUDTicket()
        {
            InitializeComponent();
        }

        private void txtFilterByEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           string serachCriteria=txtFilterByEmail.Text;// i will change it filter by id or email
            TicketService ticketService = new TicketService();

            // Create a filter based on the search criteria (e.g., by subject)
            FilterDefinition<Ticket> filter = Builders<Ticket>.Filter.Eq("Subject", serachCriteria);

            // Call the service method to get the filtered tickets
            Ticket matchingTicket = ticketService.GetTicketByFilter(filter);

            // Bind the matching tickets to the DataGrid
            dataGrid.ItemsSource = new[]{ matchingTicket };


        }
    }
}
