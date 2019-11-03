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
        private static int MaxNumOfGlasses = 8;
        private static int MaxNumOfChairs = 9;
        public static Random random = new Random();
        public MainWindow mainWindow;
        public ConcurrentStack<BeerGlass> shelf;
        public ConcurrentStack<BeerGlass> table;
        public ConcurrentStack<Chair> chair;
        public ConcurrentQueue<Patron> guest;
        public List<Patron> patronList;
        public bool IsOpen = true;
        public bool GotBeer = false;
        public int TimeToFindChair = 4000;
        public int TimeToDrinkBeer = random.Next(20000, 30001);
        public int TimeToGetGlass = 3000;
        public int TimeToPourBeer = 3000;
        public int TimeToCheckID = random.Next(3000, 10001);
        public int TimeToPickGlasses = 10000;
        public int TimeToDoDishes = 15000;
        public Bar(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            shelf = new ConcurrentStack<BeerGlass>();
            table = new ConcurrentStack<BeerGlass>();
            chair = new ConcurrentStack<Chair>();
            guest = new ConcurrentQueue<Patron>();
            patronList = new List<Patron>();
            var bouncer = new Bouncer(this);
            var waitress = new Waitress(this);
            var bartender = new Bartender(this);        
           
            for (int i = 0; i < MaxNumOfGlasses; i++)
            {
                shelf.Push(new BeerGlass());
            }
            for (int i = 0; i < MaxNumOfChairs; i++)
            {
                chair.Push(new Chair());
            }            
        }
    }
}
