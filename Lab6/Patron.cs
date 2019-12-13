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
            lock (bar.myListLock) { bar.patronList.Add(this); }
            
            names.AddRange(patronNames);
            int randomName = Bar.random.Next(0, names.Count);
            Name = names[randomName];
            Run();            
        }
        private void Run()
        {
            Task.Run(() =>
            {
                //while (bar.IsOpen)
                //{
                    EnterBar();
                    LookForEmptyChair();
                    DrinkBeer();
                    while (ExitBar() == false)
                    {
                        Thread.Sleep(50);
                    }
                //}
            });
        }
        private void EnterBar()
        {
            bar.Log($"{Name} enters the bar", MainWindow.LogBox.Patron);
            Thread.Sleep(bar.TimeToWalkToBar);
            bar.Log($"{Name} arrives at the bar", MainWindow.LogBox.Patron);
        }        
        public void LookForEmptyChair()
        {
            while(bar.GotBeer == false)
            {
                Thread.Sleep(50);
            }
            if (bar.GotBeer == true)
            {
                bar.GotBeer = false;
                bar.Log($"{Name} looks for an empty chair", MainWindow.LogBox.Patron);
                Thread.Sleep(bar.TimeToFindChair);
                while (bar.chair.TryPop(out chair) == false)
                {
                    Thread.Sleep(50);
                }
            }            
        }
        private void DrinkBeer()
        {
            bar.Log($"{Name} sits down and drinks their beer", MainWindow.LogBox.Patron);
            Thread.Sleep(bar.TimeToDrinkBeer);            
        }
        private bool ExitBar()
        {
            if (bar.table.Count == bar.MaxNumOfGlasses) { return false; }            
            else { bar.table.Push(this.glass); }

            if (bar.chair.Count == bar.MaxNumOfChairs) { return false; }           
            else { bar.chair.Push(this.chair); }

            Thread.Sleep(new Random().Next(1,5)); //silvertejp
            lock (bar.myListLock)
            {
                bar.patronList.Remove(this);
            }

            //bar.patronList.Remove(this);
            bar.Log($"{Name} leaves bar", MainWindow.LogBox.Patron);
            return true;
        }
    }
}
