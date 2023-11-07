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
        ViewUsersPage = new ViewUsersPage();
        NewUserPage = new NewUserPage();
        AddTicketPage = new AddTicketPage();
        RudTicketPage = new RudTicketPage();
        
        InitializeComponent();
    }

    private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.Content = ViewUsersPage;
    }

    private void AddUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.Content = NewUserPage;
    }

    private void AddTicketButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.Content = AddTicketPage;
    }

    private void CrudTicketButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.Content = RudTicketPage;
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        LoginUI loginWindow = new LoginUI();
        loginWindow.Show();
        this.Close();
    }
}