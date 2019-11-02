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
        //Patron patron;
        public Bartender(Bar bar)
        {
            this.bar = bar;
            Task.Run(() =>
            {   
                //Loop for every new guest
                //Tweek walking to shelf so it doesnt happen befor guest arrives 
                bar.mainWindow.BartenderListBoxMessage("Waiting for guest to arraive");
                LookingForGuest();
                //When guest arraive
                Thread.Sleep(3000);
                WhenGuestOrders();
                //bar.shelf.TryPop(out glass); //Taking a glass
                Thread.Sleep(3000); //Pouring beer
                bar.mainWindow.BartenderListBoxMessage("Pouring beer");
                //Gives beer to patron, only metaforically? does he need to accully get the glass?

                /*if(bar.guest.IsEmpty) //IsEmpty?? NO! then he will go home before anyone comes to the bar!
                {   
                    //Last guest leaves not IsEmpty
                    bar.mainWindow.BartenderListBoxMessage("Batrender goes home");
                }*/
            });
        }
        private void LookingForGuest()
        {
            Patron patron;
            //Behöver en loop som fortsätter att kolla om det kommit patrons
            if (bar.guest.TryPeek(out patron)) //Maybe works?? dunno
            {
                bar.mainWindow.BartenderListBoxMessage("Walking to shelf"); //Befor or after WhenGUest()?

                Thread.Sleep(1500);
            }
            

            //Look for guest
           /* if (bar.guest.TryDequeue(out patron) 
            {
                Thread.Sleep(1500); 
            }  */          
        }
        private void WhenGuestOrders() //WhenGuestArrives?
        {
            while(bar.shelf.Count == 0)
            {
                Thread.Sleep(500);
            }
            if(bar.shelf.Count != 0)
            {
                bar.shelf.TryPop(out glass);
            }
        }
    }
}
