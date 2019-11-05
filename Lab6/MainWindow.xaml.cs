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

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Bar bar = new Bar(this);
            
            
        }
        public void BartenderListBoxMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                bartenderListBox.Items.Insert(0, message);             
            });
        }        
        public void WaitressListBoxMessage(string message)
        {
            Dispatcher.Invoke(() => 
            {
                waitressListBox.Items.Insert(0, message);
            });
        }
        public void PatronListBoxMessage(string message)
        {
            Dispatcher.Invoke(() => 
            {
                patronListBox.Items.Insert(0, message);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Bar.IsOpen == false)
            {
                Bar.IsOpen = true;
                Countdown(120, TimeSpan.FromSeconds(1), cur => countdownTextBox.Text = cur.ToString());
                
            }
            else
            {
                Bar.IsOpen = false;
            }
            
        }
        void Countdown(int count, TimeSpan interval, Action<int> ts)
        {
            if(Bar.IsOpen == true)
            {
                var dt = new System.Windows.Threading.DispatcherTimer();
                dt.Interval = interval;
                dt.Tick += (_, a) =>
                {
                 if (count-- == 0)
                 {
                    dt.Stop();
                    Bar.IsOpen = false;
                 }
                 else
                 {
                    ts(count);
                 }
                    
                };
                ts(count);
                dt.Start();
            }
            
        }
    }
}
