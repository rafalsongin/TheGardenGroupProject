using System.Windows;
using Model;
using UI;

namespace TheGardenGroupProject;

public partial class CompanyEmployeeWindow : Window
{
    private DashboardPage DashboardPage { get; }
    private CreateTicketPage CreateTicketPage { get; }

    private AddTicketPage AddTicketPage { get; }

    public CompanyEmployeeWindow()
    {
        InitializeComponent();
        
        CreateTicketPage = new CreateTicketPage();
        DashboardPage = new DashboardPage();
        AddTicketPage = new AddTicketPage();
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
        ContentPage.NavigationService.Navigate(AddTicketPage);
    }
    
    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        LoginUI loginWindow = new LoginUI();
        loginWindow.Show();
        this.Close();
    }
}