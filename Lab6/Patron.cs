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
        
        public Patron()
        {
            Random random = new Random();
            int randInt = random.Next(10000, 20001);

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                //Log walk to bar
                //Get a beer
                Thread.Sleep(4000);
                bar.chair.Take();
                //Drink beer
                Thread.Sleep(randInt);
                bar.table.Add(glass);
                bar.chair.Add(barChair);
            });
        }
        /*names.Add("Nils", "Simon", "Alex", "Wille", "Sofia", "Charlotte",
        "Johan", "Jonas", "Emil", "Elvis", "Daniel", "Andrea", "Andreas", "Anders", "Karo", "Khosro", "Luna",
        "Nicklas", "Petter", "Robin", "Tijana", "Tommy", "Pontus", "John", "Andreé");*/
       
    }
}
