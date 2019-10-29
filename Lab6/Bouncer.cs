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
        public Bouncer(Bar bar)
        {
            Random random = new Random();
            int randInt = random.Next(3000, 10001);

            Task.Run(() => 
            {
                Thread.Sleep(randInt);
                bar.guest.Enqueue(new Patron());
            });
        }
        
    }
}
