using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pivoprovodapp.DSTableAdapters;

namespace Pivoprovodapp
{
    public partial class SkladsWindow : Window
    {
        SkladiSotrudnikiTableAdapter a = new SkladiSotrudnikiTableAdapter();
        SkladsTableAdapter b = new SkladsTableAdapter();
        public SkladsWindow()
        {
            InitializeComponent();
            DG.ItemsSource = a.GetDataBy();
            SkladBox.ItemsSource = b.GetData();
            SkladBox.DisplayMemberPath = "SkladAdress";
        }

        private bool validation()
        {
            int i = 0;
            if (!Regex.IsMatch(NameBox.Text, @"[\dC#]") && NameBox.Text != "" && NameBox.Text.Length <= 50) { i += 1; }
            if (!Regex.IsMatch(SurnameBox.Text, @"[\dC#]") && SurnameBox.Text != "" && SurnameBox.Text.Length <= 50) { i += 1; }
            if (MiddlenameBox.Text.Length <= 50) { i += 1; }
            if (Regex.IsMatch(PhoneBox.Text, @"[\dC#]") && PhoneBox.Text != "" && PhoneBox.Text.Length <= 20) { i += 1; }
            if (SkladBox.Text != "") { i += 1; }

            var all = a.GetDataBy();
            for(int j = 0;j < all.Count; j++)
            {
                if (all[j][4].ToString() == PhoneBox.Text)
                {
                    MessageBox.Show("Ошибка, не может быть одинаковых номеров телефонов");
                    return false;
                }
            }
            if (i == 5) return true;
            else
            {
                MessageBox.Show("Ошибка ввода, все строки не должны быть пустыми и должны содержать в себе только буквенные значения и не должны превышать 50 символов");
                NameBox.Text = ""; SurnameBox.Text = ""; MiddlenameBox.Text = ""; SkladBox.Text = ""; PhoneBox.Text = "";
                return false;
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            bool answ = validation();
            if (answ == true)
            {
                var sklad = (SkladBox.SelectedItem as DataRowView).Row[0];
                a.InsertQuery(NameBox.Text, SurnameBox.Text, MiddlenameBox.Text, PhoneBox.Text, (int)sklad);
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
                var id = (int)(DG.SelectedItem as DataRowView).Row[0];
                a.UpdateQuery(NameBox.Text, SurnameBox.Text, MiddlenameBox.Text, PhoneBox.Text, id);
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
                    PhoneBox.Text = (string)(DG.SelectedItem as DataRowView).Row[4];
                    SkladBox.Text = (string)(DG.SelectedItem as DataRowView).Row[6];
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
