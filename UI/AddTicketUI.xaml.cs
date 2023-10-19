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
            EmptyErrorLabels();
            if (CheckIfFieldsFilledIn())
            {
                CreateTicketConcept();
            }
        }

        private void CreateTicketConcept()
        {
            string title = titleTextBox.Text;
            Priority priority = (Priority)priorityComboBox.SelectedItem;
            IncidentType incidentType = (IncidentType)typeComboBox.SelectedItem;
            string description = descriptionTextBox.Text;

            Ticket ticket = new Ticket();
            ticket.createConceptTicket(title, priority, description, incidentType, user);

            TicketService service = new TicketService();
        }

        private bool CheckIfFieldsFilledIn()
        {
            bool filledIn = true;
            if (!CheckIfTitleFilled())
            {
                filledIn = false;
            }
            if (!CheckIfPrioritySelected())
            {
                filledIn = false;
            }
            if (!CheckIfIncidentTypeSelected())
            {
                filledIn = false;
            }
            if (!CheckIfDescriptionFilled())
            {
                filledIn = false;
            }

            return filledIn;
        }

        private bool CheckIfPrioritySelected()
        {
            if (priorityComboBox.SelectedIndex == -1)
            {
                priorityError.Content = "Select a priority";
                return false;
            }
            else
            {
                return true;
            }

        }

        private bool CheckIfTitleFilled()
        {
            if (titleTextBox.Text.Length == 0)
            {
                titleError.Content = "Enter a title";
                return false;
            }
            else { return true; }
        }

        private bool CheckIfIncidentTypeSelected()
        {
            if (typeComboBox.SelectedIndex == -1)
            {
                incidentTypeError.Content = "Select an incident type";
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckIfDescriptionFilled()
        {
            if (descriptionTextBox.Text.Length == 0)
            {
                descriptionError.Content = "Give a description";
                return false;
            }
            else { return true; }
        }

        private void LoadComboBoxes()
        {
            typeComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
        }

        private void EmptyErrorLabels()
        {
            titleError.Content = "";
            priorityError.Content = "";
            incidentTypeError.Content = "";
            descriptionError.Content = "";
        }
    }
}
