using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HeroJourney
{
    class Ogre:Enemy
    {
        Random r = new Random();

        public Ogre() {
            this.Health = r.Next(150, 201);
            this.Damage = r.Next(30, 51);
            this.Clumsiness = r.Next(60, 101);
            this.BribeAmount = r.Next(1, 1001);
            this.GoldBounty = r.Next(150, 300);
        }

        public override string showEnemyType()
        {
            return "ENEMY TYPE: " + MethodBase.GetCurrentMethod().DeclaringType.ToString().Split(".")[1];
        }
    }
}
