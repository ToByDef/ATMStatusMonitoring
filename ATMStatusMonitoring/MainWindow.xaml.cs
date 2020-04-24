using System;
using System.Windows;
using System.Collections.Generic;

namespace ATMStatusMonitoring
{
    public partial class MainWindow : Window
    {
        CheckID check = new CheckID();
        List<ATM> atm = new List<ATM>();
        List<ATMStatus> atmStatus = new List<ATMStatus>();
        DataAccess da = new DataAccess();
        UserAccess userAccess = new UserAccess();
        User user = new User();

        public MainWindow(string login, string pass)
        {
            InitializeComponent();
            user = userAccess.GetUser(login, pass);
        }

        private void UpdateDataGridATM()   //update DataGrid
        {
            atm = da.GetATM();
            DataATM.ItemsSource = atm;
        }

        private void UpdateDataGridATMStatus()  //update DataGridStatus
        {
            atmStatus = da.GetATMStatus();
            DataATMStatus.ItemsSource = atmStatus;
        }

        private void AddDataATM_Click(object sender, RoutedEventArgs e)
        {
            if (check.CheckIDATM(NameATM.Text) == 0 && check.CheckLastNumber(LastNameATM.Text) == 0)
            {
                if (LastNameATM.Text == "")
                {
                    da.AddATM(NameATM.Text, Convert.ToInt32(SerialNumberATM.Text), ipATM.Text, MaskATM.Text, GatewayATM.Text, AddressATM.Text);   //add new record to ATM database
                }
                else
                {
                    if (check.CheckIDATM(LastNameATM.Text) != 0)
                    {
                        da.AddATM(NameATM.Text, LastNameATM.Text, Convert.ToInt32(SerialNumberATM.Text), ipATM.Text, MaskATM.Text, GatewayATM.Text, AddressATM.Text);   //add new record to ATM database with LastName
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

        private void UpdateDataATM_Click(object sender, RoutedEventArgs e) //update record ATM database
        {
            if (check.CheckIDATM(LastNameATM.Text) == 0 && LastNameATM.Text != "")
                MessageBox.Show("Incorrect entry in Last Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                da.UpdateATM(NameATM.Text, LastNameATM.Text, Convert.ToInt32(SerialNumberATM.Text), ipATM.Text, MaskATM.Text, GatewayATM.Text, AddressATM.Text);
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
    }
}