using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab6
{
    class Waitress
    {
        BeerGlass glass = new BeerGlass();
        MainWindow mainWindow = new MainWindow();
        public Waitress(Bar bar)
        {
            Task.Run(() => 
            {
                mainWindow.WaitressListBoxMessage("Picking up glass from table"); 
                bar.table.Take(); //Taking a glass from table
                Thread.Sleep(10000);
                mainWindow.WaitressListBoxMessage("Washing glass and putting it back in the shelf");
                Thread.Sleep(15000); //Loop?
                bar.shelf.Add(glass); //Adding a glass to shelf
            });
        }
    }
}
