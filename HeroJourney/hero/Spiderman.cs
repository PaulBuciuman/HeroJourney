using System;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney
{
    class Spiderman:Hero
    {
        Random r = new Random();

        public Spiderman()
        {
            this.Health = r.Next(1, 1001);
            this.Damage = r.Next(60, 101);
            this.Gold = r.Next(1, 5001);
            this.Name = "Paianganu";
        }
        
    }
}
