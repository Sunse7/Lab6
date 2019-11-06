using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Concurrent;

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum LogBox { Bartender, Waitress, Patron }
        Bar bar;
        public MainWindow()
        {
            InitializeComponent();
            bar = new Bar(this);
            OpenBarButton.Click += Open_Bar_Click;
        }


        private void Open_Bar_Click(object sender, RoutedEventArgs e)
        {
            if (!Bar.IsOpen)
            {
                bar.OpenBar();
                OpenBarButton.Click += Open_Bar_Click;
            }
        }

        public void LogEvent(string text, LogBox textblock)
        {
            switch (textblock)
            {
                case LogBox.Bartender:
                    this.Dispatcher.Invoke(() => bartenderListBox.Items.Insert(0, text));
                    break;
                case LogBox.Patron:
                    this.Dispatcher.Invoke(() => patronListBox.Items.Insert(0, text));
                    break;
                case LogBox.Waitress:
                    this.Dispatcher.Invoke(() => waitressListBox.Items.Insert(0, text));
                    break;
            }
        }

    }
}
