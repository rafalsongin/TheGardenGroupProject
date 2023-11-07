using System.Windows;
using Model;
using UI;

namespace TheGardenGroupProject;

public partial class CompanyEmployeeWindow : Window
{
    private User ActiveUser { get; }
    private DashboardPage DashboardPage { get; }
    private CreateTicketPage CreateTicketPage { get; }

    public CompanyEmployeeWindow()
    {
        CreateTicketPage = new CreateTicketPage();
        DashboardPage = new DashboardPage();
        InitializeComponent();
        // TODO: get the user from the login page
        ActiveUser = new User("test", "test", "test", "test", UserType.Manager, "test", "test", City.Amsterdam);
    }

    private void DashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(DashboardPage);
    }

    private void ViewTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        // ContentPage.NavigationService.Navigate(ViewTicketsPage);
    }

    private void CreateTicketButton_Click(object sender, RoutedEventArgs e)
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