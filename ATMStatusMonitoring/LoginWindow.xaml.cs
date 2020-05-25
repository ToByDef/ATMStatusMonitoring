using System.Windows;

namespace ATMStatusMonitoring
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserAccess.GetUser(LoginTextBox.Text, PasswordTextBox.Password) != null)
            {
                MainWindow main = new MainWindow(LoginTextBox.Text, PasswordTextBox.Password);
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login / password is incorrectly entered or such user does not exist", "Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}