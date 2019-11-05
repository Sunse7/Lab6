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
                       
        }
        public void Work()
        {
            Task.Run(() => 
            {
                while (Bar.IsOpen)
                {
                    WaitToPickGlasses(); //Check for dirty glasses
                    bar.Log("Picking up dirty glasses from the tables", MainWindow.LogBox.Waitress);                   
                    DoDishes();
                    bar.Log("Washing the dirty glasses and putting them back onto the shelf", MainWindow.LogBox.Waitress);                    
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
