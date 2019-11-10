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

            Task.Run(async () =>
            {
                while (true) //Bartender goes home when the last guest leaves
                {                     
                    bar.Log("Waiting for guest to arraive", MainWindow.LogBox.Bartender);

                    await LookingForGuest();
                    await WhenGuestOrders();
                    
                    if(bar.patronList.Count == 0 && bar.IsOpen == false)
                    {   
                        //Last guest leaves not IsEmpty
                        bar.Log("Batrender goes home", MainWindow.LogBox.Bartender);
                    }
                }
            });
        }
        private async Task LookingForGuest()
        {
            while (bar.guest.Count == 0)
            {
                Thread.Sleep(50);
            }
            if (bar.guest.TryPeek(out patron))
            {
                Thread.Sleep(bar.TimeToGetGlass);                
                bar.Log("Walking to shelf", MainWindow.LogBox.Bartender);
            }            
        }
        public async Task WhenGuestOrders()
        {
            if(bar.shelf.Count != 0)
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
    }
}
