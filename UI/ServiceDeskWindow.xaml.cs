using System.Windows;
using Model;
using UI;

namespace TheGardenGroupProject;

// Rafal
public partial class ServiceDeskWindow : Window
{
    private ViewUsersPage ViewUsersPage { get; }
    private NewUserPage NewUserPage { get; }
    private ViewTicketsPage ViewTicketsPage { get; }
    private CreateTicketPage CreateTicketPage { get; }

    private User LoggedInUser { get; }

    public ServiceDeskWindow(User user)
    {
        InitializeComponent();

        ViewUsersPage = new ViewUsersPage();
        NewUserPage = new NewUserPage();
        ViewTicketsPage = new ViewTicketsPage();
        CreateTicketPage = new CreateTicketPage();

        LoggedInUser = user;
    }

    private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(ViewUsersPage);
    }

    private void AddUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(NewUserPage);
    }

    private void ViewTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(ViewTicketsPage);
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