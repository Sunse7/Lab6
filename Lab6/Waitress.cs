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
        public Waitress(Bar bar)
        {
            Task.Run(() => 
            {
                //Log - Pick glass from table
                bar.table.Take();
                Thread.Sleep(10000);
                //Log - Washing
                Thread.Sleep(15000); //Loop?
                bar.shelf.Add(glass);
            });
        }
    }
}
