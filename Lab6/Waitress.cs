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
                    WaitToPickGlasses();                                     
                    DoDishes();
                }                
            });
        }
        private void WaitToPickGlasses()
        {
            while (bar.table.Count == 0)
            {
                Thread.Sleep(50);
            }
            if (bar.table.Count > 0)
            {
                foreach (var item in bar.table)
                {
                    bar.table.TryPop(out this.glass);
                }
                bar.Log("Picking up glass from table", MainWindow.LogBox.Waitress);
                Thread.Sleep(bar.TimeToPickGlasses);
            }
        }
        private void DoDishes()
        {
            bar.Log("Washing glasses", MainWindow.LogBox.Waitress);
            Thread.Sleep(bar.TimeToDoDishes);
            bar.Log("Putting it back in the shelf", MainWindow.LogBox.Waitress);

            foreach (var item in bar.table)
            {
                bar.shelf.Push(this.glass);
            }
        }
    }
}
