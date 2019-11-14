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
                do
                {
                    WaitToPickGlasses();
                    DoDishes();
                    if (bar.BartenderWorking == false)
                    {
                        break;
                    }
                }
                while (bar.IsOpen && bar.patronList.Count > 0);
                bar.Log("Waitress goes home", MainWindow.LogBox.Waitress);
                bar.WaitressIsPresent = false;
                
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
                    bar.table.TryPop(out glass);
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
                bar.shelf.Push(glass);
            }
        }
    }
}
