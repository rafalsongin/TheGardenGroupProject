﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DAL;
using Model;
using Service;
using TheGardenGroupProject;

namespace UI
{
    public partial class LoginUI : Window
    {
        private readonly UserService _userService;
        
        public LoginUI()
        {
            _userService = new UserService();
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            VerifyingLoginService verifyingLoginService = new VerifyingLoginService();
            if (verifyingLoginService.IsCorrectPassword(TextBoxUsername.Text, PasswordBox.Password))
            {
                User user = _userService.GetUserByUsername(TextBoxUsername.Text);
                
                if (user.UserType == UserType.ServiceDeskEmployee)
                {
                    ServiceDeskWindow serviceDeskWindow = new ServiceDeskWindow();
                    serviceDeskWindow.Show();
                }
                else
                {
                    CompanyEmployeeWindow companyEmployeeWindow = new CompanyEmployeeWindow();
                    companyEmployeeWindow.Show();
                }
                
                this.Close();
            }
            else
            {
                LabelPasswordResetSuccess.Visibility = Visibility.Hidden;
                LabelIncorrectCredentials.Visibility = Visibility.Visible;
            }
        }

        private void labelForgotLoginDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridLoginPage.Visibility = Visibility.Hidden;
            
            PasswordResetWindow passwordResetWindow = new PasswordResetWindow();
            passwordResetWindow.Show();
            this.Close();
        }
    }
}