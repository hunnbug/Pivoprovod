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
    public partial class PostavshikiWindow : Window
    {
        PostavshikiTableAdapter a = new PostavshikiTableAdapter();
        public PostavshikiWindow()
        {
            InitializeComponent();
            DG.ItemsSource = a.GetData();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(NameBox.Text, @"[\dC#]") && NameBox.Text != "" && NameBox.Text.Length <= 60)
            {
                a.InsertQuery(NameBox.Text);
                DG.ItemsSource = a.GetData();
            }
            else {
                MessageBox.Show("Ошибка ввода, строка не должна быть пустой и превышать 60 символов, а еще строка должна содержать буквы и не содержать цифр");
                NameBox.Text = ""; 
            }
        }

        private void ChangeButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(NameBox.Text, @"[\dC#]") && NameBox.Text != "" && NameBox.Text.Length <= 60)
            {
                var id = (int)(DG.SelectedItem as DataRowView).Row[0];
                a.UpdateQuery(NameBox.Text, id);
                DG.ItemsSource = a.GetData();
            }
            else
            {
                MessageBox.Show("Ошибка ввода, строка не должна быть пустой и превышать 60 символов, а еще строка должна содержать буквы и не содержать цифр");
                NameBox.Text = "";
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if(DG.SelectedItem != null)
            {
                var id = (int)(DG.SelectedItem as DataRowView).Row[0];
                a.DeleteQuery(id);
                DG.ItemsSource = a.GetData();
            }
        }

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DG.SelectedItem != null)
                {
                    NameBox.Text = (string)(DG.SelectedItem as DataRowView).Row[1];
                }
            }
            catch(System.NullReferenceException) { return;  }
        }
    }
}
