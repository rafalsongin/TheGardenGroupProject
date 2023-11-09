using System.Windows;

namespace TheGardenGroupProject;

public partial class sdtest : Window
{
    private ViewUsersPage ViewUsersPage { get; }
    private NewUserPage NewUserPage { get; }
    private AddTicketPage AddTicketPage { get; }
    private ViewTicketsPage ViewTicketsPage { get; }
    private CreateTicketPage CreateTicketPage { get; }

    public sdtest()
    {
        // ViewUsersPage = new ViewUsersPage();
        // NewUserPage = new NewUserPage();
        // AddTicketPage = new AddTicketPage();
        // RudTicketPage = new RUDTicketPage();
        InitializeComponent();
    }

    private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(ViewUsersPage);
    }

    private void AddUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(NewUserPage);
    }

    private void AddTicketButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(AddTicketPage);
    }



    private void ViewTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(ViewTicketsPage);
    }

    private void CreateTicketButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(CreateTicketPage);
    }
}