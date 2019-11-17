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
                    if (bar.Busload)
                    {
                        BusloadTime();
                    }
                    if (bar.CouplesNight)
                    {
                        CouplesNightTime();
                    }
                    else
                    {
                        int sleepTime;
                        if (bar.Busload)
                        {
                            sleepTime = bar.TimeToCheckID;
                        }
                        else
                        {
                            sleepTime = bar.TimeToCheckID / 2;
                        }
                        Thread.Sleep(sleepTime);
                        if (bar.mainWindow.token.IsCancellationRequested)
                        {
                            return;
                        }
                        bar.guest.Enqueue(new Patron(bar));
                        Thread.Sleep(sleepTime);
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
            while (busloadIncomplete)
            {
                if (bar.BarIsOpenTime < 100)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        bar.guest.Enqueue(new Patron(bar));
                        busloadIncomplete = false;
                    }
                }
            }
            //else
            //{
            //    while (bar.IsOpen)
            //    {
            //        // Sleeping twice to ease cancellation.
            //        // Thereby normal sleeps, insead of ((Sleep/2)*2)
            //        Thread.Sleep(bar.TimeToCheckID);
            //        if (bar.mainWindow.token.IsCancellationRequested)
            //        {
            //            return;
            //        }
            //        bar.guest.Enqueue(new Patron(bar));
            //        Thread.Sleep(bar.TimeToCheckID);
            //    }                
            //}
        }
    }
}
