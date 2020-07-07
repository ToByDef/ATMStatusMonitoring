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
        CheckData check = new CheckData();

        public MainWindow(string login)
        {
            InitializeComponent();

            context.Users.Load();
            var user = context.Users.Local.FirstOrDefault(item => item.Login == login);
            if (user.Role != 1)
                ATMDataTab.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.ATMs.Load();
            context.ATMStatuses.Load();
            DataATM.ItemsSource = context.ATMs.Local.ToObservableCollection();
            DataATMStatus.ItemsSource = context.ATMStatuses.Local.ToObservableCollection();
        }

        #region ATM button click events 
        //TODO: Удалить некорректные NewNumber
        private void AddDataATM_Click(object sender, RoutedEventArgs e)
        {
            if (check.CheckIDATM(NameATM.Text) == 0)
            {
                if (string.IsNullOrEmpty(LastNameATM.Text))
                {
                    ATM atm = new ATM
                    {
                        Number = NameATM.Text,
                        SerialNumber = int.Parse(SerialNumberATM.Text),
                        IP = ipATM.Text,
                        Mask = MaskATM.Text,
                        Gateway = GatewayATM.Text,
                        Address = AddressATM.Text
                    };
                    context.ATMs.Add(atm);
                }
                else
                {
                    var atmForUpdate = context.ATMs.FirstOrDefault(item => item.Id == check.CheckIDATM(LastNameATM.Text));
                    if (atmForUpdate != null)
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
                        atmForUpdate.NewNumber = LastNameATM.Text;
                    }
                    else
                    {
                        MessageBox.Show("Invalid entry in Last Name", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                context.SaveChanges();
                DataATM.Items.Refresh();
            }
            else
            {
                MessageBox.Show("There is an entry with the same Name or Last Name", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateDataATM_Click(object sender, RoutedEventArgs e)
        {
            if (check.CheckIDATM(LastNameATM.Text) == 0 && !string.IsNullOrEmpty(LastNameATM.Text))
            {
                MessageBox.Show("Incorrect entry in Last Number", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
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

        #region Fill textbox
        private void DataATM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataATM.SelectedItem != null)
            {
                var rowSelected = (ATM)DataATM.SelectedItem;
                NameATM.Text = rowSelected.Number;
                LastNameATM.Text = rowSelected.LastNumber;
                SerialNumberATM.Text = rowSelected.SerialNumber.ToString();
                ipATM.Text = rowSelected.IP;
                MaskATM.Text = rowSelected.Mask;
                GatewayATM.Text = rowSelected.Gateway;
                AddressATM.Text = rowSelected.Address;
            }
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