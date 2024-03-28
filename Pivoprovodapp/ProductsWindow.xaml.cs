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
    
    public partial class ProductsWindow : Window
    {
        FinalProductsTableAdapter a = new FinalProductsTableAdapter();
        ProductsTableAdapter b = new ProductsTableAdapter();
        SkladsTableAdapter sklads = new SkladsTableAdapter();
        PostavshikiTableAdapter postavshiki = new PostavshikiTableAdapter();
        public ProductsWindow()
        {
            InitializeComponent();
            DG.ItemsSource = a.GetDataBy();
            ProductBox.ItemsSource = b.GetData();
            ProductBox.DisplayMemberPath = "ProductName";
            SkladBox.ItemsSource = sklads.GetData();
            SkladBox.DisplayMemberPath = "SkladAdress";
            PostavshikBox.ItemsSource = postavshiki.GetData();
            PostavshikBox.DisplayMemberPath = "PostavshikName";
        }
        private bool validation()
        {
            int i = 0;
            if (Regex.IsMatch(PriceBox.Text, @"^\d+$") && PriceBox.Text != "") { i += 1; }
            if (Regex.IsMatch(AmountBox.Text, @"^\d+$") && AmountBox.Text != "") { i += 1; }
            if (ProductBox.Text != "") { i += 1; }
            if (SkladBox.Text != "") { i += 1; }
            if (PostavshikBox.Text != "") { i += 1; }

            if (i == 5) return true;
            else
            {
                MessageBox.Show("Ошибка ввода, все строки не должны быть пустыми и должны содержать в себе только циферные значения");
                PriceBox.Text = ""; AmountBox.Text = ""; ProductBox.Text = ""; SkladBox.Text = ""; PostavshikBox.Text = "";
                return false;
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            bool answ = validation();
            if (answ == true)
            {
                var product = (ProductBox.SelectedItem as DataRowView).Row[0];
                var sklad = (SkladBox.SelectedItem as DataRowView).Row[0];
                var postavshik = (PostavshikBox.SelectedItem as DataRowView).Row[0];
                a.InsertQuery((int)product, (int)sklad, (int)postavshik, Convert.ToInt32(PriceBox.Text), Convert.ToInt32(AmountBox.Text));
                DG.ItemsSource = a.GetDataBy();
                DG.Columns[1].Visibility = Visibility.Collapsed;
                DG.Columns[2].Visibility = Visibility.Collapsed;
                DG.Columns[3].Visibility = Visibility.Collapsed;
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
                DG.Columns[1].Visibility = Visibility.Collapsed;
                DG.Columns[2].Visibility = Visibility.Collapsed;
                DG.Columns[3].Visibility = Visibility.Collapsed;
            }
            else { return; }
        }

        private void ChangeButtonClick(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                var id = (int)(DG.SelectedItem as DataRowView).Row[0];
                var product = (ProductBox.SelectedItem as DataRowView).Row[0];
                var sklad = (SkladBox.SelectedItem as DataRowView).Row[0];
                var postavshik = (PostavshikBox.SelectedItem as DataRowView).Row[0];
                a.UpdateQuery((int)product, (int)sklad, (int)postavshik, Convert.ToInt32(PriceBox.Text), Convert.ToInt32(AmountBox.Text), id);
                DG.ItemsSource = a.GetDataBy();
                DG.Columns[1].Visibility = Visibility.Collapsed;
                DG.Columns[2].Visibility = Visibility.Collapsed;
                DG.Columns[3].Visibility = Visibility.Collapsed;
            }
            else { return; }
        }

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DG.SelectedItem != null)
                {
                    PriceBox.Text = (string)(DG.SelectedItem as DataRowView).Row[1];
                    AmountBox.Text = (string)(DG.SelectedItem as DataRowView).Row[2];
                    ProductBox.Text = (string)(DG.SelectedItem as DataRowView).Row[3];
                    PostavshikBox.Text = (string)(DG.SelectedItem as DataRowView).Row[4];
                    SkladBox.Text = (string)(DG.SelectedItem as DataRowView).Row[5];
                }
            }
            catch (System.NullReferenceException) { return; }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DG.Columns[1].Visibility = Visibility.Collapsed;
            DG.Columns[2].Visibility = Visibility.Collapsed;
            DG.Columns[3].Visibility = Visibility.Collapsed;
        }
    }
}
