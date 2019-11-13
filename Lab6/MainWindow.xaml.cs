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
using System.Collections.Concurrent;
using System.Threading;

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum LogBox { Bartender, Waitress, Patron }
        Bar bar;
        public CancellationTokenSource source = new CancellationTokenSource();
        public CancellationToken token;
        public MainWindow()
        {
            InitializeComponent();
            token = source.Token;
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
        private void OnOpenBarButtonClick(object sender, RoutedEventArgs e)
        {
            bar = new Bar(this);
            bar.Countdown(bar.BarIsOpenTime, TimeSpan.FromSeconds(1), cur => countDownLabel.Content = cur.ToString());            
        }
        private void OnCloseBarButtonClick(object sender, RoutedEventArgs e)
        {
            bar.IsOpen = false;
            source.Cancel();
        }         
    }
}
