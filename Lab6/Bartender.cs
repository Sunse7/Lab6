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
        
        
        public Bartender(Bar bar)
        {
            
            
            Task.Run(() =>
            {
                //Log to listbox - Walking to shelf
                
                Thread.Sleep(3000);
                bar.shelf.Take();
                Thread.Sleep(3000);
                //Log to Listbox - Pouring beer
            });
        }
    }
}
