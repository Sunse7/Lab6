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
            
        }  
        public void Work()
        {
            Task.Run(() => 
            {
                while (Bar.IsOpen)
                {
                    Thread.Sleep(bar.TimeToCheckID);
                    bar.guest.Enqueue(new Patron(bar));
                }
                bar.Log("The Bouncer goes home", MainWindow.LogBox.Patron);
                //bar.mainWindow.PatronListBoxMessage("Bouncer goes home"); //When bar closes
            });
        }      
    }
}
