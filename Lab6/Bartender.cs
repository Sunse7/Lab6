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
                while (true) //Bartender goes home when the last guest leaves
                {
                    //Loop for every new guest?
                    
                    bar.mainWindow.BartenderListBoxMessage("Waiting for guest to arraive");
                    //Patron thisPatron = LookingForGuest();  
                    LookingForGuest();
                    WhenGuestOrders();
                    
                    /*if(bar.guest.?)
                    {   
                        //Last guest leaves not IsEmpty
                        bar.mainWindow.BartenderListBoxMessage("Batrender goes home");
                    }*/
                }
            });
        }
        private void LookingForGuest()
        {
            while (bar.guest.Count == 0)
            {
                Thread.Sleep(50);
            }
            if (bar.guest.TryPeek(out patron))
            {
               // bar.guest.TryDequeue(out patron);
                bar.mainWindow.BartenderListBoxMessage("Walking to shelf");
                Thread.Sleep(bar.TimeToGetGlass);
                
            }
            
        }
      /*  private Patron LookingForGuest()
        {            
            while(bar.guest.Count == 0)
            {
                Thread.Sleep(50);
            }
            if (bar.guest.TryPeek(out patron))
            {
                bar.guest.TryDequeue(out patron);
                bar.mainWindow.BartenderListBoxMessage("Walking to shelf");
                Thread.Sleep(bar.TimeToGetGlass);
                return patron;
            }
            else
                return null;
        }*/
        private void WhenGuestOrders() //WhenGuestArrives?
        {
            while(bar.shelf.Count == 0)
            {
                Thread.Sleep(50);
            }
            if(bar.shelf.Count != 0)
            {
                bar.shelf.TryPop(out glass);
                Thread.Sleep(bar.TimeToPourBeer);
                bar.mainWindow.BartenderListBoxMessage($"Gives beer to guest");
                bar.GotBeer = true;
            }
        }
    }
}
