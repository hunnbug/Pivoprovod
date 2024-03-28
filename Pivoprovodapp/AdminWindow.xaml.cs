using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public partial class AdminWindow : Window
    {
        private LoginsTableAdapter a        = new LoginsTableAdapter();
        private RolesTableAdapter roles     = new RolesTableAdapter();
        public AdminWindow()
        {
            InitializeComponent();
            DG.ItemsSource = a.GetDataBy3();
            RoleBox.ItemsSource = roles.GetData();
            RoleBox.DisplayMemberPath = "RoleName";
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            foreach(Window window in Application.Current.Windows)
            {
                if (window != this) { window.Close(); }
            }
            var main = new MainWindow();
            main.Show();
        }


        private void ClientsBtnClick(object sender, RoutedEventArgs e)
        {
            foreach(Window wind in Application.Current.Windows)
            {
                if(wind is ClientsWindow)
                {
                    MessageBox.Show("вы не можете открывать больше одного одинакового окна");
                    return;
                }
            }
            var window = new ClientsWindow(); window.Show();
        }

        private void ProductsClick(object sender, RoutedEventArgs e)
        {
            foreach (Window wind in Application.Current.Windows)
            {
                if (wind is ProductsWindow)
                {
                    MessageBox.Show("вы не можете открывать больше одного одинакового окна");
                    return;
                }
            }
            var window = new ProductsWindow(); window.Show();
        }

        private void PostavshikiClick(object sender, RoutedEventArgs e)
        {
            foreach (Window wind in Application.Current.Windows)
            {
                if (wind is PostavshikiWindow)
                {
                    MessageBox.Show("вы не можете открывать больше одного одинакового окна");
                    return;
                }
            }
            var window = new PostavshikiWindow(); window.Show();
        }

        private void SkladsClick(object sender, RoutedEventArgs e)
        {
            foreach (Window wind in Application.Current.Windows)
            {
                if (wind is SkladsWindow)
                {
                    MessageBox.Show("вы не можете открывать больше одного одинакового окна");
                    return;
                }
            }
            var window = new SkladsWindow(); window.Show();
        }

        private void LarkiClick(object sender, RoutedEventArgs e)
        {
            foreach (Window wind in Application.Current.Windows)
            {
                if (wind is LarkiWindow)
                {
                    MessageBox.Show("вы не можете открывать больше одного одинакового окна");
                    return;
                }
            }
            var window = new LarkiWindow(); window.Show();
        }
        private bool validation()
        {
            int i = 0;
            if (!Regex.IsMatch(NameBox.Text, @"[\dC#]") && NameBox.Text != "" && NameBox.Text.Length <= 50) { i += 1; }
            if (!Regex.IsMatch(SurnameBox.Text, @"[\dC#]") && SurnameBox.Text != "" && SurnameBox.Text.Length <= 50) { i += 1; }
            if (!Regex.IsMatch(LoginBox.Text, @"[\dC#]") && LoginBox.Text != "" && LoginBox.Text.Length <= 50) { i += 1; }
            if (PasswordBox.Text.Length <= 50) { i += 1; }
            if (RoleBox.Text != "") { i += 1; }

            if (i == 5)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Ошибка ввода, все строки крмое строки пароля не должны быть пустыми и должны содержать в себе только буквенные значения и не должны превышать 50 символов");
                NameBox.Text = ""; SurnameBox.Text = ""; LoginBox.Text = ""; PasswordBox.Text = ""; RoleBox.Text = "";
                return false;
            }
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            bool answ = validation();
            if (answ == true)
            {
                var all = a.GetData().Rows;
                for (int j = 0; j < all.Count; j++)
                {
                    if (LoginBox.Text == all[j][3].ToString())
                    {
                        MessageBox.Show("Нельзя создавать одинаковые логины");
                        return;
                    }
                }
                var id = (int)(RoleBox.SelectedItem as DataRowView).Row[0];
                a.InsertQuery(NameBox.Text, SurnameBox.Text, LoginBox.Text, PasswordBox.Text, id);
                DG.ItemsSource = a.GetDataBy3();
            }
            else { return; }
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            bool answ = validation();
            if (answ == true && DG.SelectedItem != null)
            {
                var selected = (int)(DG.SelectedItem as DataRowView).Row[0];
                var id = (int)(RoleBox.SelectedItem as DataRowView).Row[0];
                a.UpdateQuery(NameBox.Text, SurnameBox.Text, LoginBox.Text, PasswordBox.Text, id, selected);
                DG.ItemsSource = a.GetDataBy3();
            }
            else { return; }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                var id = (int)(DG.SelectedItem as DataRowView).Row[0];
                a.DeleteQuery(id);
                DG.ItemsSource = a.GetDataBy3();
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
                    LoginBox.Text = (string)(DG.SelectedItem as DataRowView).Row[3];
                    PasswordBox.Text = (string)(DG.SelectedItem as DataRowView).Row[4];
                }
            }
            catch (System.NullReferenceException) { return; }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DG.Columns[5].Visibility = Visibility.Collapsed;
        }
    }
}
