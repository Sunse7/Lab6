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
        BeerGlass glass;
        Bar bar;
        public Waitress(Bar bar)
        {
            this.bar = bar;

            Task.Run(() => 
            {
                while (bar.IsOpen)
                {
                    WaitToPickGlasses(); //Check for dirty glasses
                    bar.mainWindow.WaitressListBoxMessage("Picking up glass from table");                    
                    DoDishes();
                    bar.mainWindow.WaitressListBoxMessage("Washing glass and putting it back in the shelf");                    
                }                
            });
        }

        private void WaitToPickGlasses()
        {
            while (bar.table.Count == 0)
            {
                Thread.Sleep(50);
            }

            if (bar.table.Count != 0)
            {
                foreach (var item in bar.table)
                {
                    bar.table.TryPop(out glass);
                }
                Thread.Sleep(bar.TimeToPickGlasses);
            }
        }

        private void DoDishes()
        {
            foreach (var item in bar.table)
            {
                bar.shelf.Push(glass);
            }
            Thread.Sleep(bar.TimeToDoDishes);
        }
    }
}
