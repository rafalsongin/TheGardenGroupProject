using System.Windows;
using Model;
using UI;

namespace TheGardenGroupProject;

public partial class CompanyEmployeeWindow : Window
{
    private DashboardPage DashboardPage { get; }
    private CreateTicketPage CreateTicketPage { get; }

    private AddTicketPage AddTicketPage { get; }

    private ViewTicketsForEmployeePage ViewTicketsForEmployeePage { get; }
    private User LoggedInUser { get; }

    public CompanyEmployeeWindow(User user)
    {
        InitializeComponent();
        
        CreateTicketPage = new CreateTicketPage();
        DashboardPage = new DashboardPage();
        AddTicketPage = new AddTicketPage();
        ViewTicketsForEmployeePage = new ViewTicketsForEmployeePage();
        
        LoggedInUser = user;
    }

    private void DashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(DashboardPage);
    }

    private void ViewTicketsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.NavigationService.Navigate(ViewTicketsForEmployeePage);
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