using System;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney
{
    class Hulk:Hero
    {
        Random r = new Random();

        public Hulk() {
            this.Health = r.Next(750, 1001);
            this.Damage = r.Next(1, 101);
            this.Gold = r.Next(1, 5001);
            this.Name = "Omu verde mare";
        }
        

    }
}
