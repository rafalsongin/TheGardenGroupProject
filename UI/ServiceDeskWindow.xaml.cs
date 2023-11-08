using System.Windows;
using System.Windows.Media;
using UI;

namespace TheGardenGroupProject;

public partial class ServiceDeskWindow : Window
{
    private ViewUsersPage ViewUsersPage { get; }
    private NewUserPage NewUserPage { get; }
    private AddTicketPage AddTicketPage { get; }
    private RudTicketPage RudTicketPage { get; }
    
    public ServiceDeskWindow()
    {
        InitializeComponent();
        
        ViewUsersPage = new ViewUsersPage();
        NewUserPage = new NewUserPage();
        AddTicketPage = new AddTicketPage();
        RudTicketPage = new RudTicketPage();
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

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        LoginUI loginWindow = new LoginUI();
        loginWindow.Show();
        this.Close();
    }
}