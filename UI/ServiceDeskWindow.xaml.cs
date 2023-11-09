using System.Windows;
using System.Windows.Media;
using UI;

namespace TheGardenGroupProject;

public partial class ServiceDeskWindow : Window
{
    private ViewUsersPage ViewUsersPage { get; }
    private NewUserPage NewUserPage { get; }
    private AddTicketPage AddTicketPage { get; }
    private ViewTicketsPage ViewTicketsPage { get; }
    private CreateTicketPage CreateTicketPage { get; }
    
    public ServiceDeskWindow()
    {
        InitializeComponent();
        
        ViewUsersPage = new ViewUsersPage();
        NewUserPage = new NewUserPage();
        AddTicketPage = new AddTicketPage();
        ViewTicketsPage = new ViewTicketsPage();
        CreateTicketPage = new CreateTicketPage();
        
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
        ContentPage.Content = ViewTicketsPage;
    }
    private void CreateTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.Content = CreateTicketPage;
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        LoginUI loginWindow = new LoginUI();
        loginWindow.Show();
        this.Close();
    }
}