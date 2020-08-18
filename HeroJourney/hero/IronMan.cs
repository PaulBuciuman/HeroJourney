using System;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney
{
    class IronMan:Hero
    {
        Random r = new Random();

        public IronMan()
        {
            this.Health = r.Next(1, 1001);
            this.Damage = r.Next(1, 101);
            this.Gold = r.Next(3000, 5001);
            this.Name = "Omu dă fer";
        }
        
    }
}
