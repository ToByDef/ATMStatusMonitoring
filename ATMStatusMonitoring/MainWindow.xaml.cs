using ATMStatusMonitoring.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ATMStatusMonitoring
{
    public partial class MainWindow : Window
    {
        MyDbContext context = new MyDbContext();

        public MainWindow(string login, string pass)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.ATMs.Load();
            DataATM.ItemsSource = context.ATMs.Local.ToObservableCollection();
        }

        #region ATM button click events 
        private void AddDataATM_Click(object sender, RoutedEventArgs e)
        {
            ATM atm = new ATM
            {
                Number = NameATM.Text,
                LastNumber = LastNameATM.Text,
                SerialNumber = int.Parse(SerialNumberATM.Text),
                IP = ipATM.Text,
                Mask = MaskATM.Text,
                Gateway = GatewayATM.Text,
                Address = AddressATM.Text
            };
            context.ATMs.Add(atm);
            context.SaveChanges();
        }

        private void UpdateDataATM_Click(object sender, RoutedEventArgs e)
        {
            var atm = context.ATMs.FirstOrDefault(item => item.Number == NameATM.Text);
            if (atm != null)
            {
                atm.LastNumber = LastNameATM.Text;
                atm.SerialNumber = int.Parse(SerialNumberATM.Text);
                atm.IP = ipATM.Text;
                atm.Mask = MaskATM.Text;
                atm.Gateway = GatewayATM.Text;
                atm.Address = AddressATM.Text;
                context.SaveChanges();
            }
            DataATM.Items.Refresh();
        }

        private void DeleteDataATM_Click(object sender, RoutedEventArgs e)
        {
            var atm = context.ATMs.FirstOrDefault(item => item.Number == NameATM.Text);
            if (atm != null)
            {
                context.ATMs.Remove(atm);
                context.SaveChanges();
            }
            DataATM.Items.Refresh();
        }
        #endregion

        private void textChangedEventATM(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox[] textboxesATM = { NameATM, SerialNumberATM, ipATM, GatewayATM, MaskATM, AddressATM };
            AddDataATM.IsEnabled = textboxesATM.All(tb => !string.IsNullOrEmpty(tb.Text));
            UpdateDataATM.IsEnabled = textboxesATM.All(tb => !string.IsNullOrEmpty(tb.Text));
            DeleteDataATM.IsEnabled = !string.IsNullOrEmpty(NameATM.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
        }
    }
}