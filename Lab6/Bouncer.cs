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
        public Bouncer(Bar bar)
        {
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
