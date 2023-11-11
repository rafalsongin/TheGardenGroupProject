using System.Windows;
using Model;
using UI;

namespace TheGardenGroupProject;

// Rafal
public partial class CompanyEmployeeWindow : Window
{
    private AddTicketPage AddTicketPage { get; }
    private User LoggedInUser { get; }

    public CompanyEmployeeWindow(User user)
    {
        InitializeComponent();
        LoggedInUser = user;
        
        AddTicketPage = new AddTicketPage(LoggedInUser);
    }

    private void DashboardButton_Click(object sender, RoutedEventArgs e)
    {
        // to refresh the page when the user clicks on the button
        ContentPage.NavigationService.Navigate(new DashboardPage());
    }

    private void ViewTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        // to refresh the page when the user clicks on the button
        ContentPage.NavigationService.Navigate(new ViewTicketsForEmployeePage(LoggedInUser));
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