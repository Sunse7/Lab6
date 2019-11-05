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
        Chair chair;
        Bar bar;

        string[] patronNames = {"Nils", "Simon", "Alex", "Wille", "Sofia", "Charlotte",
        "Johan", "Jonas", "Emil", "Elvis", "Daniel", "Andrea", "Andreas", "Anders", "Karo", "Khosro", "Luna",
        "Nicklas", "Petter", "Robin", "Tijana", "Tommy", "Pontus", "John", "Andreé" };
        
        public Patron(Bar bar)
        {
            this.bar = bar;
            
            bar.patronList.Add(this);
            names.AddRange(patronNames);
            int randomName = Bar.random.Next(0, names.Count);            
            Name = names[randomName];           

            Task.Run(() =>
            {
                while (bar.IsOpen) //? Bar closes in TimeStamp when button click CloseBar or time hits 0
                {
                    bar.mainWindow.PatronListBoxMessage($"{Name} enters the bar");
                    Thread.Sleep(1000); //Walk to bar
                    bar.mainWindow.PatronListBoxMessage($"{Name} walks to the bar");
                    LookForEmptyChair();
                    Thread.Sleep(bar.TimeToDrinkBeer); //Drink beer

                    bar.table.Push(glass);
                    bar.chair.Push(chair);
                    bar.mainWindow.PatronListBoxMessage($"{Name} leaves bar");
                    bar.patronList.Remove(this);

                }
            });
        }
        private void LookForEmptyChair()
        {            
            while (bar.chair.Count == 0)
            {
                Thread.Sleep(50);
            }
            if (bar.chair.Count > 0 && bar.GotBeer)
            {
                bar.GotBeer = false; //First guest does not LookForEmptyChair(); bool needs to be somewhere else
                bar.mainWindow.PatronListBoxMessage($"{Name} looks for an empty chair");
                Thread.Sleep(bar.TimeToFindChair);
                bar.chair.TryPop(out chair);
                bar.mainWindow.PatronListBoxMessage($"{Name} sits down and drinks its beer");
            }            
        }       
    }
}
