using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pivoprovodapp.DSTableAdapters;

namespace Pivoprovodapp
{
    public partial class MainWindow : Window
    {
        LoginsTableAdapter a = new LoginsTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != "" && PasswordBox.Password != "")
            {
                var all = a.GetData().Rows;

                for (int i = 0; i < all.Count; i++)
                {
                    if (all[i][3].ToString() == LoginBox.Text && all[i][4].ToString() == PasswordBox.Password)
                    {
                        int roleID = (int)all[i][5];

                        if (roleID == 1) { var window = new AdminWindow(); window.Show(); Close(); }
                        if(roleID == 2) { var window = new BlinovozWindow(); window.Show(); Close(); }
                    }
                }
            }
            else { MessageBox.Show("Все поля должны быть заполнены!"); LoginBox.Text = ""; PasswordBox.Password = ""; }
        }
    }
}
