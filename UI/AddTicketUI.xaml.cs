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
    /// Interaction logic for AddTicketUI.xaml
    /// </summary>
    public partial class AddTicketUI : Window
    {
        private User user;

        public AddTicketUI()
        {
            InitializeComponent();
            user = new User("Kim", "kim123", "Kim", "van Schagen", UserType.Manager, "smt", "000", City.Amsterdam);
            LoadComboBoxes();
            
        }

        private void CreateTicket_Click(object sender, RoutedEventArgs e)
        {
            string title = titleTextBox.Text;
            Priority priority = (Priority)priorityComboBox.SelectedItem;
            IncidentType incidentType = (IncidentType)typeComboBox.SelectedItem;
            string description = descriptionTextBox.Text;

            Ticket ticket = new Ticket();
            ticket.createConceptTicket(title, priority, description, incidentType, user);

            TicketService service = new TicketService();
        }

        private void LoadComboBoxes()
        {
            typeComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            priorityComboBox.ItemsSource = Enum.GetValues(typeof (Priority));
        }
    }
}
