using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System.Collections;

namespace ATMStatusMonitoring
{
    public partial class MainWindow : Window
    {
        CheckID check = new CheckID();
        DataAccess da = new DataAccess();
        User user = new User();
        MyDbContext context;


        public MainWindow(string login, string pass)
        {
            InitializeComponent();

            context = new MyDbContext();
            context.ATMs.Load();
            context.ATMStatuses.Load();
            DataATM.ItemsSource = context.ATMs.Local.ToBindingList();
            //TODO: Переделать по base-first с помощью ADO.NET
                //= context.ATMStatuses.Include("ATM").ToList();
            //DataATMStatus.ItemsSource = test;

            user = UserAccess.GetUser(login, pass);
        }

        private void UpdateDataGridATM()
        {
            //DataATM.ItemsSource = da.GetATM();
        }

        private void UpdateDataGridATMStatus()
        {
            //DataATMStatus.ItemsSource = da.GetATMStatus();
        }

        private void AddDataATM_Click(object sender, RoutedEventArgs e)
        {
            if (check.CheckIDATM(NameATM.Text) == 0 && check.CheckLastNumber(LastNameATM.Text) == 0)
            {
                if (string.IsNullOrEmpty(LastNameATM.Text))
                {
                    da.AddATM(NameATM.Text, int.Parse(SerialNumberATM.Text), ipATM.Text, MaskATM.Text, GatewayATM.Text, AddressATM.Text);   //add new record to ATM database
                }
                else
                {
                    if (check.CheckIDATM(LastNameATM.Text) != 0)
                    {
                        da.AddATM(NameATM.Text, LastNameATM.Text, int.Parse(SerialNumberATM.Text), ipATM.Text, MaskATM.Text, GatewayATM.Text, AddressATM.Text);   //add new record to ATM database with LastName
                    }
                    else
                        MessageBox.Show("Invalid entry in Last Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("There is an entry with the same Name or Last Name", "Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
            UpdateDataGridATM();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(user.Role != 1)
            {
                ATMDataTab.Visibility = Visibility.Collapsed;
            }
            UpdateDataGridATM();
            UpdateDataGridATMStatus();
        }

        private void UpdateDataATM_Click(object sender, RoutedEventArgs e)
        {
            if (check.CheckIDATM(LastNameATM.Text) == 0 && !string.IsNullOrEmpty(LastNameATM.Text))
                MessageBox.Show("Incorrect entry in Last Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                da.UpdateATM(NameATM.Text, LastNameATM.Text, int.Parse(SerialNumberATM.Text), ipATM.Text, MaskATM.Text, GatewayATM.Text, AddressATM.Text);
            UpdateDataGridATM();
        }

        private void DeleteDataATM_Click(object sender, RoutedEventArgs e)
        {
            da.DeleteATM(NameATM.Text);
            UpdateDataGridATM();
            UpdateDataGridATMStatus();
        }

        private void UpdateATMStatus_Click(object sender, RoutedEventArgs e)
        {
            if (check.CheckIDATM(NameATMStatus.Text) != 0 && DateChangeStatus.SelectedDate != null)
                da.UpdateATMStatus(NameATMStatus.Text, StatusATM.Text, (DateTime)DateChangeStatus.SelectedDate, user.Name, CommentStatus.Text);
            else
                MessageBox.Show("There is no ATM, no closing date is indicated or the record is incorrect", "Error",MessageBoxButton.OK,MessageBoxImage.Error);
            UpdateDataGridATMStatus();
        }

        private void DeleteATMStatus_Click(object sender, RoutedEventArgs e)
        {
            da.DeleteATMStatus(NameATMStatus.Text);
            UpdateDataGridATMStatus();
        }

        #region Double click event
        private void DataATM_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(DataATM.SelectedItem != null)
            {
                dynamic rowSelected = DataATM.SelectedItem;
                NameATM.Text = rowSelected.Number;
                LastNameATM.Text = rowSelected.LastNumber;
                SerialNumberATM.Text = rowSelected.SerialNumber.ToString();
                ipATM.Text = rowSelected.IP;
                MaskATM.Text = rowSelected.Mask;
                GatewayATM.Text = rowSelected.Gateway;
                AddressATM.Text = rowSelected.Address;
            }
        }

        private void DataATMStatus_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(DataATMStatus.SelectedItem != null)
            {
                dynamic rowSelected = DataATMStatus.SelectedItem;
                NameATMStatus.Text = rowSelected.Number;
                DateChangeStatus.SelectedDate = Convert.ToDateTime(rowSelected.Date);
                CommentStatus.Text = rowSelected.Comment;
            }
        }
        #endregion

        private void textChangedEventATM(object sender, TextChangedEventArgs e)
        {
            TextBox[] textboxesATM = { NameATM, SerialNumberATM, ipATM, GatewayATM, MaskATM, AddressATM };
            AddDataATM.IsEnabled = textboxesATM.All(tb => !string.IsNullOrEmpty(tb.Text));
            UpdateDataATM.IsEnabled = textboxesATM.All(tb => !string.IsNullOrEmpty(tb.Text));
            DeleteDataATM.IsEnabled = !string.IsNullOrEmpty(NameATM.Text);
        }

        private void searchATMButton_Click(object sender, RoutedEventArgs e)
        {
            DataATM.ItemsSource = DataSearch.SearchATM(searchATMText.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
        }
    }
}