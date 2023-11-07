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
        ContentPage.Content = new Page1();
    }

    private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ContentPage.Content = new Page2();
    }
}