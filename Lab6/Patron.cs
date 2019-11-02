using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab6
{
    class Patron
    {
        public string Name { get; set; }
        List<string> names = new List<string>();
        BeerGlass glass;
        Chair barChair;
        Bar bar;

        string[] patronNames = {"Nils", "Simon", "Alex", "Wille", "Sofia", "Charlotte",
        "Johan", "Jonas", "Emil", "Elvis", "Daniel", "Andrea", "Andreas", "Anders", "Karo", "Khosro", "Luna",
        "Nicklas", "Petter", "Robin", "Tijana", "Tommy", "Pontus", "John", "Andreé" };
        
        public Patron(Bar bar)
        {
            this.bar = bar;

            bar.patronList.Add(this);
            names.AddRange(patronNames);
            Random random = new Random();
            int randomName = random.Next(0, names.Count);
            int randomNum = random.Next(20000, 30001);
            Name = names[randomName];           

            Task.Run(() =>
            {
                bar.mainWindow.PatronListBoxMessage($"{Name} enters the bar");
                Thread.Sleep(1000); //Walk to bar
                bar.mainWindow.PatronListBoxMessage($"{Name} walks to the bar");
                //Get a beer from bartender
                bar.mainWindow.PatronListBoxMessage($"{Name} looks for an empty chair");
                LookForEmptyChair();
                Thread.Sleep(bar.TimeToFindChair);
                bar.mainWindow.PatronListBoxMessage($"{Name} sits down and drinks its beer");
                Thread.Sleep(randomNum); //Drink beer

                    //bar.table.Push(glass);
                    //bar.chair.Push(barChair);
                bar.mainWindow.PatronListBoxMessage($"{Name} leaves bar");
                bar.patronList.Remove(this);
                    // bar.IsOpen = false;               

            });
        }
        private void LookForEmptyChair()
        {
            while (bar.chair.Count == 0)
            {
                Thread.Sleep(500);
            }
            if (bar.chair.Count != 0)
            {
                bar.chair.TryPop(out barChair);
            }
            
        }       
    }
}
