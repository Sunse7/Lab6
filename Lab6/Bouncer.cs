using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Lab6
{
    class Bouncer
    {
        Bar bar;
        public Bouncer(Bar bar)
        {
            this.bar = bar;
            Task.Run(() => 
            {
                while (bar.IsOpen)
                {
                    Thread.Sleep(bar.TimeToCheckID / 2);
                    if (bar.mainWindow.token.IsCancellationRequested)
                    {                        
                        return;
                    }
                    bar.guest.Enqueue(new Patron(bar));
                    Thread.Sleep(bar.TimeToCheckID / 2);
                }
                bar.Log("Bouncer goes home", MainWindow.LogBox.Patron);
            });            
        }
    }
}
