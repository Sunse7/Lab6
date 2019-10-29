using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Lab6
{
    class Bar
    {
        private static int numOfGlasses = 8;
        private static int numOfChairs = 9;
        public BlockingCollection<BeerGlass> shelf = new BlockingCollection<BeerGlass>(numOfGlasses);
        public BlockingCollection<BeerGlass> table = new BlockingCollection<BeerGlass>(numOfGlasses);
        public BlockingCollection<Chair> chair = new BlockingCollection<Chair>(numOfChairs);
        public ConcurrentQueue<Patron> guest = new ConcurrentQueue<Patron>();
        public Bar()
        {
            
        }
    }
}
