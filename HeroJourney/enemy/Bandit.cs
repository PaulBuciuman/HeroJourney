using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HeroJourney
{
    class Bandit:Enemy
    {

        Random r = new Random();

        public Bandit() {
            this.Health = r.Next(1, 201);
            this.Damage = r.Next(1, 51);
            this.Clumsiness = r.Next(1, 25);
            this.BribeAmount = r.Next(1, 301);
            this.GoldBounty = r.Next(100, 250);
        }
        public override string showEnemyType()
        {
            return "ENEMY TYPE: " + MethodBase.GetCurrentMethod().DeclaringType.ToString().Split(".")[1];
        }
    }
}
