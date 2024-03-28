using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Pivoprovodapp
{
    public partial class BlinovozWindow : Window
    {
        public BlinovozWindow()
        {
            InitializeComponent();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this) { window.Close(); }
            }
            var main = new MainWindow();
            main.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
    }
}
