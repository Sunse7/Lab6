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
        Bar bar;        
        public Waitress(Bar bar)
        {
            this.bar = bar;

            Task.Run(() => 
            {                
                while ((bar.patronList.Count + bar.guest.Count) > 0 || bar.IsOpen)
                {
                    WaitToPickGlasses();
                }
                bar.Log("Waitress goes home", MainWindow.LogBox.Waitress);
                bar.WaitressIsPresent = false;                
            });
        }
        private void WaitToPickGlasses()
        {
            while (!bar.table.IsEmpty)
            {
                if (bar.table.TryPop(out BeerGlass currentGlass))
                {
                    bar.glasses.Push(currentGlass);
                }
            }
            if (bar.glasses.Count > 0)
            {
                bar.Log("Picking up glass from table", MainWindow.LogBox.Waitress);
                Thread.Sleep(bar.TimeToPickGlasses);
                DoDishes();
            }
        }
        private void DoDishes()
        {
            bar.Log("Washing glasses", MainWindow.LogBox.Waitress);
            Thread.Sleep(bar.TimeToDoDishes);
           
            while (bar.glasses.Count > 0)
            {
                bar.shelf.Push(bar.glasses.Pop());
            }
            bar.Log("Putting it back in the shelf", MainWindow.LogBox.Waitress);
        }
    }
}
