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
        MainWindow mainWindow = new MainWindow();
        
        public Bartender(Bar bar)
        {
            
            
            Task.Run(() =>
            {
                mainWindow.BartenderListBoxMessage("Waiting for guest to arraive");
                //When guest arraive
                mainWindow.BartenderListBoxMessage("Walking to shelf"); 
                Thread.Sleep(3000);
                bar.shelf.Take(); //Taking a glass
                Thread.Sleep(3000);
                mainWindow.BartenderListBoxMessage("Pouring beer");
                //Gives beer to patron
            });
        }
    }
}
