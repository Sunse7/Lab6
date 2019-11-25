using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Threading;

namespace Lab6
{
    class Bar
    {
        public int MaxNumOfGlasses = 8;
        public int MaxNumOfChairs = 9;
        public static Random random = new Random();
        public MainWindow mainWindow;
        public ConcurrentStack<BeerGlass> shelf;
        public ConcurrentStack<BeerGlass> table;
        public ConcurrentStack<Chair> chair;
        public ConcurrentQueue<Patron> guest;
        public Stack<BeerGlass> glasses;
        public List<Patron> patronList;
        public bool IsOpen { get; set; } = true;
        public bool WaitressIsPresent = true;
        public bool GotBeer { get; set; } = false;
        public bool CouplesNight = false;
        public bool Busload = false;
        public bool BusloadIncomplete = true;
        public int TimeToCheckID = random.Next(3000, 10001);
        public int TimeToDrinkBeer = random.Next(20000, 30001);
        public int TimeToWalkToBar = 1000;
        public int TimeToFindChair = 4000;
        public int TimeToGetGlass = 3000;
        public int TimeToPourBeer = 3000;
        public int TimeToPickGlasses = 10000;
        public int TimeToDoDishes = 15000;
        public int BarIsOpenTime = 120;
        

        public Bar(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            shelf = new ConcurrentStack<BeerGlass>();
            table = new ConcurrentStack<BeerGlass>();
            chair = new ConcurrentStack<Chair>();
            guest = new ConcurrentQueue<Patron>();
            glasses = new Stack<BeerGlass>();
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
            
            BarInfo();
        }
        public void BarInfo()
        {
            Task.Run(() => {
                while (true)
                {
                    Thread.Sleep(50);                    
                    mainWindow.Dispatcher.Invoke(() =>
                    {
                        mainWindow.numOfGuestInBarLable.Content = $"Number of guests in bar: {patronList.Count} Num of waiting guests: {guest.Count}";                        
                        mainWindow.numOfGlassesInShelfLable.Content = $"Number of glasses in shelf: {shelf.Count} (Max: {MaxNumOfGlasses})";
                        mainWindow.numOfEmptyChairsLable.Content = $"Number of empty chairs: {chair.Count} (Max: {MaxNumOfChairs})";
                    });
                }
            }); 
        }
        public void Log(string text, MainWindow.LogBox listbox)
        {
            string timeStamp = DateTime.Now.ToString("mm:ss");
            mainWindow.LogEvent($"{timeStamp} {text}", listbox);
        }
        public void Countdown(int count, TimeSpan interval, Action<int> ts)
        {            
            var dt = new DispatcherTimer();            
            dt.Interval = interval;
            dt.Tick += (_, a) =>
            {                
                if (count-- == 0)
                {
                    dt.Stop();
                    IsOpen = false;
                    CloseBar();
                }
                else if (IsOpen == false)
                {
                    dt.Stop();
                }
                else
                    ts(count);
            };
            ts(count);
            dt.Start();
        }
        public void CloseBar()
        {
            IsOpen = false;
            mainWindow.source.Cancel();
        }
    }
}
