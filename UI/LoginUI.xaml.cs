using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Service;
using TheGardenGroupProject;

namespace UI
{
    public partial class LoginUI : Window
    {
        private readonly DatabaseService _databaseService;
        public LoginUI()
        {
            _databaseService = new DatabaseService();
            InitializeComponent();
            LoginUI_Load();
        }

        private void LoginUI_Load()
        {
            var dbList = _databaseService.Get_All_Databases();

            foreach (var db in dbList)
            {
                // ListBoxAllDbTest.Items.Add(db.name);
            }
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Hello");
            DashboardUI dashboardWindow = new DashboardUI();
            dashboardWindow.Show();
        }

        private void textBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
 
        }

        private void textBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void labelForgotLoginDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Forgot details");
        }
    }
}