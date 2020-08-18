using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HeroJourney
{
    class Champion:Enemy
    {
        Random r = new Random();

        public Champion() {

            this.Health = r.Next(1, 201);
            this.Damage = r.Next(1, 51);
            this.Clumsiness = r.Next(1, 101);
            this.BribeAmount = r.Next(750, 1001);
            this.GoldBounty = r.Next(50, 200);
        }

        public override string showEnemyType()
        {
            return "ENEMY TYPE: " + MethodBase.GetCurrentMethod().DeclaringType.ToString().Split(".")[1];
        }
    }
}
