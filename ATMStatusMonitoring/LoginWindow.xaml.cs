using ATMStatusMonitoring.Service;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Windows;

namespace ATMStatusMonitoring
{
    public partial class LoginWindow : Window
    {
        MyDbContext context = new MyDbContext();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private bool CheckUser()
        {
            foreach (var item in context.Users)
            {
                if (item.Login == LoginTextBox.Text && item.Password == PasswordTextBox.Password)
                    return true;
            }
            return false;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUser())
            {
                MainWindow main = new MainWindow(LoginTextBox.Text, PasswordTextBox.Password);
                main.Show();
                this.Close();
            }
            else
                MessageBox.Show("Login / password is incorrectly entered or such user does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
        }
    }
}