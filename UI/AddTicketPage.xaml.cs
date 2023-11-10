using Model;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace TheGardenGroupProject
{
    // Made by Kim
    public partial class AddTicketPage : Page
    {
        private User user;

        public AddTicketPage(User user)
        {
            InitializeComponent();            
            LoadComboBoxes();
            this.user = user;
        }

        private void LoadComboBoxes() // filling one combo box with all the values of the enum 'IncidentType' and the other combo box with all the values of the enum 'Priority'
        {
            typeComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
        }

        private void CreateTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmptyErrorLabels();
                if (CheckIfFieldsFilledIn()) // if all info needed is given we can go on to create the ticket
                {
                    CreateTicketConcept(); // create and send the ticket to the database
                    ClearTicket(); //clearing all fields and combo boxes
                    MessageBox.Show("Ticket Successfully added!"); // showing confirmation that the ticket is successfully added to the database
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void EmptyErrorLabels()
        {
            titleError.Content = "";
            priorityError.Content = "";
            incidentTypeError.Content = "";
            descriptionError.Content = "";
        }

        private bool CheckIfFieldsFilledIn()
        {
            bool filledIn = true;
            if (titleTextBox.Text.Length == 0) // if no title is given give error and make sure no ticket is created
            {
                titleError.Content = "Enter a title";
                filledIn = false;
            }
            if (priorityComboBox.SelectedIndex == -1) // if no priority is selected give error and make sure no ticket is created
            {
                priorityError.Content = "Select a priority";
                filledIn = false;
            }
            if (typeComboBox.SelectedIndex == -1) // if no type is selected give error and make sure no ticket is created
            {
                incidentTypeError.Content = "Select an incident type";
                filledIn = false;
            }
            if (descriptionTextBox.Text.Length == 0) // if no description is given give error and make sure no ticket is created
            {
                descriptionError.Content = "Give a description";
                filledIn = false;
            }

            return filledIn;
        }

        private void CreateTicketConcept()
        {
            TicketService service = new();
            Ticket ticket = new(titleTextBox.Text, (Priority)priorityComboBox.SelectedItem, descriptionTextBox.Text, (IncidentType)typeComboBox.SelectedItem, user);
            
            service.CreateTicket(ticket);
        }

        private void ClearTicket()
        {
            titleTextBox.Text = "";
            priorityComboBox.SelectedIndex = -1;
            typeComboBox.SelectedIndex = -1;
            descriptionTextBox.Text = "";
        }               
    }
}