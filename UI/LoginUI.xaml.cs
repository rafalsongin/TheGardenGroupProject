using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Service;

namespace UI
{
    public partial class LoginUI : Window
    {
        private readonly Databases _databases;
        public LoginUI()
        {
            _databases = new Databases();
            InitializeComponent();
            LoginUI_Load();
        }

        private void LoginUI_Load()
        {
            var dbList = _databases.Get_All_Databases();

            foreach (var db in dbList)
            {
                // ListBoxAllDbTest.Items.Add(db.name);
            }
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");

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