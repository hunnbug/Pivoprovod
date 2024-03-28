using System;
using System.Collections.Generic;
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
    public partial class LarkiWindow : Window
    {
        LarekProductsTableAdapter a = new LarekProductsTableAdapter();
        public LarkiWindow()
        {
            InitializeComponent();
            DG.ItemsSource = a.GetData();
            LarekBox.ItemsSource = a.GetData();
            LarekBox.DisplayMemberPath = "LarekAdress";
            ProductsBox.ItemsSource = a.GetData();
            ProductsBox.DisplayMemberPath = "ProductName";
        }

        private bool validation()
        {
            int i = 0;
            if (Regex.IsMatch(NameBox.Text, @"[\dC#]") && NameBox.Text != "") { i += 1; }
            if( LarekBox.Text != "") {  i += 1; }
            if( ProductsBox.Text != "") {  i += 1; }

            if (i == 3) return true;
            else
            {
                MessageBox.Show("все поля должнв быть заполнены");
                NameBox.Text = ""; LarekBox.Text = ""; ProductsBox.Text = "";
                return false;
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            bool answ = validation();
            if (answ == true)
            {
                var larek = (LarekBox.SelectedItem as DataRowView).Row[0];
                var product = (ProductsBox.SelectedItem as DataRowView).Row[0];
                a.InsertQuery((int)larek, (int)product, Convert.ToInt32(NameBox.Text));
                DG.ItemsSource = a.GetData();
            }
            else { return; }
        }

        private void ChangeButtonClick(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                var id = (DG.SelectedItem as DataRowView).Row[0];
                var larek = (LarekBox.SelectedItem as DataRowView).Row[0];
                var product = (ProductsBox.SelectedItem as DataRowView).Row[0];
                a.UpdateQuery(Convert.ToInt32(NameBox.Text), larek, product, (int)id);
                DG.ItemsSource = a.GetData();
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if(DG.SelectedItem != null)
            {
                var id = (DG.SelectedItem as DataRowView).Row[0];
                a.DeleteQuery((int)id);
                DG.ItemsSource = a.GetData();
            }
        }

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DG.SelectedItem != null)
                {
                    NameBox.Text = Convert.ToString((DG.SelectedItem as DataRowView).Row[1]);
                    LarekBox.Text = (string)(DG.SelectedItem as DataRowView).Row[2];
                    ProductsBox.Text = (string)(DG.SelectedItem as DataRowView).Row[3];
                }
            }
            catch (System.NullReferenceException) { return; }
        }
    }
}
