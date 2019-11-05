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
        public static int NumOfGuestsInBar { get; set; } = 0;
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
                        
        }
        public void Visit()
        {
            Task.Run(() =>
            {
                NumOfGuestsInBar++;
                while (Bar.IsOpen) //? Bar closes in TimeStamp when button click CloseBar or time hits 0
                {
                    bar.Log($"{Name} enters the bar", MainWindow.LogBox.Patron);
                    Thread.Sleep(bar.TimeToWalkToBar);
                    bar.Log($"{Name} walks to the bar", MainWindow.LogBox.Patron);
                    LookForEmptyChair();
                    bar.Log($"{Name} sits down and drinks their beer", MainWindow.LogBox.Patron);
                    Thread.Sleep(bar.TimeToDrinkBeer);
                    bar.table.Push(glass);
                    bar.chair.Push(chair);
                    bar.Log($"{Name} leaves the bar", MainWindow.LogBox.Patron);
                    bar.patronList.Remove(this);
                    NumOfGuestsInBar--;
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
                bar.GotBeer = false; //Should work now
                bar.Log($"{Name} looks for an empty chair", MainWindow.LogBox.Patron);
                Thread.Sleep(bar.TimeToFindChair);
                bar.chair.TryPop(out chair);
            }            
        }       
    }
}
