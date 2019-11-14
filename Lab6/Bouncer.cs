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
                    if (bar.CouplesNight)
                    {
                        CouplesNightTime();
                    }
                    else if (bar.Busload)
                    {
                        BusloadTime();
                    }
                    else
                    {
                        Thread.Sleep(bar.TimeToCheckID / 2);
                        if (bar.mainWindow.token.IsCancellationRequested)
                        {
                            return;
                        }
                        bar.guest.Enqueue(new Patron(bar));
                        Thread.Sleep(bar.TimeToCheckID / 2);
                    }                    
                }
                bar.Log("Bouncer goes home", MainWindow.LogBox.Patron);
            });            
        }
        private void CouplesNightTime()
        {
            Thread.Sleep(bar.TimeToCheckID / 2);
            if (bar.mainWindow.token.IsCancellationRequested)
            {
                return;
            }
            bar.guest.Enqueue(new Patron(bar));
            bar.guest.Enqueue(new Patron(bar));
            Thread.Sleep(bar.TimeToCheckID / 2);
        }
        private void BusloadTime()
        {
            if (bar.TimeUntilBarCloses <= 100) //First 20 sec
            {
                bar.Busload = false;
                for (int i = 0; i < 15; i++)
                {
                    bar.guest.Enqueue(new Patron(bar));
                }
            }
            else
            {
                Thread.Sleep(bar.TimeToCheckID * 2);
            if (bar.mainWindow.token.IsCancellationRequested)
            {
                return;
            }
            bar.guest.Enqueue(new Patron(bar));
            Thread.Sleep(bar.TimeToCheckID * 2);
            }
        }
    }
}
