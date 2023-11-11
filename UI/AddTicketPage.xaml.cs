using System;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace TheGardenGroupProject
{
    // Made by Kim
    public partial class AddTicketPage : Page
    {
        private readonly User _user;

        public AddTicketPage(User user)
        {
            InitializeComponent();
            LoadComboBoxes();
            EmptyErrorLabels();
            ClearTicket();
            _user = user;
        }

        private void
            LoadComboBoxes() // filling one combo box with all the values of the enum 'IncidentType' and the other combo box with all the values of the enum 'Priority'
        {
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(IncidentType));
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
        }

        private void EmptyErrorLabels()
        {
            TitleError.Content = "";
            PriorityError.Content = "";
            IncidentTypeError.Content = "";
            DescriptionError.Content = "";
        }

        private void ClearTicket()
        {
            TitleTextBox.Text = "";
            PriorityComboBox.SelectedIndex = -1;
            TypeComboBox.SelectedIndex = -1;
            DescriptionTextBox.Text = "";
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
                    MessageBox.Show(
                        "Ticket Successfully added!"); // showing confirmation that the ticket is successfully added to the database
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearTicket_Click(object sender, RoutedEventArgs e)
        {
            EmptyErrorLabels();
            ClearTicket();
        }

        private bool CheckIfFieldsFilledIn()
        {
            bool filledIn = true;
            if (TitleTextBox.Text.Length == 0) // if no title is given give error and make sure no ticket is created
            {
                TitleError.Content = "Enter a title";
                filledIn = false;
            }

            if (PriorityComboBox.SelectedIndex ==
                -1) // if no priority is selected give error and make sure no ticket is created
            {
                PriorityError.Content = "Select a priority";
                filledIn = false;
            }

            if (TypeComboBox.SelectedIndex ==
                -1) // if no type is selected give error and make sure no ticket is created
            {
                IncidentTypeError.Content = "Select an incident type";
                filledIn = false;
            }

            if (DescriptionTextBox.Text.Length ==
                0) // if no description is given give error and make sure no ticket is created
            {
                DescriptionError.Content = "Give a description";
                filledIn = false;
            }

            return filledIn;
        }

        private void CreateTicketConcept()
        {
            TicketService service = new();
            Ticket ticket = new(TitleTextBox.Text, (Priority)PriorityComboBox.SelectedItem, DescriptionTextBox.Text,
                (IncidentType)TypeComboBox.SelectedItem, _user);

            service.CreateTicket(ticket);
        }
    }
}