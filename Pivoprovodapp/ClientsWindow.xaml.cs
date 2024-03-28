using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pivoprovodapp.DSTableAdapters;

namespace Pivoprovodapp
{
    public partial class ClientsWindow : Window
    {
        ClientsTableAdapter a = new ClientsTableAdapter();
        ClientStatusTableAdapter s = new ClientStatusTableAdapter();
        RolesTableAdapter role = new RolesTableAdapter();
        public ClientsWindow()
        {
            InitializeComponent();
            DG.ItemsSource = a.GetDataBy();
            StatusBox.ItemsSource = s.GetData();
            StatusBox.DisplayMemberPath = "StatusName";
        }

        private bool validation()
        {
            int i = 0;
            if (!Regex.IsMatch(NameBox.Text, @"[\dC#]") && NameBox.Text != "" && NameBox.Text.Length <= 50) { i += 1; }
            if (!Regex.IsMatch(SurnameBox.Text, @"[\dC#]") && SurnameBox.Text != "" && SurnameBox.Text.Length <= 50) { i += 1; }
            if (MiddlenameBox.Text.Length <= 50) { i += 1; }
            if (StatusBox.Text != "") { i += 1; }

            if (i == 4) return true;
            else
            {
                MessageBox.Show("Ошибка ввода, все строки не должны быть пустыми и должны содержать в себе только буквенные значения и не должны превышать 50 символов");
                NameBox.Text = ""; SurnameBox.Text = ""; MiddlenameBox.Text = ""; StatusBox.Text = "";
                return false;
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            bool answ = validation();
            if (answ == true)
            {
                var status = (StatusBox.SelectedItem as DataRowView).Row[0];
                a.InsertQuery(NameBox.Text, SurnameBox.Text, MiddlenameBox.Text, (int)status);
                DG.ItemsSource = a.GetDataBy();
            }
            else { return; }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                var id = (int)(DG.SelectedItem as DataRowView).Row[0];
                a.DeleteQuery(id);
                DG.ItemsSource = a.GetDataBy();
            }
            else { return; }
        }

        private void ChangeButtonClick(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                var status = (StatusBox.SelectedItem as DataRowView).Row[0];
                var id = (int)(DG.SelectedItem as DataRowView).Row[0];
                a.UpdateQuery(NameBox.Text, SurnameBox.Text, MiddlenameBox.Text, (int)status, id);
                DG.ItemsSource = a.GetDataBy();
            }
            else { return; }
        }

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DG.SelectedItem != null)
                {
                    NameBox.Text = (string)(DG.SelectedItem as DataRowView).Row[1];
                    SurnameBox.Text = (string)(DG.SelectedItem as DataRowView).Row[2];
                    MiddlenameBox.Text = (string)(DG.SelectedItem as DataRowView).Row[3];
                }
            }
            catch (System.NullReferenceException) { return; }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DG.Columns[4].Visibility = Visibility.Collapsed;
            DG.Columns[5].Visibility = Visibility.Collapsed;
        }
    }
}
