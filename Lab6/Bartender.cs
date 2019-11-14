using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
namespace Lab6
{
    class Bartender
    {
        BeerGlass glass;
        Bar bar;
        Patron patron;
        
        public Bartender(Bar bar)
        {            
            this.bar = bar;

            Task.Run(() =>
            {
                while ((bar.patronList.Count + bar.guest.Count) > 0 || bar.IsOpen)
                {
                    bar.Log("Waiting for guest to arraive", MainWindow.LogBox.Bartender);

                    LookingForGuest();
                    WhenGuestOrders();
                }
            });
        }
        private void LookingForGuest()
        {
            while (bar.guest.Count == 0)
            {
                Thread.Sleep(50);
                if (bar.mainWindow.token.IsCancellationRequested)
                {
                    BartenderGoHome();
                    break;
                }
            }
            if (bar.guest.TryDequeue(out patron))
            {
                Thread.Sleep(bar.TimeToGetGlass);                
                bar.Log("Walking to shelf", MainWindow.LogBox.Bartender);
            }         
        }
        public void WhenGuestOrders()
        {
            if(bar.shelf.Count != 0 && this.patron != null)
            {
                bar.shelf.TryPop(out this.glass);
                Thread.Sleep(bar.TimeToPourBeer);
                bar.Log("Gives beer to guest", MainWindow.LogBox.Bartender);
                bar.GotBeer = true;                
            }
            else
            {
                Thread.Sleep(50);
                WhenGuestOrders();
            }
        }
        public void BartenderGoHome()
        {
            while ((bar.patronList.Count + bar.guest.Count) > 0 || bar.WaitressIsPresent) { }
            bar.CloseBar();
            bar.Log("I'll go home now", MainWindow.LogBox.Bartender);
        }
    }
}
