using System.Windows;
using Model;
using UI;

namespace TheGardenGroupProject;

// Rafal
public partial class ServiceDeskWindow : Window
{
    private NewUserPage NewUserPage { get; }
    private ViewTicketsPage ViewTicketsPage { get; }
    private CreateTicketPage CreateTicketPage { get; }

    private User LoggedInUser { get; }

    public ServiceDeskWindow(User user)
    {
        InitializeComponent();
        
        NewUserPage = new NewUserPage();
        CreateTicketPage = new CreateTicketPage();

        LoggedInUser = user;
    }

    private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
    {
        // to refresh the page when the user clicks on the button
        ContentPage.NavigationService.Navigate(new ViewUsersPage());
    }

    private void AddUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(NewUserPage);
    }

    private void ViewTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        // to refresh the page when the user clicks on the button
        ContentPage.NavigationService.Navigate(new ViewTicketsPage());
    }

    private void CreateTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(CreateTicketPage);
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        LoginUI loginWindow = new LoginUI();
        loginWindow.Show();
        this.Close();
    }
}