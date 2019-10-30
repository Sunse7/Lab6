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
        List<Patron> patrons = new List<Patron>();
        List<string> names = new List<string>();
        Bar bar = new Bar();
        BeerGlass glass = new BeerGlass();
        Chair barChair = new Chair();
        MainWindow mainWindow = new MainWindow();

        string[] patronNames = {"Nils", "Simon", "Alex", "Wille", "Sofia", "Charlotte",
        "Johan", "Jonas", "Emil", "Elvis", "Daniel", "Andrea", "Andreas", "Anders", "Karo", "Khosro", "Luna",
        "Nicklas", "Petter", "Robin", "Tijana", "Tommy", "Pontus", "John", "Andreé" };
        
        public Patron()
        {            
            names.AddRange(patronNames);
            Random random = new Random();
            int randInt = random.Next(20000, 30001);
            

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                mainWindow.PatronListBoxMessage($"{names} walks to the bar");
                //Get a beer
                mainWindow.PatronListBoxMessage($"{names} looks for an empty chair");
                if (bar.chair == null)
                {
                    bar.chair.TryTake(out barChair);
                    Thread.Sleep(4000);
                }
                else
                {
                    //Wait and try again
                }
                mainWindow.PatronListBoxMessage($"{names} sits down and drinks its beer");
                Thread.Sleep(randInt); //Drink beer

                bar.table.Add(glass);
                bar.chair.Add(barChair);
                mainWindow.PatronListBoxMessage("Leaves bar when done");
            });
        }
        
        
       
    }
}
