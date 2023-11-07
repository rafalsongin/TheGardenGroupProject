using System.Windows;

namespace TheGardenGroupProject;

public partial class sdtest : Window
{
    private ViewUsersPage ViewUsersPage { get; }
    private NewUserPage NewUserPage { get; }
    private AddTicketPage AddTicketPage { get; }
    private RudTicketPage RudTicketPage { get; }

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

    private void CrudTicketButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(RudTicketPage);
    }
}