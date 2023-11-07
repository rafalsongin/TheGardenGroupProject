using System.Windows;
using System.Windows.Controls;

namespace TheGardenGroupProject;

public partial class MainWindowCompanyEmployee : Window
{
    public MainWindowCompanyEmployee()
    {
        InitializeComponent();
    }

    private void DashboardButton_Click(object sender, RoutedEventArgs e)
    {
        // ContentPage.Content = new Page1();
        // ContentPage.NavigationService.Navigate(new Page1());
        ContentPage.Content = new Page1();
        // ContentPage.NavigationService.Source = null;
        // ContentPage.NavigationService.RemoveBackEntry();
    }

    private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
    {
        // ContentPage.Content = new Page2();
        // ContentPage.NavigationService.Navigate(new Page2());
        ContentPage.Content = new Page2();
        // ContentPage.NavigationService.Source = null;
        // ContentPage.NavigationService.RemoveBackEntry();
    }
}